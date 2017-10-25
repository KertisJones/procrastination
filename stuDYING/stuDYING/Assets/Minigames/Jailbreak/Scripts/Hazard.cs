using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hazard : MonoBehaviour {

	//Checks if collider is player, resets level unless player is hiding
	void OnTriggerStay2D(Collider2D other) {
		//Checks the tag of the collider
		if (other.tag == "Player") {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
	}
}