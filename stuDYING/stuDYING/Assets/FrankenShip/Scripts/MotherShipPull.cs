using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MotherShipPull : MonoBehaviour {

	GameObject player01;
	GameObject player02;
	public GameObject myPlayer;
	public GameObject otherPlayer;
	public List<GameObject> stasisFieldList;
	GameObject gameMaster;
	PhaseNavigator navScript;
	CrateScript[] crateScripts;
	CircleCollider2D [] fieldColls;
	Inventory invScript;
	Vector3 forceDir;
	Vector3 lookDir;
	public float currentTime01;
	int fieldCount;
	bool deactivate;
	public bool pullStasis;
	public int numCratesPulled;
	public bool equalCount;
	public float pullStrength;

	// Use this for initialization
	void Start () {
		player01 = GameObject.FindWithTag ("Player01");
		player02 = GameObject.FindWithTag ("Player02");
		navScript = GameObject.FindWithTag ("GameMaster").GetComponent<PhaseNavigator> ();
		if (gameObject.tag == "MotherShip01") {
			myPlayer = player01;
			otherPlayer = player02;
		} else {
			myPlayer = player02;
			otherPlayer = player01;
		}
		invScript = myPlayer.GetComponent<Inventory> ();
    }
	
	// Update is called once per frame
	void Update () {
		if (numCratesPulled == invScript.invParts.Count){
			equalCount = true;
		}

		forceDir = transform.position - myPlayer.transform.position;
		lookDir = otherPlayer.transform.position - myPlayer.transform.position;
		myPlayer.GetComponent<Rigidbody2D>().AddForceAtPosition(forceDir.normalized * pullStrength, transform.position);

		var angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
		myPlayer.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

		for (int i = 0; i < invScript.invParts.Count; i++){
			if (invScript.invParts[i].gameObject != null){
				Rigidbody2D thisBody = invScript.invParts[i].GetComponent<Rigidbody2D>();
				//invScript.invParts [i].GetComponent<CircleCollider2D> ().isTrigger = true;
				forceDir = transform.position - thisBody.transform.position;
				thisBody.AddForceAtPosition(forceDir, transform.position);
			}
		}

		if (pullStasis) {
			float pullTime = 2.5f;
			if (currentTime01 <= pullTime) {
				currentTime01 += Time.unscaledDeltaTime;
				foreach (GameObject stasis in stasisFieldList) {
					Vector3 pullDir = transform.position - stasis.transform.position;
					stasis.transform.position = Vector3.Lerp (transform.position + pullDir.normalized * 45, transform.position, Mathf.SmoothStep (0.0f, 1.0f, currentTime01 / pullTime));
				}
			} else {
				pullStasis = false;
			}
		}
	}

	public void DeactivateFields(){
		crateScripts = gameObject.GetComponentsInChildren<CrateScript> ();
		fieldColls = gameObject.GetComponentsInChildren <CircleCollider2D> ();
		foreach (CircleCollider2D fieldColl in fieldColls) {
			fieldColl.enabled = false;
		}

		foreach (CrateScript script in crateScripts) {
			GameObject crate = script.gameObject;
			invScript.invParts.Add (crate);
			crate.transform.SetParent (null);
		}
	}

	void OnTriggerStay2D (Collider2D other){
		if (other.gameObject.layer == 18) {
			for (int i = 0; i < invScript.invParts.Count; i++){
				if (other.gameObject == invScript.invParts[i].gameObject){
					numCratesPulled++;
				}
			}
		}
		if (other.transform.root.gameObject == myPlayer) {
			other.transform.root.position = transform.position;
			other.transform.root.rotation = transform.rotation;
			other.transform.root.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;
			gameObject.GetComponent<Collider2D> ().enabled = false;
			pullStasis = true;
			foreach (PolygonCollider2D poly in gameObject.GetComponentsInChildren<PolygonCollider2D>()){
				poly.enabled = false;
			}
			DeactivateFields ();
		}
	}

}
