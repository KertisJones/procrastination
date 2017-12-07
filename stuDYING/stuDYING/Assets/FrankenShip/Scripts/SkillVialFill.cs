using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillVialFill : MonoBehaviour {

	public SkillShot skillScript;
	Image myVial;
	public float currentTime01;

	// Use this for initialization
	void Start () {
		myVial = gameObject.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		if (skillScript.coolDown) {
			if (currentTime01 < skillScript.coolDownTime){
			currentTime01 += Time.unscaledDeltaTime;
			myVial.fillAmount = Mathf.Lerp (0.13f, 0.86f,
					Mathf.SmoothStep (0.0f, 1.0f, currentTime01 / skillScript.coolDownTime));
			}
		} else {
			currentTime01 = 0.0f;
			myVial.fillAmount = 0.86f;
		}
	}
}
