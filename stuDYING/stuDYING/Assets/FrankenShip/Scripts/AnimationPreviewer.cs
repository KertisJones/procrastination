using UnityEngine;
using System.Collections;

public class AnimationPreviewer : MonoBehaviour {

	public GameObject core01;
	public GameObject core02;
	Animator anim01;
	Animator anim02;

	// Use this for initialization
	void Start () {
		anim01 = core01.GetComponent<Animator> ();
		anim02 = core02.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			anim01.SetTrigger ("Still");
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			anim01.SetTrigger ("Laugh");
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			anim01.SetTrigger ("Death");
		}
		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			anim01.SetTrigger ("Angry");
		}
		if (Input.GetKeyDown (KeyCode.Alpha5)) {
			anim02.SetTrigger ("Still");
		}
		if (Input.GetKeyDown (KeyCode.Alpha6)) {
			anim02.SetTrigger ("Laugh");
		}
		if (Input.GetKeyDown (KeyCode.Alpha7)) {
			anim02.SetTrigger ("Death");
		}
		if (Input.GetKeyDown (KeyCode.Alpha8)) {
			anim02.SetTrigger ("Angry");
		}
	}
}
