using UnityEngine;
using System.Collections;

public class String1 : MonoBehaviour {

	public KeyCode activateString;

	public bool lockInput = false;

	public static bool releasedKey = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown (activateString) && !lockInput){
			lockInput = true;
			GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 2);
			StartCoroutine (retractCollider ());
			releasedKey = false;
		}

		if (Input.GetKeyUp (activateString)) {
			releasedKey = true;
		}
	}

	IEnumerator retractCollider(){
		yield return new WaitForSeconds (.75f);
		GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
		if (!releasedKey) {
			yield return new WaitForSeconds (1);
			StartCoroutine (releaseNote ());
		}
		if (releasedKey) {
			StartCoroutine (releaseNote ());
		}
	}

	IEnumerator releaseNote(){
		GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, -2);
		yield return new WaitForSeconds (.75f);
		GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
		lockInput = false;
	}
}
