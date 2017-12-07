using UnityEngine;
using System.Collections;

public class PlasmaShock : MonoBehaviour {

	public bool shock;
	HullHealth otherHealth;
	bool timerStarted;
	public float shockDelay;
	public int shockDamage;
	ParticleSystem shockParticle;

	public InAudioNode plasmaShockFx;

	// Use this for initialization
	void Start () {
		shockParticle = gameObject.GetComponentInChildren<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (PhaseNavigator.battlePhase && shock && !timerStarted) {
			otherHealth.DoDamage (shockDamage);
			InAudio.Play (gameObject, plasmaShockFx, null);
			StartCoroutine (ShockWait ());
		}
	}

	void OnTriggerStay2D (Collider2D other){
		if (!other.isTrigger && other.GetComponent<HullHealth> () != null && other.transform.root != transform.root) {
			otherHealth = other.GetComponent<HullHealth> ();
			shock = true;
			shockParticle.enableEmission = true;
			Vector3 dir = transform.position - other.transform.position;
			var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			shockParticle.transform.rotation = Quaternion.AngleAxis(angle + 158, Vector3.forward);
		}
	}

	void OnTriggerExit2D (Collider2D other){
		shockParticle.enableEmission = false;
	}

	IEnumerator ShockWait(){
		timerStarted = true;
		yield return new WaitForSeconds (shockDelay);
		timerStarted = false;
		shock = false;
	}
}
