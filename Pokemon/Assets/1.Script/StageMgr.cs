using UnityEngine;
using System.Collections;

public class StageMgr : MonoBehaviour {

	public int stageNo;
	public GameObject boss;
	public Transform playerTr;
	private Vector3 pos;
	public bool isInit;
	// Use this for initialization
	void Awake()
	{
		stageNo = PlayerPrefs.GetInt("stageNo",1);
		isInit = false;
	}

	void Start () {
		pos = playerTr.position;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("isinit : " + isInit);
		if(boss.GetComponent<Trainer>().isFought == true)
		{
			if(!isInit)
				playerInit();
		//	GameObject.Find("Player").GetComponent<Player>().curPokemon.saveAbility();

		}
	}
	void playerInit()
	{
		playerTr.position = new Vector3(5.295f,1.4392f,-2.0f);
		isInit = true;
		stageNo+=1;
		PlayerPrefs.SetInt("stageNo",stageNo);
	}
}
