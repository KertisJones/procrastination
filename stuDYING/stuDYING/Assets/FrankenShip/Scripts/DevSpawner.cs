using UnityEngine;
using System.Collections;

public class DevSpawner : MonoBehaviour {

	public GameObject lootCrate01;
	public GameObject lootCrate02;
	public GameObject lootCrate03;
	public GameObject asteroid01;
	public GameObject asteroid02;
	public GameObject asteroid03;
	public GameObject spaceMine;
	public GameObject pupAttack;
	public GameObject pupMove;
	public GameObject pupShield;
	public GameObject pupRepair;
	public GameObject pupSlime;
	public GameObject pupMine;
	GameObject player01;
	GameObject player02;
	GameObject currentPlayer;
	Vector3 spawnPosition;


	// Use this for initialization
	void Start () {
		player01 = GameObject.FindWithTag ("Player01");
		player02 = GameObject.FindWithTag ("Player02");
		currentPlayer = player01;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Alpha0)){
			if (currentPlayer == player01){
				currentPlayer = player02;
			} else{
				currentPlayer = player01;
			}
		}
		spawnPosition = currentPlayer.transform.position + currentPlayer.transform.rotation * Vector2.up * 7.5f;
		if (PhaseNavigator.battlePhase) {
			if (Input.GetKeyDown (KeyCode.Alpha1)) {
				Instantiate (pupAttack, spawnPosition, transform.rotation);
			}
			if (Input.GetKeyDown (KeyCode.Alpha2)) {
				Instantiate (pupMove, spawnPosition, transform.rotation);
			}
			if (Input.GetKeyDown (KeyCode.Alpha3)) {
				Instantiate (pupShield, spawnPosition, transform.rotation);
			}
			if (Input.GetKeyDown (KeyCode.Alpha4)) {
				Instantiate (pupRepair, spawnPosition, transform.rotation);
			}
			if (Input.GetKeyDown (KeyCode.Alpha5)) {
				Instantiate (pupSlime, spawnPosition, transform.rotation);
			}
			if (Input.GetKeyDown (KeyCode.Alpha6)) {
				Instantiate (pupMine, spawnPosition, transform.rotation);
			}
		} else {
				
				if (Input.GetKeyDown (KeyCode.Alpha1)) {
					Instantiate (lootCrate01, spawnPosition, transform.rotation);
				}
				if (Input.GetKeyDown (KeyCode.Alpha2)) {
					Instantiate (lootCrate02, spawnPosition, transform.rotation);
				}
				if (Input.GetKeyDown (KeyCode.Alpha3)) {
					Instantiate (lootCrate03, spawnPosition, transform.rotation);
				}
				if (Input.GetKeyDown (KeyCode.Alpha4)) {
					Instantiate (asteroid01, spawnPosition, transform.rotation);
				}
				if (Input.GetKeyDown (KeyCode.Alpha5)) {
					Instantiate (asteroid02, spawnPosition, transform.rotation);
				}
				if (Input.GetKeyDown (KeyCode.Alpha6)) {
					Instantiate (asteroid03, spawnPosition, transform.rotation);
				}
				if (Input.GetKeyDown (KeyCode.Alpha7)) {
					Instantiate (spaceMine, spawnPosition, transform.rotation);
				}
		}
	
	}
}
