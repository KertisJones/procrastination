using UnityEngine;
using System.Collections;

public class MineExplosion : MonoBehaviour {

	Rigidbody2D myBody;
	Rigidbody2D otherBody;
	SpriteRenderer mySprite;
	ParticleSystem mySystem;
	CircleCollider2D myColl;
	BlackHoleObjectState stateScript;
	Vector3 forceDir;
	public float expStrength;
	public float torqueIncrement;
	public float maxAngVelocity;
	public bool explode = false;



	// Use this for initialization
	void Start () {
		mySystem = gameObject.GetComponent<ParticleSystem> ();
		mySystem.enableEmission = false;
		mySprite = transform.root.GetComponent<SpriteRenderer> ();
		myBody = transform.root.GetComponent<Rigidbody2D> ();
		myColl = gameObject.GetComponent<CircleCollider2D> ();
		stateScript = transform.root.GetComponent<BlackHoleObjectState> ();
		myBody.AddTorque (-250);
	}
	
	// Update is called once per frame
	void Update () {
		if (explode) {
			StartCoroutine (DeathWait ());
		}
	}

	void FixedUpdate(){
		if (myBody.angularVelocity <= -maxAngVelocity) {
			myBody.angularVelocity = -maxAngVelocity;
		} else if (stateScript.myState == "Held") {
			myBody.AddTorque(-torqueIncrement);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		
		if (!other.isTrigger && explode) {
			forceDir = other.transform.position - transform.position;
			if (other.gameObject.layer != 8){
				otherBody = other.GetComponent<Rigidbody2D> ();
			} else {
				otherBody = other.transform.root.GetComponentInChildren<Rigidbody2D> ();
			}
			if(PhaseNavigator.battlePhase && other.GetComponent<HullHealth>() != null)
			{	other.GetComponent<HullHealth> ().DoDamage (2);
            }
			if (otherBody != null) {
				otherBody.AddForceAtPosition (forceDir.normalized * expStrength, transform.position, ForceMode2D.Impulse);
			}
		}
	}

	IEnumerator DeathWait(){
		gameObject.GetComponent<Collider2D> ().enabled = true;
		mySystem.enableEmission = true;
		mySprite.enabled = false;
		yield return new WaitForSeconds (0.15f);
		Destroy (transform.parent.gameObject);
	}
}
