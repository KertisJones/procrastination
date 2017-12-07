using UnityEngine;
using System.Collections;

public class BlackHoleObjectState : MonoBehaviour {

	Rigidbody2D myBody;
	public string myState;
	float shotSpeedIdle;
	float lastVelocity;
	bool spawnScale;
	float currentTimer01;

	// Use this for initialization
	void Start () {
		myBody = gameObject.GetComponent<Rigidbody2D> ();
		myState = "Idle";
		shotSpeedIdle = 5.0f;
		spawnScale = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (myState == "Shot") {
			if (lastVelocity > myBody.velocity.magnitude && myBody.velocity.magnitude < shotSpeedIdle){
				myState = "Idle";
			}
			lastVelocity = myBody.velocity.magnitude;
		}
	}

	void Update(){
		float duration = 2.0f;
		if (PhaseNavigator.collectionPhase && spawnScale && currentTimer01 < duration) {
			currentTimer01 += Time.unscaledDeltaTime;
			transform.localScale = Vector3.Lerp (Vector3.zero, Vector3.one, Mathf.SmoothStep (0.0f, 1.0f, currentTimer01 / duration));
		} else {
			spawnScale = false;
		}
	}
}
