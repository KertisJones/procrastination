using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpotlightDetector : MonoBehaviour {

	//Public variable for spotlight child object
	public GameObject lightObject;

	//Checks if collider is player, resets level unless player is hiding
	void OnTriggerStay2D(Collider2D other) {
		//Checks the tag of the collider
		if (other.tag == "Player") {
			//Gets movement script
			PlayerMovement player = other.gameObject.GetComponent<PlayerMovement> ();
			//Resets scene if player is not hiding
			if (!player.isHiding) {
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			}
		}
	}
}
