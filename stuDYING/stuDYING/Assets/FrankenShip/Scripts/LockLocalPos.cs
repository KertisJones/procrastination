using UnityEngine;
using System.Collections;

public class LockLocalPos : MonoBehaviour {

	public Vector3 myLocPos;
	public Quaternion myLocRot;
    

    // Use this for initialization
    void Start () {
		myLocPos = transform.localPosition;
		myLocRot = transform.localRotation;
    }
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = myLocPos;
		transform.localRotation = myLocRot;
	}
}
