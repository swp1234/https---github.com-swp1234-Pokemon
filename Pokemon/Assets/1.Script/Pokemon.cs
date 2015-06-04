using UnityEngine;
using System.Collections;

public class Pokemon : MonoBehaviour {

	public string name;
	private int level;
	private int atk;
	private int def;
	private int speed;
	private int hp;
	public int speciesHP,speciesAtk,speciesDef,speciesSpeed;
	private int levelRange = 5;
	public int captureRate;
	private bool isDead;
	public  Skill[] skill;
	public Sprite[] sprite;
	public Type type;
	public bool isRevolution;

	public enum Type{
		water,
		fire,
		grass,
		electric
	}

	void awake()
	{
		level = Random.Range(levelRange,levelRange+5);
	}
	
	// Use this for initialization
	void Start () {
		setAbility(speciesHP,speciesAtk,speciesDef,speciesSpeed);
		isDead = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void setAbility(int a,int b, int c, int d)
	{
		this.hp = this.level * ( (a + 70 ) /50);
		this.atk = this.level * ( (b + 70 ) /50)+5;
		this.def = this.level * ( (c + 70 ) /50)+5;
		this.speed = this.level * ( (d + 70 ) /50)+ 5;
	}

}
