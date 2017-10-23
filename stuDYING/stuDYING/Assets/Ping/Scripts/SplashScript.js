#pragma strict

function Start () {
	yield WaitForSeconds (3);
	Application.LoadLevel("Menu");
}