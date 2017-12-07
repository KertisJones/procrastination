using UnityEngine;
using System.Collections;

public class TeslaAnimTrigger : MonoBehaviour {
	
	Animator myAnim;

	// Use this for initialization
	void Start () {
		myAnim = gameObject.GetComponent<Animator> ();
		myAnim.SetBool ("Player02", true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
