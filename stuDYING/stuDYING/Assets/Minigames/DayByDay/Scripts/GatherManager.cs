using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherManager : MonoBehaviour {
	public float gatherTimer = 5f;
	bool nearResource = false;

	public AudioManager audioManager;

	// Use this for initialization
	void Start () {
		audioManager = GameObject.Find ("AudioManager").GetComponent<AudioManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (nearResource == true) {
			if (Input.GetButton ("Gather")) {
				gatherTimer -= Time.deltaTime;
				Debug.Log (gatherTimer);
				if(!this.GetComponent<AudioSource>().isPlaying)
					this.GetComponent<AudioSource> ().Play ();
			} else
				this.GetComponent<AudioSource> ().Pause ();
		} else
			this.GetComponent<AudioSource>().Pause ();
		
		if (gatherTimer < 0) {
			//Destroy (gameObject);
			this.GetComponent<AudioSource> ().Pause ();
			//GameObject player = GameObject.Find ("Player");
			//var playerScript = player.GetComponent<Player>();
			if (this.gameObject.CompareTag ("Tree")) {
				//Debug.Log (playerScript.LumberSupply);
				//playerScript.LumberSupply++;
				//Debug.Log (playerScript.LumberSupply);
				Debug.Log (Player.LumberSupply);
				Player.LumberSupply++;
				Player.LumberSold++;
				Debug.Log (Player.LumberSupply);
				audioManager.WoodCollectSuccess ();
			} else if (this.gameObject.CompareTag ("Berry")) {
				//Debug.Log (playerScript.BerrySupply);
				//playerScript.BerrySupply++;
				//Debug.Log (playerScript.BerrySupply);
				Debug.Log (Player.BerrySupply);
				Player.BerrySupply++;
				Player.BerrySold++;
				Debug.Log (Player.BerrySupply);
				audioManager.BerriesCollectSuccess ();
			} else if (this.gameObject.CompareTag ("Coal")) {
				//Debug.Log (playerScript.CoalSupply);
				//playerScript.CoalSupply++;
				//Debug.Log (playerScript.CoalSupply);
				Debug.Log (Player.CoalSupply);
				Player.CoalSupply++;
				Player.CoalSold++;
				Debug.Log (Player.CoalSupply);
				audioManager.CoalCollectSuccess ();
			}
			Destroy (gameObject);
			Debug.Log ("Item Gathered!");
			nearResource = false;
		}
	}
	void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject.CompareTag ("Player")) {
			Debug.Log ("It's the player!");
			nearResource = true;
		}
	}
	void OnTriggerExit2D (Collider2D other){
		if (other.gameObject.CompareTag ("Player")) {
			Debug.Log ("Player Left");
			nearResource = false;
		}
	}
}
