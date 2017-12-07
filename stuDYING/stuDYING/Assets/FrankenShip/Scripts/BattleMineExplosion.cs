using UnityEngine;
using System.Collections;

public class BattleMineExplosion : MonoBehaviour {

	public int myDamage;
	public float explosionStrength;
	Vector3 forceDir;
	CircleCollider2D myColl;

	// Use this for initialization
	void Start () {
		myColl = gameObject.GetComponent<CircleCollider2D> ();
		myColl.enabled = true;
		StartCoroutine (DeathWait ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (!other.isTrigger && other.gameObject.layer == 8) {
			forceDir = other.transform.position - transform.position;
			other.GetComponent<HullHealth> ().DoDamage (myDamage);
			other.transform.root.GetComponent<Rigidbody2D> ().AddForceAtPosition (forceDir.normalized * explosionStrength, transform.position, ForceMode2D.Impulse);
			gameObject.GetComponent<ParticleSystem> ().enableEmission = true;
		}
	}

	IEnumerator DeathWait(){
		yield return new WaitForSeconds (0.25f);
		Destroy (transform.parent.gameObject);
	}
}
