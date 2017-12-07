using UnityEngine;
using System.Collections;

public class DistortRotation : MonoBehaviour {

	public float rotateSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround (transform.position, Vector3.forward, -rotateSpeed);
	}
}
