using UnityEngine;
using System.Collections;

public class ThrusterRotateConn : MonoBehaviour {

	GameObject gameMaster;
	public ShipBuilder buildScript;

	// Use this for initialization
	void Start () {
		gameMaster = GameObject.FindWithTag ("GameMaster");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.transform.parent.GetComponent<SpriteRenderer>().sortingLayerName == "Hulls" && transform.parent == buildScript.currentDrag){
			Vector3 dir = transform.parent.transform.position - other.transform.parent.position;
			var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			transform.parent.transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
		}
	}
}
