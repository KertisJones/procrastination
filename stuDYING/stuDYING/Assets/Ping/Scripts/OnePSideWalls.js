﻿#pragma strict
var ballIs : Transform;
var fixDisStupid = false;
//var Escape : KeyCode;

function OnTriggerEnter2D (hitInfo : Collider2D) {
	if(hitInfo.name == "Ball")
	{
		if (fixDisStupid == true) {
			OnePGameManager.reset ();
			yield WaitForSeconds (1);
			ballIs.position.x = 0;
			ballIs.position.y = 0;
			GetComponent.<Rigidbody2D>().velocity.x = 0;
			GetComponent.<Rigidbody2D>().velocity.y = 0;
		}
		else {
			fixDisStupid = true;
		}	
	}
}

//function OnKeyDown () {
//		if (Input.GetKeyDown (Escape)) {
//			//OnePGameManager.reset ();
//			ballIs.position.x = 0;
//			ballIs.position.y = 0;
//			rigidbody2D.velocity.x = 0;
//			rigidbody2D.velocity.y = 0;
//			
//			Application.LoadLevel("Menu");
//			yield WaitForSeconds (3);
//		}
//}