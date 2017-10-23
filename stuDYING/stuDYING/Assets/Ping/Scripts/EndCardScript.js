#pragma strict

function Start () {
	yield WaitForSeconds (4);
	Application.LoadLevel("PingMenu");
}