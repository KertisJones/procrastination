using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DevPartSpawn : MonoBehaviour {

	public ShipBuilder buildScript;
	public GameObject myPart;
	public Sprite myGraySprite;
	public InAudioNode spawnSound;
	public Transform spawnTrans;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.parent.transform.parent.tag == "BuildCanvas01") {
			buildScript = GameObject.FindGameObjectWithTag("ShipBuilder01").GetComponent<ShipBuilder>();
		} else{
			buildScript = GameObject.FindGameObjectWithTag("ShipBuilder02").GetComponent<ShipBuilder>();
		}
	}

	public void SpawnDevPart(){
		GameObject newPart = (GameObject) Instantiate(myPart, spawnTrans.position, transform.rotation);

		if (transform.parent.transform.parent.tag == "BuildCanvas01") {
			newPart.GetComponent<Animator> ().SetTrigger ("Player02");
			newPart.GetComponentInChildren<HighlightSpriter> ().p2 = true;
		} 

		CheckConnection[] checks = newPart.GetComponentsInChildren<CheckConnection>();
		foreach(CheckConnection script in checks){
			script.buildScript = buildScript;
		}
			
		BumpCheck[] bumpers = newPart.GetComponentsInChildren<BumpCheck> ();
		if (bumpers.Length > 0) {
			foreach (BumpCheck bumper in bumpers){
				bumper.buildScript = buildScript;
			}

		}


		ThrusterRotateConn[] thrustConns = newPart.GetComponentsInChildren<ThrusterRotateConn> ();
		if (thrustConns.Length > 0) {
			foreach (ThrusterRotateConn thrust in thrustConns){
				thrust.buildScript = buildScript;
			}
		}

		newPart.GetComponentInChildren<GridConnection> ().buildScript = buildScript;
		newPart.GetComponent<PolygonCollider2D> ().isTrigger = true;
		if (transform.parent.tag == "BuildCanvas02") {
			newPart.GetComponent<Animator> ().SetTrigger ("Player02");
		}
		buildScript.numPartsSpawned++;
		InAudio.Play (gameObject, spawnSound, null);
	}

	public void GreyButton(){
		gameObject.GetComponent<Image> ().sprite = myGraySprite;
		gameObject.GetComponent<Image> ().color = Color.gray * 0.75f;
	}
}
