using UnityEngine;
using System.Collections;

public class MotherBulbShocker : MonoBehaviour {

	Animator[] bulbAnims;
	Animator faceAnim;

	// Use this for initialization
	void Start () {
		bulbAnims = gameObject.GetComponentsInChildren<Animator> ();
		faceAnim = transform.root.GetComponentInChildren<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D (Collision2D other){
		faceAnim.SetTrigger ("Laugh");
		foreach (Animator bulbAnim in bulbAnims) {
			bulbAnim.SetTrigger ("Shock");
		}
		StartCoroutine (WaitTrigIdle ());
	}

	IEnumerator WaitTrigIdle(){
		yield return new WaitForSeconds (2.0f);
		foreach (Animator bulbAnim in bulbAnims) {
			bulbAnim.SetTrigger ("Idle");
		}
		faceAnim.SetTrigger ("Still");
	}
}
