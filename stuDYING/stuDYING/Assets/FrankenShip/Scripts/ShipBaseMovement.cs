using UnityEngine;
using System.Collections;
using System.Linq;

public class ShipBaseMovement : MonoBehaviour {

	public float vertAxe;
	float hoAxe;
	Rigidbody2D shipBody;
	Vector2 linearForce;
	public float thrustPower;
	public float torquePower;
	public float MaxLinVelocity;
	public float MaxAngVelocity;
	public bool MaxLin;
	public bool MaxAng;
	public bool thrusterOn;
	Animator igorAnim;
	float lastVert;
	GravGunPull gravScript;
	float analogDeadzone = 0.15f;

	// Use this for initialization
	void Start () {
		shipBody = transform.root.gameObject.GetComponent<Rigidbody2D> ();
		gravScript = transform.GetComponentInChildren<GravGunPull> ();
		if (transform.tag == "IGOR") {
			igorAnim = transform.parent.GetComponentInChildren<Animator> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.root.tag == "Player01")
        {
            vertAxe = ControlManager.P1LVert;
            hoAxe = ControlManager.P1RHriz;
        }
        else
        {
            vertAxe = ControlManager.P2LVert;
            hoAxe = ControlManager.P2RHriz;
        }


		if (Mathf.Abs (hoAxe) < analogDeadzone) {
			hoAxe = 0.0f;
		}
		if (Mathf.Abs (vertAxe) < analogDeadzone) {
			vertAxe = 0.0f;
		}

        linearForce = Vector2.up * vertAxe * thrustPower;

		if (transform.tag == "IGOR") {
			if (lastVert == 0) {
				if (vertAxe != lastVert) {
					igorAnim.SetTrigger ("Move");
				}
			} else if (vertAxe == 0) {
				if (gravScript.heldObject != null) {
					igorAnim.SetTrigger ("Grab");
				} else {
					igorAnim.SetTrigger ("Idle");
				}
			}
		}
	}

	void FixedUpdate(){
		shipBody.AddRelativeForce (linearForce);
		shipBody.AddTorque (-hoAxe * torquePower);
		if (shipBody.velocity.magnitude <= -MaxLinVelocity || shipBody.velocity.magnitude >= MaxLinVelocity) {
			MaxLin = true;
		} else {
			MaxLin = false;
		}
		if (shipBody.velocity.magnitude < -MaxLinVelocity) {
			shipBody.velocity = shipBody.velocity.normalized * -MaxLinVelocity;
		}
		if (shipBody.velocity.magnitude > MaxLinVelocity) {
			shipBody.velocity = shipBody.velocity.normalized * MaxLinVelocity;
		}

		if (shipBody.angularVelocity <= -MaxAngVelocity || shipBody.angularVelocity >= MaxAngVelocity) {
			MaxAng = true;
		} else {
			MaxAng = false;
		}
		if (shipBody.angularVelocity < -MaxAngVelocity) {
			shipBody.angularVelocity = -MaxAngVelocity;
		}
		if (shipBody.angularVelocity > MaxAngVelocity) {
			shipBody.angularVelocity = MaxAngVelocity;
		}
		lastVert = vertAxe;
	}
}
