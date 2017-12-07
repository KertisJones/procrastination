﻿using UnityEngine;
using System.Collections;

public class DestroyAfterDelay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (DestroySelf ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator DestroySelf() {
		yield return new WaitForSeconds (2.0f);
		Destroy (gameObject);
	}
}
