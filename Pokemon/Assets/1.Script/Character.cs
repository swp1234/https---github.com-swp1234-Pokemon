using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Character : MonoBehaviour {

	// 객체들 위치 
	private Transform tr;
	public Transform playerTr;
	private Vector3 pos;
	private Vector3 playerPos;

	// 각 기능 
	public Canvas centerCanvas;

	// Use this for initialization
	void Start () {
		centerCanvas.enabled = false;
		tr = this.gameObject.GetComponent<Transform>();
		pos = tr.position;
		playerPos = playerTr.position;
	}
	
	// Update is called once per frame
	void Update () {
		playerPos = playerTr.position;
		if(GameObject.Find("Player").GetComponent<Player>().isBattle == false &&( pos.x - playerPos.x)<= -0.055f && (pos.x-playerPos.x) >= -0.56f && (pos.y - playerPos.y) <=1.0f && (pos.y-playerPos.y) >=0.98f)
		{
			dialog();
		}
	}

	 public void dialog()
	{
		if(centerCanvas.enabled == true)
		{
			if(Input.GetKey(KeyCode.Space))
			{
				for(int i = 0; i<GameObject.Find("Player").GetComponent<Player>().size;i++)
				{
					if(GameObject.Find("Player").GetComponent<Player>().pokemon[i] !=null)
					{
						string s = GameObject.Find("Player").GetComponent<Player>().pokemon[i].name;
						PlayerPrefs.SetInt(s+"(Clone)hp",PlayerPrefs.GetInt(s+"(Clone)maxHp"));
						centerCanvas.enabled = false;
					}
				}
			}
			if(Input.GetKey(KeyCode.X))
			{
				centerCanvas.enabled = false;
			}
		}
		else 
		{
			if(Input.GetKey(KeyCode.Z))
			{
				centerCanvas.enabled = true;
			}
		}
	}
}
