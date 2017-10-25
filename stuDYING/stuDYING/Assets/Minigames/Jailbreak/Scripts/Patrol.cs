using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour {

	//Public variables
	public float moveSpeed;
	public GameObject startPoint;
	public GameObject toPoint;

	//Private variables
	Vector3 target;

	void Start() {
		target = toPoint.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPos = Vector3.MoveTowards (this.transform.localPosition, target, moveSpeed * Time.deltaTime);

		if (newPos == target) {
			if (target == toPoint.transform.localPosition) {
				target = startPoint.transform.localPosition;
			} else {
				target = toPoint.transform.localPosition;
			}
		} else {
			this.transform.localPosition = newPos;
		}
	}
}
