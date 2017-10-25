using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotarySpotlight : MonoBehaviour {

	//Public variables
	public float moveSpeed;
	public float moveTime;
	public GameObject home;

	//Private variables
	private float time;

	// Update is called once per frame
	void Update () {
		//If time is up, resets spotlight, otherwise moves it to the right
		if (time <= 0) {
			this.transform.localPosition = this.home.transform.localPosition;
			time = moveTime;
		} else {
			Vector3 newPos = this.transform.localPosition;
			newPos.x = newPos.x + (moveSpeed * Time.deltaTime);
			this.transform.localPosition = newPos;
			time = time - Time.deltaTime;
		}
	}
}
