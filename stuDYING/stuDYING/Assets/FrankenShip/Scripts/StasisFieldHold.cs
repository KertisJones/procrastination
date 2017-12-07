using UnityEngine;
using System.Collections;

public class StasisFieldHold : MonoBehaviour {

	public GameObject heldObject;
	private float minObjVelocity;
	private float myCollRadius;
	public Vector2 forceDir;
	public float forceStrength;
	public InAudioNode stasisCollect;
	public float pullStrength;
	Animator faceAnim;


	// Use this for initialization
	void Start () {
		myCollRadius = gameObject.GetComponent<CircleCollider2D> ().radius;
		minObjVelocity = 5.0f;
        forceStrength = 800.0f;
		faceAnim = transform.root.GetComponentInChildren<Animator> ();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay2D(Collider2D other){
		if (!other.isTrigger && other.GetComponent<BlackHoleObjectState> ().myState != "Held") {
			if (heldObject == null && other.GetComponent<Rigidbody2D>().velocity.magnitude < minObjVelocity) {
				other.GetComponent<BlackHoleObjectState> ().myState = "Stasis";
				heldObject = other.gameObject;
                forceStrength = 500.0f;
				heldObject.transform.SetParent(transform);
				InAudio.Play(gameObject, stasisCollect, null);
				faceAnim.SetTrigger ("Angry");
				StartCoroutine (WaitTrigIdle ());
			}
			forceDir = other.transform.position - transform.position;
			float lerpDist = Mathf.InverseLerp (0.0f, myCollRadius, forceDir.magnitude);
			Vector2 pull = forceDir.normalized * -forceStrength * lerpDist;
			pullStrength = pull.magnitude;
			other.GetComponent<Rigidbody2D>().AddForce(pull);
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (!other.isTrigger && other.gameObject == heldObject) {
			if (other.GetComponent<BlackHoleObjectState> ().myState != "Held") {
				other.GetComponent<BlackHoleObjectState> ().myState = "Idle";
				heldObject.transform.SetParent (null);
			}
			heldObject = null;
		}
	}

	IEnumerator WaitTrigIdle(){
		yield return new WaitForSeconds (2);
		faceAnim.SetTrigger ("Still");
	}
}
