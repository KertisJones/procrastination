using UnityEngine;
using System.Collections;

public class BoundaryFlip : MonoBehaviour {

	public Vector3 offsetDir;

	// Use this for initialization
	void Start () {
		offsetDir = transform.position -  GameObject.FindWithTag ("BlackHole").transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other){
		if (!other.isTrigger) {
			if (other.gameObject.layer != 8 && other.GetComponent<BlackHoleObjectState> ().myState == "Held") {

			} else {
				other.transform.root.transform.position = new Vector3 (other.transform.root.transform.position.x, 
					other.transform.root.transform.position.y * -1 + offsetDir.normalized.y * 2, 
					other.transform.root.transform.position.z);
			}
		}
	}
}
