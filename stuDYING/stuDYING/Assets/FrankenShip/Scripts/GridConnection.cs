using UnityEngine;
using System.Collections;

public class GridConnection : MonoBehaviour {

	public ShipBuilder buildScript;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D other){
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Grid"){
			if (transform.parent.parent != null) {
				CheckConnection[] checks = transform.parent.GetComponentsInChildren<CheckConnection>();
				foreach (CheckConnection check in checks){
					if (check.connected && check.GetComponent<SliderJoint2D>().connectedBody.transform.parent == transform.parent.parent){
						Destroy(check.GetComponent<SliderJoint2D>().connectedBody.GetComponent<SliderJoint2D>());
						Destroy(check.GetComponent<SliderJoint2D>());
					}
				}

				transform.parent.SetParent(null);
			}
		}
	}
}