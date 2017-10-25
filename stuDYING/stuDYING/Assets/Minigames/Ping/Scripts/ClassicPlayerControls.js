#pragma strict

var moveUp : KeyCode;
var moveDown : KeyCode;
var Escape : KeyCode;
var speed : float = 20;

function Update () 
{
	if (Input.GetKey(moveUp))
	{
		GetComponent.<Rigidbody2D>().velocity.y = speed;
	}
	else if (Input.GetKey(moveDown))
	{
		GetComponent.<Rigidbody2D>().velocity.y = speed * -1;
	}
	else
	{
		GetComponent.<Rigidbody2D>().velocity.y = 0;
	}
	
	//rigidbody2D.velocity.x = 0;
	
	if (Input.GetKeyDown (Escape)) {
		ClassicGameManager.playerScore01 = 0;
		ClassicGameManager.playerScore02 = 0;
		Application.LoadLevel("PingMenu");
	}
}