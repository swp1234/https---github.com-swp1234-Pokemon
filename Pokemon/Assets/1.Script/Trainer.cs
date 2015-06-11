	using UnityEngine;
using System.Collections;

public class Trainer : MonoBehaviour,Changeable {

	public Transform playerTr;
	private Transform tr;

	private Vector3 pos;
	private Vector3 playerPos;

	//private int stageNum;
	public int order;

	// 전투유무 판단 
	public bool isFight;
	public bool isFought;
	//포켓몬 
	public Pokemon[] pokemon;
	public Pokemon curPokemon;
	private int curNo;
	private int stageNo;
	public float a;
	// Use this for initialization
	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}

	void Start () {
		curNo = 1;
		tr = this.gameObject.GetComponent<Transform>();
		pos = tr.position;
		playerPos = playerTr.position;
	}
	
	// Update is called once per frame
	void Update () {
		pos = tr.position;
		a = Vector3.Distance(pos,playerPos);
		stageNo = PlayerPrefs.GetInt("stageNo");
		if(PlayerPrefs.GetInt("Trainer"+order+stageNo,0) != 1)
			isFought = false;
		else
			isFought = true;
		
		playerTr = GameObject.Find("Player").GetComponent<Transform>();
		playerPos = playerTr.position;
		if(Vector3.Distance(pos,playerPos) <= 1.35f)
		{
			if(Input.GetKey(KeyCode.Z) && isFought == false && isFight == false)
			{
				isFight  = true; 
				battle();
			}
		}

	}

	public void battle()
	{
		curPokemon = Instantiate(pokemon[curNo-1]);
		GameObject[] s = GameObject.FindGameObjectsWithTag("enemy");
		foreach(GameObject c in s)
		{
			if(c.GetComponent<Trainer>().isFight != true)
			{
				Destroy(c);
			}
		}
		GameObject.Find("Player").GetComponent<Player>().battle(1);
	}

	public void battleEnd()
	{
		PlayerPrefs.SetInt("Trainer"+order+stageNo,1);
		Destroy(GameObject.Find ("PokemonMgr"));
		Destroy(this.gameObject);
	}

	public virtual bool change()
	{
		if( (++curNo) <=pokemon.Length )
		{	
			return true;
		}
		else
			return false;
	}
}
