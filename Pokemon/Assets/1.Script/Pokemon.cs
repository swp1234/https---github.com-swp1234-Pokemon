using UnityEngine;
using System.Collections;

public class Pokemon : MonoBehaviour {

	private string name;
	public int level;
	private int atk;
	private int def;
	private int speed;
	private int hp;
	private int maxHp;
	public int speciesHP,speciesAtk,speciesDef,speciesSpeed;
	private int levelRange;
	public int captureRate;
	private bool isDead;
	public  Skill[] skill;
	public Sprite[] sprite;
	public Type type;
	public bool isRevolution;
	private int stageNo;
	public Sprite curSprite;
	public bool domesticated;
	private int exp;
	private int size;
	//public bool isLevelUp;

	public enum Type{
		water,
		fire,
		grass,
		electric
	}

	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}
	// Use this for initialization
	void Start () {
		name = this.gameObject.name;
		domesticated = false;

		if (GameObject.Find ("Player").GetComponent<Player>().curPokemon.name == this.gameObject.name)
		{
			domesticated = true;
		}
		stageNo = PlayerPrefs.GetInt("stageNo",1);
		if(stageNo == 1)
		{
			levelRange = 5;
		}
		else if(stageNo == 2)
		{
			levelRange = 15;
		}
		else{
			levelRange = 25;
		}
		level = Random.Range(levelRange,2*levelRange);

		if(domesticated == true)
		{
			setAbility(name);
			setAbility(speciesAtk,speciesDef,speciesSpeed);
		}
		else{
			setAbility(speciesHP, speciesAtk,speciesDef,speciesSpeed);
		}
		size = GameObject.Find("Player").GetComponent<Player>().size;
		skillInit(stageNo);
		renderCheck();
		isDead = false;
	}

	void Update()
	{
		if(domesticated)
		{
			Debug.Log (this.level);
		}
	}
	
	// Update is called once per frame

	void skillInit(int a)
	{

		skill[0].isUse = true;
		skill[1].isUse = true;
		skill[2].isUse = false;
		skill[3].isUse = false;

		for(int stageNo = 1; stageNo<=a;stageNo++)
		{
			skill[stageNo].isUse = true;
		}

	}

	void setAbility(int b, int c, int d)
	{
	//	maxHp =  this.level * ( (speciesHP + 70 ) /20)+35;
		this.atk = this.level * ( (b + 70 ) /50)+5;
		this.def = this.level * ( (c + 70 ) /50)+5;
		this.speed = this.level * ( (d + 70 ) /50)+ 5;
	}

	void setAbility(int a,int b, int c, int d)
	{
		this.hp= maxHp = this.level * ( (a + 70 ) /20)+5;
		this.atk = this.level * ( (b + 70 ) /50)+5;
		this.def = this.level * ( (c + 70 ) /50)+5;
		this.speed = this.level * ( (d + 70 ) /50)+ 5;
	}

	void renderCheck()
	{
		curSprite = this.sprite[0];

		if(this.isRevolution == true)
		{
			if(this.level >= 15)
			{
				curSprite= this.sprite[1];
			}

			if(this.level >= 30)
			{
				if(this.sprite[2] != null)
				{
					curSprite = this.sprite[2];
				}
				else
				{
					curSprite = this.sprite[1];
				}
			}
		}
	}

	public int getHP()
	{
		return this.hp;
	}

	public int getMaxHP()
	{
		return this.maxHp;
	}
		
	public int getLevel()
	{
		return this.level;
	}

	public int getExp()
	{
		return exp;
	}
	
	public  void leveUp(string prefname)
	{
		this.level+=1;
		PlayerPrefs.SetInt(prefname+"level",this.level);
	}

	public void getExp(string prefName)
	{
		exp = PlayerPrefs.GetInt(prefName+"exp",0);
	}

	public void setExp(string prefName,int p)
	{
		this.exp = p;
		 PlayerPrefs.SetInt(prefName+"exp",p);
	}

	private void getLevel(string prefName)
	{
		this.level = PlayerPrefs.GetInt(prefName+"level",Random.Range(levelRange,2*levelRange));
	}


	public void getHp(string prefName)
	{
		this.hp = PlayerPrefs.GetInt(prefName+"hp",(this.level * ( (speciesHP + 70 ) /20)) + 35);
		this.maxHp = PlayerPrefs.GetInt(prefName+"maxHp",(this.level * ( (speciesHP + 70 ) /20)) + 35);
	}


	public void setAbility(string a)
	{
		getExp(a);
		getLevel(a);
		getHp(a);
	}
	public void getDamage(int a)
	{
		this.hp -=a;
	}

	public void saveAbility(string prefName)
	{
		PlayerPrefs.SetInt(prefName+"maxHp",this.maxHp);
		PlayerPrefs.SetInt(prefName+"level",this.level);
		PlayerPrefs.SetInt(prefName+"exp",this.exp);
		PlayerPrefs.SetInt(prefName+"hp",this.hp);
	}

	public int getAtk()
	{
		return this.atk;
	}
	public int getDef()
	{
		return this.def;
	}

	public Type getType()
	{
		return this.type;
	}

	public void expPlus(int a )
	{
		this.exp +=a;
	}

}
