using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattleMgr : MonoBehaviour {

	public Image battleStatus;

	public Image playerHp;
	public Text   pHp;
	public Image enemyHp;
	public Text eHp;
	public Image playerExp;
	
	public Image[] choice;
	public Image[] skillChoice;
	public Image [] skills;
	public Image battleWindow;

	public Image enemyImg;
	public Image playerImg;
	public Text  enemyLevel;
	public Text  playerLevel;

	public Pokemon enemy;
	public Pokemon player;

	private bool isBattle;
	private bool playerBattle;
	private bool isRender;
	private bool isInfo;
	private bool isChoice;
	private bool isTurn;
	private bool isFightRender;
	private bool skillUsed;

	void Start()
	{
		isFightRender = true;
		skillUsed = true;
		isTurn = true;
		isRender = false;
		eraseRender();
		enemyLevel.enabled = false;
		playerLevel.enabled = false;
	

		foreach(Image c in choice)
		{
			c.enabled = false;
		}
		choice[0].enabled = true;
		isChoice = false;
	}

	// Update is called once per frame
	void Update () {
		isBattle = GameObject.Find("Player").GetComponent<Player>().isBattle;
		playerBattle = GameObject.Find("Player").GetComponent<Player>().playerBattle;
		//if(isBattle == true  )
		//{
			if(isTurn)
			{
				if(isRender == false )
				{
					if(isBattle == true)
				{
					getRender("PokemonMgr");
					GameObject[] s = GameObject.FindGameObjectsWithTag("enemy");
					foreach(GameObject c in s)
					{
						if(c.GetComponent<Trainer>().isFight != true)
						{
							Destroy(c);
						}
					}
				}
				else
					getRender("Trainer");
				}
				if(isFightRender == false)
				{
					getFightRender();
				}

				if(isChoice == false)
				{
					getChoice();
				}
				if(skillUsed == false)
				{
					fight();
				}
			}
			else
			{
				Skill s = enemy.skill[Random.Range(0,4)];
				while(s.isUse == false)
				{
					 s = enemy.skill[Random.Range(0,4)];
				}
				useSkill(s,enemy,player);
				isTurn = true;
				isChoice = false;
				choice[0].enabled = true;
			}

			if(isInfo == false)
			{
				getInfo();
			}
		//}
	}
	void getChoice()
	{
		if(choice[0].enabled == true)
		{
			if(Input.GetKey(KeyCode.Space))
			   {
				isChoice = true;
				choice[0].enabled = false;
				skillUsed = false;
				isFightRender = false;
			}
			else if(Input.GetKey(KeyCode.RightArrow))
			        {
				choice[0].enabled = false;
				choice[1].enabled = true;
			}
			else if (Input.GetKey(KeyCode.DownArrow))
			         {
				choice[0].enabled = false;
				choice[2].enabled = true;
			}
		}
		else if(choice[1].enabled == true)
		{
			if(Input.GetKey(KeyCode.Space))
			{
				isChoice = true;
			}
			else if(Input.GetKey(KeyCode.LeftArrow))
			{
				choice[0].enabled = true;
				choice[1].enabled = false;
			}
			else if (Input.GetKey(KeyCode.DownArrow))
			{
				choice[1].enabled = false;;
				choice[3].enabled = true;
			}
		}
		else if(choice[2].enabled == true)
		{
			if(Input.GetKey(KeyCode.Space))
			{
				isChoice = true;
			}
			else if(Input.GetKey(KeyCode.RightArrow))
			{
				choice[2].enabled = false;
				choice[3].enabled = true;
			}
			else if (Input.GetKey(KeyCode.UpArrow))
			{
				choice[0].enabled = true;
				choice[2].enabled = false;
			}
		}
		else if(choice[3].enabled == true)
		{
			if(Input.GetKey(KeyCode.Space))
			{
				battleEnd();
				isChoice = true;
			}
			else if(Input.GetKey(KeyCode.UpArrow))
			{
				choice[3].enabled = false;
				choice[1].enabled = true;
			}
			else if (Input.GetKey(KeyCode.LeftArrow))
			{
				choice[3].enabled = false;
				choice[2].enabled = true;
			}
		}
	}

	void getRender(string s)
	{	
		if( s == "Trainer")
			enemy = GameObject.Find(s).GetComponent<Trainer>().curPokemon;
		else
			enemy = GameObject.Find(s).GetComponent<PokemonMgr>().enemy;
		player = GameObject.Find("Player").GetComponent<Player>().curPokemon;
		enemyImg.sprite = enemy.GetComponent<Pokemon>().curSprite;
		playerImg.sprite = player.GetComponent<Pokemon>().curSprite;
		playerLevel.enabled = true;
		enemyLevel.enabled = true;
		playerLevel.text ="LV : " + player.getLevel();
		enemyLevel.text ="LV : " +  enemy.getLevel();

		isRender = true;
	}

	void getFightRender()
	{
		getSkills();
		battleWindow.enabled = true;
		for(int i = 0; i<4;i++)
		{
			skills[i].enabled = true;
			skills[i].GetComponentInChildren<Text>().enabled = true;
			skillChoice[0].enabled = true;
		}
		isFightRender = true;
	}

	void getInfo()
	{
		int a = enemy.getHP();
		int b = enemy.getMaxHP();
		int c = player.getHP();
		int d = player.getMaxHP();
		int e = player.getExp();
		pHp.text = "" + c + "/"  +d;
		eHp.text = "" + a + "/" + b;

		if ( a<= 0)
		{
			player.expPlus(Random.Range(10,36));
			battleEnd();
		}
		
		if( e>=100)
		{
			player.leveUp();
			e-=100;
		}
		
		if(c<=0)
		{
			Application.LoadLevel(Application.loadedLevel);
		}

		enemyHp.fillAmount = (float)a/b;
		playerHp.fillAmount = (float)c/d;
		playerExp.fillAmount = (float) e/100; 

		isInfo = true;
	}

	void getSkills()
	{
		for(int i = 0; i<4;i++)
		{
			Skill skill = GameObject.Find("Player").GetComponent<Player>().curPokemon.skill[i];
			if(skill.isUse != false)
			{
				this.skills[i].GetComponentInChildren<Text>().text =skill.name;
			}
			else{
				this.skills[i].GetComponentInChildren<Text>().text = "없음";
			}

		}
	}

	void fight()
	{
			if(skillChoice[0].enabled == true)
			{
				if(Input.GetKey(KeyCode.Z))
				{
						if(player.skill[0].isUse != false)
					{
						useSkill(player.skill[0],player,enemy);	
						isTurn = false;
						skillUsed = true;
						eraseRender();
					}
				else
					skillUsed = false;
				}
				else if(Input.GetKey(KeyCode.RightArrow))
				{
					skillChoice[0].enabled = false;
					skillChoice[1].enabled = true;
				}
				else if (Input.GetKey(KeyCode.DownArrow))
				{
					skillChoice[0].enabled = false;
					skillChoice[2].enabled = true;
				}
			}
			else if(skillChoice[1].enabled == true)
			{
				if(Input.GetKey(KeyCode.Z))
				{
					if(player.skill[1].isUse != false)
					{
						useSkill(player.skill[1],player,enemy);	
					isTurn = false;	
					skillUsed = true;
					eraseRender();
					}
					else
					skillUsed = false;
				}
				else if(Input.GetKey(KeyCode.LeftArrow))
				{
					skillChoice[0].enabled = true;
					skillChoice[1].enabled = false;
				}
				else if (Input.GetKey(KeyCode.DownArrow))
				{
					skillChoice[1].enabled = false;;
					skillChoice[3].enabled = true;
				}
			}
			else if(skillChoice[2].enabled == true)
			{
				if(Input.GetKey(KeyCode.Z))
				{
					if(player.skill[2].isUse != false)
					{
						useSkill(player.skill[2],player,enemy);	
						isTurn = false;
						skillUsed = true;
						eraseRender();
					}
					else
					skillUsed = false;
				}
				else if(Input.GetKey(KeyCode.RightArrow))
				{
					skillChoice[2].enabled = false;
					skillChoice[3].enabled = true;
				}
				else if (Input.GetKey(KeyCode.UpArrow))
				{
					skillChoice[0].enabled = true;
					skillChoice[2].enabled = false;
				}
			}
			else if(skillChoice[3].enabled == true)
			{
				if(Input.GetKey(KeyCode.Z))
				{
					if(player.skill[3].isUse != false)
					{
						useSkill(player.skill[3],player,enemy);	
						isTurn = false;
						skillUsed = true;
						eraseRender();
					}
				else
					skillUsed = false;
					
				}
				else if(Input.GetKey(KeyCode.UpArrow))
				{
					skillChoice[3].enabled = false;
					skillChoice[1].enabled = true;
				}
				else if (Input.GetKey(KeyCode.LeftArrow))
				{
					skillChoice[3].enabled = false;
					skillChoice[2].enabled = true;
				}
			}
	
		if(Input.GetKey(KeyCode.X))
		{
			isChoice = false;
			skillUsed = true;
			choice[0].enabled = true;
			eraseRender();
		}

	}

	void eraseRender()
	{
		battleWindow.enabled = false;
		for(int i = 0; i<4;i++)
		{
			skills[i].enabled = false;
			skills[i].GetComponentInChildren<Text>().enabled = false;
			skillChoice[i].enabled = false;
		}
	}
	
	void battleEnd()
	{
		GameObject.Find("Player").GetComponent<Player>().battleEnd();
		if(isBattle == true)
		{
			isBattle = false;
			GameObject.Find ("PokemonMgr").GetComponent<PokemonMgr>().battleEnd();
		}
		else
		{
			playerBattle = false;
			GameObject.Find ("Trainer").GetComponent<Trainer>().battleEnd();
		}
		Application.LoadLevel(0);
	}

	void useSkill(Skill skill,Pokemon attacker,Pokemon defender)
	{
		float type = typePower(attacker,defender);
		int damage =(int)(( skill.power * attacker.getAtk() *(int)( attacker.getLevel() * 0.4 +2)/defender.getDef()/50+2) * type) ;
		defender.getDamage(damage);
		isInfo = false;
		Debug.Log (skill.name);
	}

	float typePower(Pokemon attacker,Pokemon defender)
	{
		if(attacker.getType() == Pokemon.Type.water)
		{
			if(defender.getType() == Pokemon.Type.fire)
			{
				return 1.3f;
			}
			else if(defender.getType() == Pokemon.Type.electric)
			{
				return 0.8f;
			}
			else if(defender.getType() == Pokemon.Type.grass)
			{
				return 1.0f;
			}
			else if(defender.getType() == Pokemon.Type.water)
			{
				return 1.0f;
			}
			return 1.0f;
		}
		else if(attacker.getType() == Pokemon.Type.electric)
		{
			if(defender.getType() == Pokemon.Type.fire)
			{
				return 0.8f;
			}
			else if(defender.getType() == Pokemon.Type.electric)
			{
				return 1.0f;
			}
			else if(defender.getType() == Pokemon.Type.grass)
			{
				return 1.0f;
			}
			else if(defender.getType() == Pokemon.Type.water)
			{
				return 1.3f;
			}
			return 1.0f;
		}
		else if(attacker.getType() == Pokemon.Type.grass)
		{
			if(defender.getType() == Pokemon.Type.fire)
			{
				return 0.8f;
			}
			else if(defender.getType() == Pokemon.Type.electric)
			{
				return 1.0f;
			}
			else if(defender.getType() == Pokemon.Type.grass)
			{
				return 1.0f;
			}
			else if(defender.getType() == Pokemon.Type.water)
			{
				return 1.0f;
			}
			return 1.0f;
		}
		else if(attacker.getType() == Pokemon.Type.fire)
		{
			if(defender.getType() == Pokemon.Type.fire)
			{
				return 1.0f;
			}
			else if(defender.getType() == Pokemon.Type.electric)
			{
				return 1.3f;
			}
			else if(defender.getType() == Pokemon.Type.grass)
			{
				return 1.3f;
			}
			else if(defender.getType() == Pokemon.Type.water)
			{
				return 0.8f;
			}
			return 1.0f;
		}
		return 1.0f;
	}
	
}
