using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing : MonoBehaviour {

	//Public variables
	public float moveSpeed;
	public GameObject player;
	public GameObject home;
	public GameObject parent;

	//Private variables
	PlayerMovement playerScript;
	Vector3 target;

	void Start() {
		playerScript = player.GetComponent<PlayerMovement> ();
	}

	// Update is called once per frame
	void Update () {
		//Sets target based on player status
		if (playerScript.isHiding) {
			target = home.transform.localPosition;
		} else {
			target = player.transform.position;
			target = this.parent.transform.InverseTransformPoint (target);
		}

		Vector3 newPos = Vector3.MoveTowards (this.transform.localPosition, target, moveSpeed * Time.deltaTime);
		newPos = new Vector3 (newPos.x, newPos.y, 5);
		this.transform.localPosition = newPos;
	}
}
