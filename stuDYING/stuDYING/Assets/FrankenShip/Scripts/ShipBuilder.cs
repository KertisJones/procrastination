using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShipBuilder : MonoBehaviour {

	SpriteRenderer lastSprite;
	public Animator[] cursorAnims;
	public GameObject myPlayer;
	public Image pointerImage;
	public Image igorImage;
	public Transform currentDrag;
	public Transform lastDrag;
	public GameObject rotateInfo;
	public GameObject nodeConnect;
    //FOR RETURN TO SPAWN Transform
	public Transform spawnTransform;
    ////////////////////////////////
	public Vector3 mPos;
	public Collider2D dragHitColl;
	public Collider2D gridHitColl;
	public Collider2D lastHitColl;
	LayerMask dragLayer = 1 << 14;
	LayerMask gridLayer = 1 << 15;
	public bool rotateMode;
	public bool rotateCenter;
	public bool areaClear;
	public bool usedAllParts;
	public int weaponDir = 0;
	public int initialInvCount;
	public int numChildrens;
	public int numPartsSpawned;
	public InAudioNode grabSound;
	public InAudioNode placeSound;
	CursorMovement cursorScript;
	public bool actionButton;
	public bool leftRotate;
	public bool rightRotate;
    /// <summary>
    /// Part drop check. True if dropped.
    /// </summary>
    public bool dropped;
	PhaseNavigator navScript;
	bool timerStarted;
	public float currentTime01;
	GameObject gridObject;
	List <GameObject> nodeConnList;

    //particle FX
    public GameObject sparksFX;
	GameObject newSparks;

	// Use this for initialization
	void Start () {
		nodeConnList = new List<GameObject> ();
		if (gameObject.tag == "ShipBuilder01") {
			myPlayer = GameObject.FindWithTag("Player02");
		} else if (gameObject.tag == "ShipBuilder02") {
			myPlayer = GameObject.FindWithTag("Player01");
        }
        initialInvCount = myPlayer.GetComponent<Inventory> ().invParts.Count;
		cursorAnims = pointerImage.transform.parent.GetComponentsInChildren<Animator> ();
		cursorScript = pointerImage.transform.parent.GetComponent<CursorMovement> ();
		cursorScript.gridBound = true;
		navScript = GameObject.FindWithTag ("GameMaster").GetComponent<PhaseNavigator> ();
		gridObject = gameObject.GetComponentInChildren<BuilderPull> ().gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.tag == "ShipBuilder01") {
			actionButton = ControlManager.P1A;
			leftRotate = ControlManager.P1LBump;
			rightRotate = ControlManager.P1RBump;
		} else if (gameObject.tag == "ShipBuilder02") {
			actionButton = ControlManager.P2A;
			leftRotate = ControlManager.P2LBump;
			rightRotate = ControlManager.P2RBump;
		}

		mPos = Camera.main.ScreenToWorldPoint (pointerImage.transform.position);
		mPos.z = -2;
		dragHitColl = Physics2D.OverlapPoint (new Vector2 (mPos.x, mPos.y), dragLayer);
		gridHitColl = Physics2D.OverlapPoint (new Vector2 (mPos.x, mPos.y), gridLayer);

		if (!rotateMode){
			cursorScript.cursorSpeed = cursorScript.startSpeed;
			igorImage.color = Color.white;
			pointerImage.enabled = true;
			if (lastHitColl != null && currentDrag == null && (dragHitColl == null || dragHitColl != lastHitColl)){
				lastHitColl.transform.parent.GetComponent<SpriteRenderer>().material.color = Color.white;
				SetAnimTrigs ("Idle");
				lastHitColl = null;
			}

			if (dragHitColl != null) {
				Transform highlightedObject = dragHitColl.transform.parent;
				if (highlightedObject != currentDrag) {
					highlightedObject.GetComponent<SpriteRenderer> ().material.color = Color.yellow;
					if (lastHitColl == null || (lastHitColl != null && lastHitColl != dragHitColl)) {
						SetAnimTrigs ("Highlight");
					}
					lastHitColl = dragHitColl;

					if (actionButton) {
						currentDrag = highlightedObject;
						currentDrag.eulerAngles = Vector3.zero;
						currentDrag.GetComponent<SpriteRenderer> ().sortingOrder = 10;
						currentDrag.GetComponentInChildren<HighlightSpriter> ().GetComponent<SpriteRenderer> ().enabled = true;
						SetAnimTrigs ("Grab");
						InAudio.Play (gameObject, grabSound, null);
                        dropped = false;
						StartCoroutine (nodeConnWait ());
					}
				} else if (actionButton) {
					lastDrag = currentDrag;
					lastDrag.GetComponent<SpriteRenderer> ().sortingOrder = 5;
					SetAnimTrigs ("Highlight");
					InAudio.Play (gameObject, placeSound, null);
                    dropped = true;
					currentDrag.position = spawnTransform.position;
                    currentDrag = null;
                    DestroyNodes ();
				} else {
					currentDrag.position = mPos;
				}
			}
		}

		else {
			cursorScript.cursorSpeed = 0;
			igorImage.color = Color.white * 0.35f;
			pointerImage.enabled = false;
			rotateInfo.SetActive (true);
			lastSprite = lastDrag.GetComponent<SpriteRenderer> ();

			if (areaClear) {
				lastSprite.material.color = Color.green;
				if (actionButton) {
					rotateInfo.SetActive(false);
					rotateMode = false;
					rotateCenter = false;
					lastSprite.material.color = Color.white;
					weaponDir = 0;
				}
			} else {
				lastSprite.material.color = Color.red;
			}

			if (leftRotate) {
				if (!rotateCenter && weaponDir == -2) {
					
				} else {
					lastDrag.transform.eulerAngles = lastDrag.transform.eulerAngles + new Vector3 (0, 0, 45);
					weaponDir -= 1;
				}
			} else if (rightRotate) {
				if (!rotateCenter && weaponDir == 2) {

				} else {
					lastDrag.transform.eulerAngles = lastDrag.transform.eulerAngles - new Vector3 (0, 0, 45);
					weaponDir += 1;
				}
			}
		}

		numChildrens = myPlayer.GetComponentsInChildren<HullHealth> ().Length;
		if (numChildrens == numPartsSpawned + 3) {
			if (navScript.DEVMODE_Build) {
				usedAllParts = true;
			} else if (initialInvCount == numPartsSpawned) {
				usedAllParts = true;
			}
		}  else {
			usedAllParts = false;
		}
	}

	public void SetAnimTrigs(string trigger){
		foreach (Animator anim in cursorAnims) {
			anim.SetTrigger (trigger);
		}
	}

    ///used to spawn Spark FX
    public void SpawnSparks(Vector3 position)
    {
        newSparks = (GameObject)Instantiate(sparksFX, position, transform.rotation);
    }

	IEnumerator nodeConnWait(){
		yield return new WaitForSeconds (0.1f);
		foreach (CheckConnection checker in myPlayer.GetComponentsInChildren<CheckConnection>()) {
			if (!checker.connected && checker.gridHitColl != null) {
				if (checker.gameObject.layer == 10 && (currentDrag.tag == "Thruster" || currentDrag.tag == "PlasmaTorch" || currentDrag.tag == "Hull")) {

				} else {
					GameObject nodeConn = (GameObject)Instantiate (nodeConnect, checker.transform.position, transform.rotation);
					nodeConnList.Add (nodeConn);
				}
			}
		}
	}

	public void DestroyNodes(){
		StartCoroutine (DestroyNodeConns ());
	}

	public IEnumerator DestroyNodeConns(){
		yield return new WaitForSeconds (0.15f);
		foreach (GameObject nodeConn in nodeConnList) {
			Destroy (nodeConn);
		}
		nodeConnList.Clear ();
	}

}
