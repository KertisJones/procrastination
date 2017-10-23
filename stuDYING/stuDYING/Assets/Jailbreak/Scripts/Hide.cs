using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour {

	//Sets player isHiding bool to true when they enter the area
	void OnTriggerEnter2D(Collider2D other) {
		//Checks if it is the player
		if (other.tag == "Player") {
			PlayerMovement player = other.gameObject.GetComponent<PlayerMovement> ();
			player.isHiding = true;
		}
	}

	//Sets player isHiding bool to false when they leave the area
	void OnTriggerExit2D(Collider2D other) {
		//Checks if it is the player
		if (other.tag == "Player") {
			PlayerMovement player = other.gameObject.GetComponent<PlayerMovement> ();
			player.isHiding = false;
		}
	}
}
