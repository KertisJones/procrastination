using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public KeyCode leftMove;
	public KeyCode rightMove;
	public GameObject beatHitter;
	public GameObject noteFailCollider;
	Vector3 leftPosn = new Vector3(-0.984f, -0.228f, -8.508f);
	Vector3 midPosn = new Vector3(0.112f, -0.228f, -8.508f);
	Vector3 rightPosn = new Vector3(1.218f, -0.228f, -8.508f);
	bool leftLane = false;
	bool midLane = true;
	bool rightLane = false;

	// Use this for initialization
	void Start () {
		this.transform.position = midPosn;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (leftMove) && !leftLane) {
			if (midLane) {
				midLane = false;
				this.transform.position = leftPosn;
				beatHitter.GetComponent<ActivatorLane> ().LeftLaneShift ();
				noteFailCollider.GetComponent<NoteColliderScript> ().LeftLaneShift ();
				StartCoroutine (laneShiftOperator ());
				leftLane = true;
			} else if (rightLane) {
				rightLane = false;
				this.transform.position = midPosn;
				beatHitter.GetComponent<ActivatorLane> ().MidLaneShift ();
				noteFailCollider.GetComponent<NoteColliderScript> ().MidLaneShift ();
				StartCoroutine (laneShiftOperator ());
				midLane = true;
			}
		} else if (Input.GetKeyDown (rightMove) && !rightLane) {
			if (midLane) {
				midLane = false;
				this.transform.position = rightPosn;
				beatHitter.GetComponent<ActivatorLane> ().RightLaneShift ();
				noteFailCollider.GetComponent<NoteColliderScript> ().RightLaneShift ();
				StartCoroutine (laneShiftOperator ());
				rightLane = true;
			} else if (leftLane) {
				leftLane = false;
				this.transform.position = midPosn;
				beatHitter.GetComponent<ActivatorLane> ().MidLaneShift ();
				noteFailCollider.GetComponent<NoteColliderScript> ().MidLaneShift ();
				StartCoroutine (laneShiftOperator ());
				midLane = true;
			}
		}
	}

	IEnumerator laneShiftOperator(){
		noteFailCollider.GetComponent<BoxCollider> ().enabled = false;
		yield return new WaitForSeconds (1);
		noteFailCollider.GetComponent<BoxCollider> ().enabled = true;
	}
}
