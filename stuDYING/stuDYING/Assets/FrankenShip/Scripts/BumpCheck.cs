using UnityEngine;
using System.Collections;

public class BumpCheck : MonoBehaviour {

	public ShipBuilder buildScript;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerStay2D(Collider2D other){
		buildScript.areaClear = false;
		transform.parent.GetComponent<SpriteRenderer> ().color = Color.red;
	}

	void OnTriggerExit2D(Collider2D other){
		buildScript.areaClear = true;
		transform.parent.GetComponent<SpriteRenderer> ().color = Color.white;
	}
}
