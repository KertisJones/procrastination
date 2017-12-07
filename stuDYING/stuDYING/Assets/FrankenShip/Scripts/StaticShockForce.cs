using UnityEngine;
using System.Collections;

public class StaticShockForce : MonoBehaviour {

	public Rigidbody2D otherBody;
	public Vector2 otherVelocity;
	public float shockStrength;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other){
		if (!other.isTrigger) {
			if (other.gameObject.layer != 8 && other.GetComponent<BlackHoleObjectState> ().myState == "Held") {
				print ("ASDSA");
			} else {
				otherBody = other.transform.root.GetComponent<Rigidbody2D> ();
				otherVelocity = otherBody.velocity;
			}

		}
	}

	void OnTriggerStay2D (Collider2D other) {
		if (otherBody != null) {
			otherBody.AddForce (otherVelocity * -shockStrength);
		}
	}

	void OnTriggerExit2D (Collider2D other){
		otherBody = null;
	}
}
