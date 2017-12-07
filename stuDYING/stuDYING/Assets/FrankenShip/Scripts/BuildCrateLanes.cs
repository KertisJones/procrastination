using System;
using UnityEngine;
using UnityEngine.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BuildCrateLanes : MonoBehaviour {

	PhaseNavigator navScript;
	ShipBuilder buildScript;
	Inventory invScript;
	CrateScript crateScript;
	List <GameObject> crateInvUI;
	List <Sprite> squareInvUI;
	Text crateText;
	public GameObject crate01UI;
	public GameObject crate02UI;
	public GameObject crate03UI;
	public Sprite bronzeSquare;
	public Sprite silverSquare;
	public Sprite diamondSquare;
	public InAudioNode spawnSound;
	public Transform spawnTrans;
    public static int p1BrzCrateNum = 0;
    public static int p1SlvCrateNum = 0;
    public static int p1DimCrateNum = 0;
    public static int p2BrzCrateNum = 0;
    public static int p2SlvCrateNum = 0;
    public static int p2DimCrateNum = 0;

    // Use this for initialization
    void Start () {
		crateInvUI = new List<GameObject> ();
		squareInvUI = new List<Sprite> ();
		navScript = GameObject.FindWithTag ("GameMaster").GetComponent<PhaseNavigator> ();
		crateText = gameObject.GetComponentInChildren<Text> ();
		if (transform.parent.tag == "BuildCanvas01") {
			invScript = GameObject.FindWithTag ("Player02").GetComponent<Inventory> ();
			buildScript = GameObject.FindWithTag ("ShipBuilder01").GetComponent<ShipBuilder> ();
		} else if (transform.parent.tag == "BuildCanvas02") {
			invScript = GameObject.FindWithTag ("Player01").GetComponent<Inventory> ();
			buildScript = GameObject.FindWithTag ("ShipBuilder02").GetComponent<ShipBuilder> ();
		}
		SpawnLaneParts ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void SpawnLaneParts(){
		crateInvUI.Clear ();
		squareInvUI.Clear ();
		if (invScript.invParts.Count == 0) {
			crateText.text = "Empty";
		} else {
			for (int i = 0; i < invScript.invParts.Count; i++) {
				if (i < transform.childCount) {
					GameObject realCrate = invScript.invParts [i].gameObject;
					Transform uiSpot = transform.GetChild (i);
					switch (realCrate.tag) {
					case "LootCrate01":
						crateInvUI.Add (crate01UI);
						squareInvUI.Add (silverSquare);
						break;
					case "LootCrate02":
						crateInvUI.Add (crate02UI);
						squareInvUI.Add (bronzeSquare);
						break;
					case "LootCrate03":
						crateInvUI.Add (crate03UI);
						squareInvUI.Add (diamondSquare);
						break;
					}
					transform.GetChild (i).GetComponent<Image> ().sprite = squareInvUI [i];
					GameObject newCrateUI = (GameObject)Instantiate (crateInvUI [i].gameObject, uiSpot.position, transform.rotation);
					newCrateUI.transform.SetParent (uiSpot);

					newCrateUI.transform.localScale = Vector3.one;
					newCrateUI.transform.localPosition = Vector3.up * 5.0f;
					if (i == 0) {
						switch (realCrate.tag) {
						case "LootCrate01":
							crateText.text = "Silver";
							break;
						case "LootCrate02":
							crateText.text = "Gold";
							break;
						case "LootCrate03":
							crateText.text = "Diamond";
							break;
						}
					} else {
						newCrateUI.GetComponent<RectTransform> ().sizeDelta = new Vector2 (70, 45);
					}
				}
			}
		}
	}

	public void SpawnCurrentPart(){
		if (buildScript.rotateMode == false && buildScript.numChildrens == buildScript.numPartsSpawned + 3 && invScript.invParts.Count > 0) {
			InAudio.Play (transform.root.gameObject, spawnSound, null);
			crateScript = invScript.invParts [0].GetComponent<CrateScript> ();
            //TODO: put analytics hook here.
			GameObject newPart = (GameObject) Instantiate(crateScript.myPart, spawnTrans.position, transform.rotation);
            string crateType;
            switch (invScript.invParts[0].tag)
            {
                case "LootCrate01":
                    crateType = "steelCrate";
                    break;
                case "LootCrate02":
                    crateType = "bronzeCrate";
                    break;
                case "LootCrate03":
                    crateType = "diamondCrate";
                    break;
                default:
                    crateType = "";
                    break;
            }
            if (transform.parent.tag == "Player01")
            {
                Analytics.CustomEvent("Player 1 Parts Received", new Dictionary<string, object>
                {
                    { "player", transform.parent.tag },
                    { "crateType", crateType },
                    { "itemType", newPart },
                    { "time", DateTime.Now }
                });
            }
            else
            {
                Analytics.CustomEvent("Player 2 Parts Received", new Dictionary<string, object>
                {
                    { "player", transform.parent.tag },
                    { "crateType", crateType },
                    { "itemType", newPart },
                    { "time", DateTime.Now }
                });
            }
			CheckConnection[] checks = newPart.GetComponentsInChildren<CheckConnection>();
			foreach(CheckConnection script in checks){
				script.buildScript = buildScript;
			}

			if (transform.parent.tag == "BuildCanvas01") {
				newPart.GetComponent<Animator> ().SetTrigger ("Player02");
				newPart.GetComponentInChildren<HighlightSpriter> ().p2 = true;
			}

			BumpCheck check = newPart.GetComponentInChildren<BumpCheck> ();
			if (check != null) {
				check.buildScript = buildScript;
			}


			ThrusterRotateConn[] thrustConns = newPart.GetComponentsInChildren<ThrusterRotateConn> ();
			if (thrustConns.Length > 0) {
				foreach (ThrusterRotateConn thrust in thrustConns){
					thrust.buildScript = buildScript;
				}
			}

			newPart.GetComponentInChildren<GridConnection> ().buildScript = buildScript;
			newPart.GetComponent<PolygonCollider2D> ().isTrigger = true;
			buildScript.numPartsSpawned++;
			invScript.invParts.RemoveAt (0);
			foreach (Transform crateBG in transform) {
				foreach (Transform crateUI in crateBG) {
					if (crateUI.GetComponent<Image> () != null) {
						Destroy (crateUI.gameObject);
					}
				}
			}
			SpawnLaneParts ();
		}
	}
}
