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
	public GameObject panel;

	// Use this for initialization
	void Start () {
		tr = this.gameObject.GetComponent<Transform>();
		pos = tr.position;
		playerPos = playerTr.position;
	}
	
	// Update is called once per frame
	void Update () {
		dialog();
	}

	 public void dialog()
	{
		if(Mathf.Abs( pos.x - playerPos.x) <= 0.16 || Mathf.Abs(pos.y - playerPos.y) <= 0.16)
		{
			if(Input.GetKey(KeyCode.Z))
			{
				panel.SetActive(true);
			}

			if(Input.GetKey(KeyCode.X))
			   {
				panel.SetActive(false);
			}
		}
	}
}
