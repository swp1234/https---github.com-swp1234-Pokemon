#pragma strict

var colCount : int = 4;
var rowCount: int = 4;

var rowNumber : int = 0;
var colNumber  : int = 0;
var totalCells : int = 4;
var fps : int = 10;
var offset: Vector2;

function Update () { setSpriteAnimation(colCount, rowCount, rowNumber,colNumber,totalCells,fps);}

function setSpriteAnimation(colCount : int, rowCount : int, rowNumber : int , colNumber : int, totalCells: int, fps: int)
{

 var index : int = Time.time * fps;
 index = index % totalCells;
 var size = Vector2 (1.0 / colCount, 1.0/rowCount);
 var uIndex = index % colCount;
 var vIndex = index / colCount;
  offset = Vector2( (uIndex + colNumber) * size.x, (1.0 - size.y) - (vIndex+rowNumber ) * size.y);
 GetComponent.<Renderer>().material.SetTextureOffset("_MainTex",offset);
 GetComponent.<Renderer>().material.SetTextureScale("_MainTex",size);

}