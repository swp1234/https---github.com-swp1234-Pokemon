using UnityEngine;
using System.Collections;

public class Trainer : MonoBehaviour,Changeable {

	public Transform playerTr;
	private Transform tr;

	private Vector3 pos;
	private Vector3 playerPos;

	// 전투유무 판단 
	private bool isBattle;
	//포켓몬 
	public GameObject[] pokemon;

	// Use this for initialization
	void Start () {
		tr = this.gameObject.GetComponent<Transform>();
		pos = tr.position;
		playerPos = playerTr.position;
		isBattle = false;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Mathf.Abs( pos.x - playerPos.x) <= 0.16 || Mathf.Abs(pos.y - playerPos.y) <= 0.16)
		{
			if(Input.GetKey(KeyCode.Z) && isBattle == false)
			{
				battle();
			}
		}
	}

	void battle()
	{




		isBattle  = true; 
	}

	public virtual void change()
	{
	}
}
