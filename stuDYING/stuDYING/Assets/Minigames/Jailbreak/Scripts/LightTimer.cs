using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LightTimer : MonoBehaviour {

	//Public variable for spotlight child object, and on/off timer values
	public GameObject lightObject;
	public float onTime;
	public float offTime;

	//Private variable for current state and time
	float curTime;
	bool isOn;
	Light spotLight;

	void Start() {
		curTime = onTime;
		isOn = true;
		spotLight = lightObject.GetComponent<Light> ();
	}

	//Checks if collider is player, raycasts towards them, and resets level if raycast hits player
	void OnTriggerStay2D(Collider2D other) {
		//Checks the tag of the collider
		if (isOn) {
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

	//Updates timer and changes light state if needed
	void Update() {
		if (curTime <= 0) {
			if (isOn) {
				isOn = false;
				spotLight.enabled = false;
				curTime = offTime;
			} else {
				isOn = true;
				spotLight.enabled = true;
				curTime = onTime;
			}
		} else {
			curTime = curTime - Time.deltaTime;
		}
	}
}