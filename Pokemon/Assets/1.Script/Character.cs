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
		if(GameObject.Find("Player").GetComponent<Player>().isBattle == false)
		{
			dialog();
		}
	}

	 public void dialog()
	{
		if(Mathf.Abs( pos.x - playerPos.x) <= 2.0f || Mathf.Abs(pos.y - playerPos.y) <= 1.3f)
		{
			if(Input.GetKey(KeyCode.Z))
			{
				centerCanvas.enabled = true;
			}

			if(Input.GetKey(KeyCode.X))
			   {
				centerCanvas.enabled = false;
			}
		}
	}
}
