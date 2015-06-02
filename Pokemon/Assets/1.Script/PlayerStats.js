#pragma strict

var startPoint : Vector3;
var endPoint  : Vector3;
var speed: float;
private var increment : float;
var isMoving : boolean;

function Start () {
startPoint = transform.position;
endPoint = transform.position;

}

function Update () {
var Sprite = gameObject.GetComponent(AnimateSprite); 
 
if(increment <= 1 && isMoving == true)
{
increment += speed/100;
Debug.Log("Moving");
}
else{
isMoving = false;
Debug.Log("Stopped");
}

if(isMoving)
{transform.position = Vector3.Lerp(startPoint,endPoint,increment);
}else{
Sprite.totalCells = 1;
}

if(Input.GetKey(KeyCode.UpArrow) && isMoving == false)
{
Sprite.rowNumber = 3;
Sprite.totalCells = 4;
increment = 0;
isMoving = true;
startPoint = transform.position;
endPoint = new Vector3 (transform.position.x,transform.position.y+0.64,transform.position.z);
}

if(Input.GetKey(KeyCode.DownArrow) && isMoving == false)
{
Sprite.rowNumber = 4;
Sprite.totalCells = 4;
increment = 0;
isMoving = true;
startPoint = transform.position;
endPoint = new Vector3 (transform.position.x,transform.position.y-0.64,transform.position.z);
}
if(Input.GetKey(KeyCode.LeftArrow) && isMoving == false)
{
Sprite.rowNumber = 1;
Sprite.totalCells = 4;
increment = 0;
isMoving = true;
startPoint = transform.position;
endPoint = new Vector3 (transform.position.x-0.64,transform.position.y,transform.position.z);
}

if(Input.GetKey(KeyCode.RightArrow) && isMoving == false)
{
Sprite.rowNumber = 2;
Sprite.totalCells = 4;
increment = 0;
isMoving = true;
startPoint = transform.position;
endPoint = new Vector3 (transform.position.x+0.64,transform.position.y,transform.position.z);
}



}