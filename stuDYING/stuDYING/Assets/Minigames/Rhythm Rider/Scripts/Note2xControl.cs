using UnityEngine;
using System.Collections;

public class Note2xControl : MonoBehaviour {

	public Transform successBurst;
	public Transform failBurst;

	public int noteHP = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (noteHP < 1) {
			Destroy (gameObject);
			Debug.Log ("SUCCESS");
			Instantiate (successBurst, transform.position, successBurst.rotation);
			GameMasterRhythmRider.winStreak++;
			GameMasterRhythmRider.totalScore += 50;
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.name == "FailCollider") {
			Destroy (gameObject);
			Debug.Log ("FAIL");
			Instantiate (failBurst, transform.position, failBurst.rotation);
		}
	}

	void OnTriggerStay(Collider other){
		if (other.gameObject.tag == "Success" && !String1.releasedKey) {
			noteHP--;
		}
	}
}