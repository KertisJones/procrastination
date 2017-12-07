using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrateScript : MonoBehaviour {

	private GameObject newSprite;
	public GameObject spawnSprite;
	public GameObject myPart;
	public float partNumber;
	public GameObject hullPrefab;
	public float hullChance;
	public GameObject thrusterPrefab;
	public float thrusterChance;
	public GameObject machinePrefab;
	public float machineChance;
	public GameObject lazerPrefab;
	public float lazerChance;
	public GameObject shotgunPrefab;
	public float shotgunChance;
	public GameObject missilePrefab;
	public float missileChance;
	public GameObject plasmaPrefab;
	public float plasmaChance;


	// Use this for initialization
	void Start () {
		StartCoroutine (KillSprite ());
		newSprite = (GameObject)Instantiate (spawnSprite, transform.position, transform.rotation);
	}

	IEnumerator KillSprite(){
		yield return new WaitForSeconds (10);
		Destroy (newSprite.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if (newSprite != null) {
			newSprite.transform.position = transform.position;
		}

        float rand_num = Random.value;
        switch (transform.root.tag)
        {
            case "LootCrate01":
                if (rand_num > 0.7f)
                {
                    myPart = hullPrefab;
                }
                
                else if (rand_num > 0.45f)
                {
                    myPart = lazerPrefab;
                }
                else if (rand_num > 0.25f)
                {
                    myPart = thrusterPrefab;
                }
                else if (rand_num > 0.05f)
                {
                    myPart = shotgunPrefab;
                }
                else if (rand_num > 0.01f)
                {
                    myPart = plasmaPrefab;
                }
                else if (rand_num > 0.0f)
                {
                    myPart = machinePrefab;
                }
                break;
            case "LootCrate02":
                if (rand_num > 0.8f)
                {
                    myPart = lazerPrefab;
                }
                else if (rand_num > 0.6f)
                {
                    myPart = shotgunPrefab;
                }
                else if (rand_num > 0.45f)
                {
                    myPart = hullPrefab;
                }
                else if (rand_num > 0.30f)
                {
                    myPart = machinePrefab;
                }
                else if (rand_num > 0.15f)
                {
                    myPart = plasmaPrefab;
                }
                else if (rand_num > 0.05f)
                {
                    myPart = missilePrefab;
                }
                else if (rand_num > 0.00f)
                {
                    myPart = thrusterPrefab;
                }
                break;
            case "LootCrate03":
                if (rand_num > 0.7f)
                {
                    myPart = machinePrefab;
                }
                else if (rand_num > 0.45f)
                {
                    myPart = plasmaPrefab;
                }
                else if (rand_num > 0.25f)
                {
                    myPart = missilePrefab;
                }
                else if (rand_num > 0.05f)
                {
                    myPart = shotgunPrefab;
                }
                else if (rand_num > 0.01f)
                {
                    myPart = lazerPrefab;
                }
                else if (rand_num > 0.00f)
                {
                    myPart = hullPrefab;
                }
                break;
            default:
                break;
        }
	}
}
