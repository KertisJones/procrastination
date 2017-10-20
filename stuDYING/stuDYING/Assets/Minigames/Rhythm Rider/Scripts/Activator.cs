using UnityEngine;
using System.Collections;

public class Activator : MonoBehaviour {

	public KeyCode key;

	SpriteRenderer sr;
	bool active = false;
	GameObject note;
	Color old;
	public bool createMode;
	public GameObject n;

	// Use this for initialization
	void Awake () {
		sr = GetComponent<SpriteRenderer> ();
	}

	void Start(){
		old = sr.color;
		PlayerPrefs.SetInt ("Score", 0);
	}

	// Update is called once per frame
	void Update () {
		if (createMode) {
			if (Input.GetKeyDown (key)) {
				Instantiate (n, transform.position, Quaternion.identity);
			}
		} else {
			if (Input.GetKeyDown (key)) {
				StartCoroutine (Pressed ());
			}

			if (Input.GetKeyDown (key) && active) {
				Destroy (note);
				AddScore ();
				active = false;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		active = true;
		if (col.gameObject.tag == "Note")
			note = col.gameObject;
	}

	void OnTriggerExit2D(Collider2D col){
		active = false;
	}

	void AddScore(){
		PlayerPrefs.SetInt ("Score", PlayerPrefs.GetInt ("Score") + 100);
	}

	IEnumerator Pressed(){
		sr.color = new Color (0, 0, 0);
		yield return new WaitForSeconds (0.05f);
		sr.color = old;
	}
}
