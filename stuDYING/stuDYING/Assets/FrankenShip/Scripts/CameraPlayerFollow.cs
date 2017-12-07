using UnityEngine;
using System.Collections;

public class CameraPlayerFollow : MonoBehaviour {

	public GameObject myPlayer;
	public bool followRotation;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (myPlayer.transform.position.x, myPlayer.transform.position.y, -10);
		if (followRotation) {
			transform.rotation = myPlayer.transform.rotation;
		}
	}
}
