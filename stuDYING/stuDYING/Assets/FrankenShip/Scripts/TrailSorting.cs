using UnityEngine;
using System.Collections;

public class TrailSorting : MonoBehaviour {

	TrailRenderer myTrail;

	// Use this for initialization
	void Start () {
		myTrail = gameObject.GetComponent<TrailRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		myTrail.sortingLayerName = "Particles02";
		myTrail.sortingOrder = 10;
	}
}
