using UnityEngine;
using System.Collections;

public class ExplodeFace : MonoBehaviour {

	public float rektStrength;

	// Use this for initialization
	void Start () {
		FaceRekt ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void FaceRekt (){
		foreach (Rigidbody2D facePart in transform.GetComponentsInChildren<Rigidbody2D>()) {
			Vector3 forceDir = facePart.transform.position - transform.position;
			facePart.AddForceAtPosition (forceDir.normalized * rektStrength, facePart.transform.position, ForceMode2D.Impulse);
		}
	}
}
