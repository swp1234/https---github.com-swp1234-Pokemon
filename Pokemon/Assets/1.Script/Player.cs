using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : Trainer,Tradeable {

	private int money;
	//public GameObject[] pokeball;
	//public GameObject[] potion;
	public GameObject Mgr;

	private Vector3 startPoint;
	private Vector3 endPoint;
	public float speed;
	private float increment;
	public bool isMoving;
	private float horizontalMove;
	private float verticalMove;
	public bool disableMove;
	
	private RaycastHit hit;
	public  bool isBattle;
	public bool playerBattle;
	
	private int battleCounter;
	private int walkCounter;
	
	private SpriteControl sprControl;
	public Camera mainCamera;
	public Camera shopCamera;
	public Camera centerCamera;
	//public Camera battleCamera;
	//public Canvas battleCanvas;
	private Canvas canvasClone;
	public int size;

	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
		this.gameObject.transform.position = new Vector3(PlayerPrefs.GetFloat("x",5.295f),PlayerPrefs.GetFloat("y",1.4392f),PlayerPrefs.GetFloat("z",-2.0f));
	}

	// Use this for initialization
	void Start () {
		shopCamera.enabled = false;
		centerCamera.enabled = false;
		money = 1000;	
		sprControl = this.gameObject.GetComponent<SpriteControl>();
		horizontalMove = 0.84f;
		verticalMove = 0.6096f;
		startPoint = transform.position;
		endPoint = transform.position;
		isBattle = false;
		playerBattle = false;
		battleCounter = Random.Range(5,16);
		//battleCamera.enabled = false;
		size = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if(!isBattle)
		{
			move();
		}
	
	}

	void move()
	{
		transport();
		if(increment <= 1 && isMoving == true)
		{
			increment += speed/100;
		}
		else
		{
			isMoving = false;
		}
		
		if(isMoving)
		{
			transform.position = Vector3.Lerp(startPoint,endPoint,increment);
		}
		else
		{
			sprControl.setTotalCells(1);
		}
		
		if(Input.GetKey(KeyCode.UpArrow) && isMoving == false)
		{
			if(Physics.Raycast(transform.position+ new Vector3(0,0.6f,0),Vector3.forward,out hit))
			{
				if(hit.collider.gameObject.tag == "tree" || hit.collider.gameObject.tag == "building"|| hit.collider.gameObject.tag  == "enemy")
				{
					disableMove = true;
				}
			}
			
			if(!disableMove)
			{
				sprControl.setRowNumber(3);
				sprControl.setTotalCells(4);
				increment = 0;
				isMoving = true;
				startPoint = transform.position;
				endPoint = new Vector3 (transform.position.x,transform.position.y+verticalMove,transform.position.z);
				calculateWalk();
			}
			disableMove = false;
		}
		else if(Input.GetKey(KeyCode.DownArrow) && isMoving == false)
		{
			if(Physics.Raycast(transform.position- new Vector3(0,0.6f,0),Vector3.forward,out hit))
			{
				if(hit.collider.gameObject.tag == "tree" || hit.collider.gameObject.tag == "building"|| hit.collider.gameObject.tag  == "enemy" )
				{
					disableMove = true;
				}
			}
	
			if(!disableMove)
			{
				sprControl.setRowNumber(4);
				sprControl.setTotalCells(4);
				increment = 0;
				isMoving = true;
				startPoint = transform.position;
				endPoint = new Vector3 (transform.position.x,transform.position.y-verticalMove,transform.position.z);	
				calculateWalk();
			}
			disableMove = false;
			
		}
		
		else if(Input.GetKey(KeyCode.LeftArrow) && isMoving == false)
		{
			
			if(Physics.Raycast(transform.position - new Vector3(0.6f,0,0),Vector3.forward,out hit))
			{
				if(hit.collider.gameObject.tag == "tree" || hit.collider.gameObject.tag == "building" || hit.collider.gameObject.tag  == "enemy")
				{
					disableMove = true;
				}
				
			}

			if(!disableMove)
			{
				sprControl.setRowNumber(1);
				sprControl.setTotalCells(4);
				increment = 0;
				isMoving = true;
				startPoint = transform.position;
				endPoint = new Vector3 (transform.position.x-horizontalMove,transform.position.y,transform.position.z);	
				calculateWalk();
			}
			disableMove = false;
		}
		else if(Input.GetKey(KeyCode.RightArrow) && isMoving == false)
		{
			
			if(Physics.Raycast(transform.position+ new Vector3(0.6f,0,0),Vector3.forward,out hit))
			{
				if(hit.collider.gameObject.tag == "tree" || hit.collider.gameObject.tag == "building"|| hit.collider.gameObject.tag  == "enemy")
				{
					disableMove = true;
				}
			}
			
			if(!disableMove)
			{
				sprControl.setRowNumber(2);
				sprControl.setTotalCells(4);
				increment = 0;
				isMoving = true;
				startPoint = transform.position;
				endPoint = new Vector3 (transform.position.x+horizontalMove,transform.position.y,transform.position.z);
				calculateWalk();
			}
			disableMove = false;
		}	

	}
	
	public void sell()
	{
	}

	public void buy()
	{
	}

	public void setMoney(int a)
	{
		money = a;
	}

	public int getMoney()
	{
		return this.money; 
	}

	public void escape()
	{
	}

	public void win()
	{
	}

	public void defeat()
	{
	}


	public override void change()
	{
	}

	void calculateWalk() {

		if(Physics.Raycast(transform.position,Vector3.forward,out hit))
		{
			if(hit.collider.gameObject.tag == "grass_bush")
			{
				walkCounter++;
			}
		}
		
		if(walkCounter == battleCounter)
		{
			isBattle = true;
			battle(0);
		}

	}

	public void battle(int a)
	{
		curPokemon = Instantiate(pokemon[0]);
		if( a ==1)
		{
			playerBattle = true;
		}
		else
		{
			Mgr.GetComponent<PokemonMgr>().generateEnemy();
			walkCounter = 0;
			battleCounter = Random.Range(3,10);
		}
		PlayerPrefs.SetFloat("x",this.transform.position.x);
		PlayerPrefs.SetFloat("y",this.transform.position.y);
		PlayerPrefs.SetFloat("z",this.transform.position.z);
		//mainCamera.enabled = false;
		//battleCamera.enabled = true;
		//canvasClone = Instantiate(battleCanvas);
		Application.LoadLevel(1);
	}

	public  void battleEnd()
	{
		curPokemon.saveAbility(curPokemon.name);
		//Destroy (canvasClone);
		//Destroy(curPokemon);
		Destroy(this.gameObject);
		playerBattle = false;
		//mainCamera.enabled = true;
		//battleCamera.enabled = false;

	}

	void transport()
	{
		if(playerTr.position == new  Vector3(2.775f,-2.828f,-2.0f))
		{
			mainCamera.GetComponent<SmoothFollow>().enabled =false;
			mainCamera.enabled = false;
			shopCamera.enabled = true;
			playerTr.position = new Vector3(-72.478f,46.277f,-2.0f);
		}

		if(playerTr.position == new  Vector3(9.495001f,-5.876f,-2.0f))
		{
			mainCamera.GetComponent<SmoothFollow>().enabled =false;
			mainCamera.enabled = false;
			centerCamera.enabled = true;
			playerTr.position = new Vector3(67.1f,32.58f,-2.0f);
		}

		if(playerTr.position.x >66.65f&&playerTr.position.x <= 68.0f && playerTr.position.y <32.58f)
		{
			playerTr.position = new  Vector3(9.495001f,-6.4856f,-2.0f);
			centerCamera.enabled = false;	
			mainCamera.enabled = true;
			mainCamera.GetComponent<SmoothFollow>().enabled =true;
		}

		if(playerTr.position.x <=-72.478f && playerTr.position.y <46.277f)
		{
			playerTr.position = new Vector3(2.775f,-3.4376f,-2.0f);
			shopCamera.enabled = false;
			mainCamera.enabled = true;
			mainCamera.GetComponent<SmoothFollow>().enabled =true;
		}
	}
	
}
