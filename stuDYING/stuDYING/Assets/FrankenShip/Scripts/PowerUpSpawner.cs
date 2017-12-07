using UnityEngine;
using System.Collections;

public class PowerUpSpawner : MonoBehaviour {

	public GameObject[] pups;
	GameObject[] spawnedPups;
	private int maxSpawnedPups;
	bool timerStarted;

	// Use this for initialization
	void Start () {
		maxSpawnedPups = 5;
	}
	
	// Update is called once per frame
	void Update () {
		spawnedPups = GameObject.FindGameObjectsWithTag ("PowerUp");
		if (!timerStarted && spawnedPups.Length < maxSpawnedPups) {
			StartCoroutine (SpawnPowerUp ());
		}
	}

	IEnumerator SpawnPowerUp(){
		timerStarted = true;
		GameObject myPup = pups [Random.Range (0, pups.Length)];
		Vector3 myPos = new Vector3 (Random.Range (-60, 60), Random.Range (-60, 60), 0.0f);
		GameObject pup = (GameObject)Instantiate (myPup, myPos, transform.rotation);
		yield return new WaitForSeconds (Random.Range (5.0f, 10.0f));
		timerStarted = false;
	}
}
