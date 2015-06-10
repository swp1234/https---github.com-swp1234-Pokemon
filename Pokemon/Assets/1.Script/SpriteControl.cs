using UnityEngine;
using System.Collections;

public class SpriteControl : MonoBehaviour {

	private int rowCount = 4;
	private int colCount = 4;
	
	private int rowNumber = 0;
	private int colNumber = 0;
	private  int totalCells = 4;
	private int fps = 10;
	private Vector2 offset;
	

	// Update is called once per frame
	void Update () {
		setSpriteAnimation(colCount, rowCount, rowNumber,colNumber,totalCells,fps);
	}

	void setSpriteAnimation(int colCount, int rowCount, int rowNumber , int colNumber, int totalCells, int fps)
	{
		
		int index  = (int)(Time.time * fps);
		index = index % totalCells;
		Vector2 size = new Vector2 ( 1.0f / colCount,1.0f/rowCount);
		int uIndex = index % colCount;
		int vIndex = index / colCount;
		offset =new Vector2( (uIndex + colNumber) * size.x, (1.0f - size.y) - (vIndex+rowNumber ) * size.y);
		this.gameObject.GetComponent<MeshRenderer>().material.SetTextureOffset("_MainTex",offset);
		this.gameObject.GetComponent<MeshRenderer>().material.SetTextureScale("_MainTex",size);		
	}

	public  void setRowNumber(int n)
	{
		this.rowNumber = n;
	}

   public  void setTotalCells(int n)
	{
		this.totalCells = n;
	}
}
