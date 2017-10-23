#pragma strict

var Escape : KeyCode;

function Update () 
{	
	if (Input.GetKeyDown (Escape)) {
		Application.Quit();
	}
}
