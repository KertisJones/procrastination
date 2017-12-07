using UnityEngine;
using System.Collections;

public class BlackHolePole : MonoBehaviour {

	BlackHoleObjectState stateScript;
	Rigidbody2D otherBody;
	Vector2 pullDir;
	float distance;
	float rotateAmount;
	float pullAmount;
	float currentAngle;
	float extraNum;
	float startDistance;
	float myRadius;
	public float rotateAmountMin;
	public float rotateAmountMax;
	public float pullAmountMin;
	public float pullAmountMax;


	// Use this for initialization
	void Start () {
		myRadius = gameObject.GetComponent<CircleCollider2D> ().radius;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerStay2D(Collider2D other){
		if (!other.isTrigger) {
			otherBody = other.transform.root.GetComponentInChildren<Rigidbody2D> ();
			pullDir = transform.position - other.transform.position;
			extraNum = Mathf.InverseLerp (myRadius, 0, pullDir.magnitude);
			pullAmount = Mathf.Lerp (pullAmountMin, pullAmountMax, extraNum);
			rotateAmount = Mathf.Lerp (rotateAmountMin, rotateAmountMax, extraNum);
			stateScript = other.GetComponent<BlackHoleObjectState> ();
			if (other.gameObject.layer != 8 && stateScript.myState == "Held") {

			}
			else {
				if (other.tag == "IGOR") {
					pullAmount = pullAmount / 3;
					rotateAmount = rotateAmount / 3;
				}
				otherBody.AddForce (pullDir.normalized * pullAmount, ForceMode2D.Force);
				other.transform.root.RotateAround (transform.position, Vector3.forward, -rotateAmount);
			}
		}
	}
}
