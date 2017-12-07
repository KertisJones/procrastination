using UnityEngine;
using System.Collections;

public class SingularityPull : MonoBehaviour {

	CircleCollider2D pullColl;
	float distance;
	float newScale;
	Vector2 pullDir;
	public float deathDistance;

	// Use this for initialization
	void Start () {
		pullColl = gameObject.GetComponent<CircleCollider2D> ();
		deathDistance = 0.25f;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D (Collider2D other){
		if (!other.isTrigger && other.GetComponent<BlackHoleObjectState>().myState != "Held" && other.GetComponent<BlackHoleObjectState>().myState != "Stasis") {
			other.transform.root.GetComponent<BlackHoleObjectState> ().myState = "Dead";
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if (!other.isTrigger && other.GetComponent<BlackHoleObjectState>().myState != "Held") {
			distance = Vector2.Distance (transform.position, other.transform.position);
			newScale = Mathf.InverseLerp (1, pullColl.radius, distance); 
			other.transform.localScale = new Vector3 (newScale, newScale, 1);
			pullDir = transform.position - other.transform.position;
			other.GetComponent<Rigidbody2D> ().AddForce (pullDir * 2.5f);
			if (other.transform.localScale.x < deathDistance) {
				if (other.GetComponent<BlackHoleObjectState> ().myState == "Stasis") {
					other.gameObject.SetActive (false);
				} else {
					Destroy (other.gameObject);
				}
			}
		}
	}

	void OnTriggerExit2D (Collider2D other){
		if (!other.isTrigger && other.GetComponent<BlackHoleObjectState>().myState != "Held" && other.GetComponent<BlackHoleObjectState>().myState != "Stasis") {
			other.GetComponent<BlackHoleObjectState> ().myState = "Idle";
		}
	}
}
