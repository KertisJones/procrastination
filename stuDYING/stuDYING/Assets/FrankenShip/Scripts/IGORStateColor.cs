using UnityEngine;
using System.Collections;

public class IGORStateColor : MonoBehaviour {

	GravGunPull gravScript;
	ParticleSystem mySystem;
	public Color idleColor;
	public Color collideColor;
	public Color grabColor;
	public Color chargeStart;
	public Color chargeEnd;
	public float startSizeMin;
	public float startSizeMax;
	float myStartSize;
	float pushPowerT;
	public float chargeTime;
	float currentTime;

	// Use this for initialization
	void Start () {
		gravScript = gameObject.transform.parent.GetComponent<GravGunPull> ();
		mySystem = gameObject.GetComponent<ParticleSystem> ();
		myStartSize = mySystem.startSize;
	}
	
	// Update is called once per frame
	void Update () {
		if (gravScript.heldObject == null) {
			mySystem.startColor = idleColor;
			mySystem.startSize = myStartSize;
			if (gravScript.collide) {
				mySystem.startColor = collideColor;
			}
		} else {
			if (gravScript.pushPower > gravScript.startPushPower) {
				currentTime += Time.deltaTime;
				mySystem.startColor = Color.Lerp(chargeStart, chargeEnd, currentTime/chargeTime);
				mySystem.startSize = Mathf.Lerp(startSizeMin, startSizeMax, currentTime/chargeTime);
			} else{
				currentTime = 0.0f;
				mySystem.startColor = grabColor;
			}
		}
	}
}
