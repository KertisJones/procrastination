using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GravGunPull : MonoBehaviour {

	public float pushPower;
	public float startPushPower;
	public float maxPushPower;
	public GameObject heldObject;
	Rigidbody2D heldBody;
	Animator myAnim;
	ParticleSystem gravField;
	BlackHoleObjectState stateScript;
	public InAudioNode gravShoot;
    public InAudioNode gravCharge;
	public InAudioNode gravGrab;
	private Vector3 pushForceDir;
	public bool atMaxPower;
	public bool collide;
	float currentTime01;
	float currentTime02;
	public float overChargeTime;
	public float chargeTime;

	// Use this for initialization
	void Start () {
		myAnim = transform.parent.GetComponentInChildren<Animator> ();
		gravField = gameObject.GetComponentInChildren<ParticleSystem> ();
    }
	
	// Update is called once per frame
	void Update () {

        //this is the shoot
        if (heldObject != null) {
			heldObject.transform.localPosition = Vector3.up * 3;
			pushForceDir = heldObject.transform.position - transform.position;
            if ((transform.root.tag == "Player01" && ControlManager.P1RTrig) || (transform.root.tag == "Player02" && ControlManager.P2RTrig))
            {
				if (pushPower == 0.0f)
				{
					myAnim.SetTrigger("Charge");
					InAudio.Play(gameObject, gravCharge, null);
				}

				if (currentTime01 < chargeTime) {
					currentTime01 += Time.unscaledDeltaTime;
					float timeRatio = Mathf.InverseLerp (0, chargeTime, currentTime01);
					pushPower = Mathf.Lerp (startPushPower, maxPushPower, timeRatio);
				} else if (currentTime02 < overChargeTime) {
					atMaxPower = true;
					currentTime02 += Time.unscaledDeltaTime;
				} else {
					ShootGravGun();
				}

            }

            else if (pushPower > startPushPower)
            {
				ShootGravGun();
            }
		}
	}

	public void ShootGravGun(){
		if (heldObject != null) {
			atMaxPower = false;
			currentTime01 = 0.0f;
			currentTime02 = 0.0f;
			myAnim.SetTrigger ("Idle");
			InAudio.Stop (gameObject, gravCharge);
			InAudio.Play (gameObject, gravShoot, null);
			heldObject.transform.SetParent (null);
			heldObject.GetComponent<Rigidbody2D> ().AddForce (pushForceDir.normalized * pushPower, ForceMode2D.Impulse);
			stateScript.myState = "Shot";
			heldObject = null;
			pushPower = 0.0f;
		}
	}

    //this is the grab
	void OnTriggerStay2D(Collider2D other){
		if (other.isTrigger != true && heldObject == null) {
			stateScript = other.GetComponent<BlackHoleObjectState> ();
			if (stateScript.myState == "Idle" || stateScript.myState == "Shot") {
				collide = true;
				if ((transform.root.tag == "Player01" && ControlManager.P1LTrig)
					|| (transform.root.tag == "Player02" && ControlManager.P2LTrig)) {
					myAnim.SetTrigger ("Grab");
					InAudio.Play(gameObject, gravGrab, null);
					other.transform.SetParent (gameObject.transform);
					other.transform.localPosition = Vector3.up * 3;
					heldObject = other.gameObject;
					heldBody = heldObject.GetComponent<Rigidbody2D> ();
					stateScript.myState = "Held";
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D other){
		collide = false;
		if (other.gameObject == heldObject) {
			heldObject = null;
		}
	}
}
