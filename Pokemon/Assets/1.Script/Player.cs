using UnityEngine;
using System.Collections;

public class Player : Trainer,Tradeable {

	private int money;
	public GameObject[] pokeball;
	public GameObject[] potion;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void battle()
	{
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
}
