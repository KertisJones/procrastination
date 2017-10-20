using UnityEngine;
using System.Collections;

public class NoteColliderScript : MonoBehaviour {

	Vector3 start;

	Vector3 leftPosn = new Vector3(-0.984f, -0.469f, -8.513f);
	Vector3 midPosn = new Vector3(0.112f, -0.469f, -8.513f);
	Vector3 rightPosn = new Vector3(1.218f, -0.469f, -8.513f);

	// Use this for initialization
	void Start () {
		start = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LeftLaneShift(){
		this.transform.position = leftPosn;
		start = leftPosn;
	}
	public void MidLaneShift(){
		this.transform.position = midPosn;
		start = midPosn;
	}
	public void RightLaneShift(){
		this.transform.position = rightPosn;
		start = rightPosn;
	}
}
