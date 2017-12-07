using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GravGunPowMeter : MonoBehaviour {

    Image powmeter;
	public Transform myGravGun;
	GravGunPull gravScript;
	public Color chargeStartColor;
	public Color chargeEndColor;
	public Color overStartColor;
	public Color overEndColor;
	Color shotColor;
	float currentTime01;
	float currentTime02;
	float currentTime03;
	public float shootFadeTime;

	// Use this for initialization
	void Start () {
		gravScript = myGravGun.GetComponent<GravGunPull>();
		powmeter = gameObject.GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (gravScript.pushPower > 0) {
			if (!gravScript.atMaxPower) {
				currentTime01 += Time.unscaledDeltaTime;
				powmeter.fillAmount = Mathf.InverseLerp (20, gravScript.maxPushPower, gravScript.pushPower);
				powmeter.color = Color.Lerp(chargeStartColor, chargeEndColor, Mathf.SmoothStep(0, 1, gravScript.pushPower / gravScript.maxPushPower - 20));
			} else {
				currentTime02 += Time.unscaledDeltaTime;
				powmeter.color = Color.Lerp(overStartColor, overEndColor, Mathf.SmoothStep(0, 1, currentTime02/gravScript.overChargeTime));
			}
			currentTime03 = 0.0f;
			shotColor = powmeter.color;
		} else {
			currentTime01 = 0.0f;
			currentTime02 = 0.0f;
			if (currentTime03 < shootFadeTime && powmeter.fillAmount > 0) {
				currentTime03 += Time.unscaledDeltaTime;
				powmeter.color = Color.Lerp (new Color (shotColor.r, shotColor.g, shotColor.b, 0.75f), new Color (shotColor.r, shotColor.g, shotColor.b, 0.0f), Mathf.SmoothStep (0, 1, currentTime03 / shootFadeTime));
			} else {
				powmeter.fillAmount = 0.0f;
				powmeter.color = chargeStartColor;
			}
		}
	}
}
