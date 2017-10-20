using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skellyChecker : MonoBehaviour {

	public GameObject skelly;
	public GameObject deskSkelly;
	public Camera fpsCamera;

	// Update is called once per frame
	void Update () {
		Vector3 skellyPos = fpsCamera.WorldToViewportPoint (skelly.transform.position);
		if (skellyPos.x >= 0 && skellyPos.x <= 1 && skellyPos.y >= 0 && skellyPos.y <= 1 && skellyPos.z >= 0)
			deskSkelly.gameObject.SetActive (true);
	}
}
