using UnityEngine;
using System.Collections;

public class CheckConnection : MonoBehaviour {

	PhaseNavigator navScript;
	public ShipBuilder buildScript;
	public CheckConnection connectScript;
	Transform connectPoint;
	GameObject weaponCent;
	public SpriteRenderer dadSprite;
	public bool collide;
	public bool connect;
	public bool connected;
	public float connDist;
	public float distFromCenter;
	GameObject shipHitch;
	public bool centerConn;
	public Collider2D gridHitColl;
	LayerMask gridLayer = 1 << 15;

	// Use this for initialization
	void Start () {
		navScript = GameObject.FindWithTag ("GameMaster").GetComponent<PhaseNavigator> ();
		if (transform.root.tag == "Player01") {
			buildScript = GameObject.FindGameObjectWithTag("ShipBuilder02").GetComponent<ShipBuilder>();
		} else if (transform.root.tag == "Player02") {
			buildScript = GameObject.FindGameObjectWithTag("ShipBuilder01").GetComponent<ShipBuilder>();
		}
		dadSprite = transform.parent.GetComponent<SpriteRenderer> ();
		if (dadSprite.sortingLayerName == "Weapons") {
			foreach (Transform child in transform.parent.transform) {
				if (child.gameObject.layer == 10){
					weaponCent = child.gameObject;
				}
			}
		}
		shipHitch = transform.GetChild (0).gameObject;
		if (gameObject.layer == 10) {
			centerConn = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		distFromCenter = Vector2.Distance (buildScript.myPlayer.transform.position, transform.position);
		gridHitColl = Physics2D.OverlapPoint (transform.position, gridLayer);

		if (transform.parent == buildScript.lastDrag) {
			transform.parent.position = new Vector3 (transform.parent.position.x, transform.parent.position.y, -1.0f);
		} else if (transform.parent != buildScript.currentDrag) {
			transform.parent.position = new Vector3 (transform.parent.position.x, transform.parent.position.y, 0.0f);
		}

		
		if (gameObject.GetComponent<SliderJoint2D> () != null) {
			connected = true;
			Transform connGuy = gameObject.GetComponent<SliderJoint2D> ().connectedBody.transform;
			connDist = Vector2.Distance (transform.position, connGuy.position);
			if(buildScript.currentDrag == transform.parent) {
				Destroy(connGuy.GetComponent<SliderJoint2D>());
				Destroy(gameObject.GetComponent<SliderJoint2D> ());
				transform.parent.SetParent(null);
				if (transform.parent == connGuy.parent.parent) {
					foreach (CheckConnection checker in connGuy.parent.GetComponentsInChildren<CheckConnection>()) {
						if (checker.connected) {
							connGuy.parent.SetParent (checker.GetComponent<SliderJoint2D> ().connectedBody.transform.parent);
							break;
						} else {
							connGuy.parent.SetParent (null);
						}
					}
				}
			}
		} else {
			connected = false;
			shipHitch.SetActive(false);
		}

		if (dadSprite.sortingLayerName == "Weapons") {
			if (connected == true){
				weaponCent.SetActive(false);
			} else{
				weaponCent.SetActive(true);
			}
		}

		if (connect == true && buildScript.gridHitColl != null) {
			dadSprite.material.color = Color.green;
			if (buildScript.currentDrag == null) {
				dadSprite.material.color = Color.white;

				SliderJoint2D newJoint = gameObject.AddComponent<SliderJoint2D> ();
				newJoint.connectedBody = connectScript.gameObject.GetComponent<Rigidbody2D>();
				shipHitch.SetActive(true);

				if (centerConn) {
					buildScript.rotateCenter = true;
				}

				connect = false;

				if(buildScript.lastDrag == transform.parent){
					buildScript.SpawnSparks(connectPoint.position);
					transform.parent.GetComponentInChildren<HighlightSpriter> ().GetComponent<SpriteRenderer> ().enabled = false;
					transform.parent.SetParent(newJoint.connectedBody.transform.parent.transform);
					if(dadSprite.sortingLayerName == "Hulls") {
						transform.parent.transform.position = new Vector3(connectPoint.position.x,
						                                                  connectPoint.position.y,
						                                                  transform.parent.transform.position.z) + connectPoint.localPosition;
					}
					else if (dadSprite.sortingLayerName == "Thrusters"){
						transform.parent.transform.position = new Vector3(connectPoint.position.x,
						                                                  connectPoint.position.y,
						                                                  transform.parent.transform.position.z) + connectPoint.localPosition / 2;
						Vector3 dir = transform.parent.transform.position - connectPoint.transform.parent.position;
						var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
						transform.parent.transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
					} else{
						if (transform.parent.tag == "PlasmaTorch") {
							transform.parent.transform.position = new Vector3 (connectPoint.position.x,
								connectPoint.position.y,
								transform.parent.transform.position.z);// + connectPoint.localPosition / 1.75f;
						} else {
							transform.parent.transform.position = new Vector3 (connectPoint.position.x,
								connectPoint.position.y,
								transform.parent.transform.position.z);
							buildScript.rotateMode = true;
						}
						Vector3 weapDir = transform.parent.transform.position - connectPoint.transform.parent.position;
						var weapAngle = Mathf.Atan2(weapDir.y, weapDir.x) * Mathf.Rad2Deg;
						transform.parent.transform.rotation = Quaternion.AngleAxis(weapAngle - 90, Vector3.forward);

					}

					if (!navScript.DEVMODE_Build) {
						foreach (Transform child in transform.parent) {
							if (child.gameObject.layer == 14) {
								Destroy (child.gameObject);
							}
						}
					}
				}
			}
		}
	}

	void OnTriggerStay2D(Collider2D other){
		connectPoint = other.transform;
		collide = true;
		connectScript = other.GetComponent<CheckConnection> ();
		if (connectScript != null && connectScript.connected == false && connected == false &&
			((connectScript.transform.root.tag == "Player01" || connectScript.transform.root.tag == "Player02") ||
				(transform.root.tag == "Player01" || transform.root.tag == "Player02"))) {
			if (dadSprite.sortingLayerName == "Hulls" && connectScript.dadSprite.sortingLayerName == "Hulls" && 
				((connectPoint.localPosition != (transform.localPosition * -1.0f)) || (centerConn && connectScript.centerConn))) {
				connect = false;
				dadSprite.material.color = Color.red;
			} else if (dadSprite.sortingLayerName == "Thrusters" &&
			         (connectScript.dadSprite.sortingLayerName != "Hulls")) {
				connect = false;
				dadSprite.material.color = Color.red;
            } else if (dadSprite.sortingLayerName == "Weapons" && connectScript.dadSprite.sortingLayerName == "Weapons") {
				connect = false;
				dadSprite.material.color = Color.red;
            } else if (!buildScript.areaClear) {
				connect = false;
				dadSprite.material.color = Color.red;
            }
			else if (buildScript.currentDrag == transform.parent || buildScript.currentDrag == other.transform.parent){
				connect = true;
			}
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (buildScript.currentDrag == transform.parent || buildScript.currentDrag == other.transform.parent){
			collide = false;
			connect = false;
			dadSprite.material.color = Color.white;
		}
	}
}
