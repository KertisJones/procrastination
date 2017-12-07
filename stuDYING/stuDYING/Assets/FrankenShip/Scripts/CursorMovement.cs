using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CursorMovement : MonoBehaviour {

    GameObject gameMaster;
	Vector3 myWorldPos;
	float xBound;
	float yBound;
	[HideInInspector]
	public float cursorSpeed;
	[HideInInspector]
	public float startSpeed;
	public bool gridBound;
	public bool menuBound;
	
	// Use this for initialization
	void Start () {
		startSpeed = 5.0f;
		cursorSpeed = startSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		myWorldPos = Camera.main.ScreenToWorldPoint (transform.position);
		if (gridBound) {
			if (gameObject.tag == "Cursor01") {
				xBound = Mathf.Clamp (myWorldPos.x, -12.9f, 18.0f);
				yBound = Mathf.Clamp (myWorldPos.y, 0.0f, 10.9f);
			} else if (gameObject.tag == "Cursor02") {
				xBound = Mathf.Clamp (myWorldPos.x, -18.0f, 12.3f);
				yBound = Mathf.Clamp (myWorldPos.y, -14.8f, -3.9f);
			}
			transform.position = Camera.main.WorldToScreenPoint (new Vector3 (xBound, yBound, myWorldPos.z));
		}
		if (menuBound) {
			xBound = Mathf.Clamp (myWorldPos.x, -4.5f, 4.5f);
			yBound = Mathf.Clamp (myWorldPos.y, 0.0f, 2.5f);
			transform.position = Camera.main.WorldToScreenPoint (new Vector3 (xBound, yBound, myWorldPos.z));
		}
        if (gameObject.tag == "Cursor01")
        {
			gameObject.transform.position += new Vector3(ControlManager.P1LHriz, ControlManager.P1LVert, 0) * cursorSpeed;
        }
        else if (gameObject.tag == "Cursor02")
        {
			gameObject.transform.position += new Vector3(ControlManager.P2LHriz, ControlManager.P2LVert, 0) * cursorSpeed;
        }
	}
}
