using UnityEngine;
using System.Collections;

public class DestroyObject : MonoBehaviour {

	ShipBuilder myBuilder;
	// Use this for initialization
	void Start () {
		myBuilder = transform.root.GetComponent<ShipBuilder> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other){
		Destroy (other.transform.parent.gameObject);
		myBuilder.numPartsSpawned--;
	}
}
