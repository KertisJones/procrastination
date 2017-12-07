using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlackHoleSpawner : MonoBehaviour {


	public GameObject LootCrate01;
	public GameObject LootCrate02;
	public GameObject LootCrate03;
	public float crate01MinSpawnDelay;
	public float crate01MaxSpawnDelay;
	public float crate02MinSpawnDelay;
	public float crate02MaxSpawnDelay;
	public float crate03MinSpawnDelay;
	public float crate03MaxSpawnDelay;
	public float crate01MinSpawnDist;
	public float crate01MaxSpawnDist;
	public float crate02MinSpawnDist;
	public float crate02MaxSpawnDist;
	public float crate03MinSpawnDist;
	public float crate03MaxSpawnDist;

	public List<GameObject> Asteroids;
	public float asteroidMinSpawnDelay;
	public float asteroidMaxSpawnDelay;
	public float asteroidMinSpawnDist;
	public float asteroidMaxSpawnDist;

	public GameObject spaceMine;
	public float mineMinSpawnDelay;
	public float mineMaxSpawnDelay;
	public float mineMinSpawnDist;
	public float mineMaxSpawnDist;

	bool timer01Started;
	bool timer02Started;
	bool timer03Started;
	bool timer04Started;
	bool timer05Started;
	Quaternion rotation;

    
	Collider2D myColl;

	// Use this for initialization
	void Start () {
		//Asteroids = new List<GameObject> ();
		myColl = gameObject.GetComponent<CircleCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		rotation = Quaternion.Euler (0.0f, 0.0f, Random.Range (0.0f, 360.0f));
		if (timer01Started == false) {
			StartCoroutine(SpawnCrate01());
		}
		if (timer02Started == false) {
			StartCoroutine(SpawnCrate02());
		}
		if (timer03Started == false) {
			StartCoroutine(SpawnCrate03());
		}
		if (timer04Started == false) {
			StartCoroutine(SpawnAsteroid());
		}
		if (timer05Started == false) {
			StartCoroutine(SpawnMine());
		}

	}

	IEnumerator SpawnCrate01(){
		timer01Started = true;
		Vector2 spawnPos = Random.insideUnitCircle.normalized * Random.Range (crate01MinSpawnDist,crate01MaxSpawnDist);
		Instantiate(LootCrate01, spawnPos, rotation);
		yield return new WaitForSeconds (Random.Range(crate01MinSpawnDelay, crate01MaxSpawnDelay));
		timer01Started = false;
	}

	IEnumerator SpawnCrate02(){
		timer02Started = true;
		Vector2 spawnPos = Random.insideUnitCircle.normalized * Random.Range (crate02MinSpawnDist, crate02MaxSpawnDist);
		Instantiate(LootCrate02, spawnPos, rotation);
		yield return new WaitForSeconds (Random.Range(crate02MinSpawnDelay, crate02MaxSpawnDelay));
		timer02Started = false;
	}

	IEnumerator SpawnCrate03(){
		timer03Started = true;
		Vector2 spawnPos = Random.insideUnitCircle.normalized * Random.Range (crate03MinSpawnDist, crate03MaxSpawnDist);
		Instantiate(LootCrate03, spawnPos, rotation);
		yield return new WaitForSeconds (Random.Range(crate03MinSpawnDelay, crate03MaxSpawnDelay));
		timer03Started = false;
	}

	IEnumerator SpawnAsteroid(){
		timer04Started = true;
		Vector2 spawnPos = Random.insideUnitCircle.normalized * Random.Range (asteroidMinSpawnDist, asteroidMaxSpawnDist);
		Instantiate (Asteroids [Random.Range (0, Asteroids.Count)].gameObject, spawnPos, rotation);
		yield return new WaitForSeconds (Random.Range(asteroidMinSpawnDelay, asteroidMaxSpawnDelay));
        timer04Started = false;
	}
	IEnumerator SpawnMine(){
		timer05Started = true;
		Vector2 spawnPos = Random.insideUnitCircle.normalized * Random.Range (mineMinSpawnDist, mineMaxSpawnDist);
		Instantiate(spaceMine, spawnPos, rotation);
		yield return new WaitForSeconds (Random.Range(mineMinSpawnDelay, mineMaxSpawnDelay));
		timer05Started = false;
	}

}
