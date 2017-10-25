using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using TDTK;

public class EnemyUnitController : MonoBehaviour {

	public Transform destination;

	public int damage;
	public float reloadTime;
	float timer = 0f;

	bool targetAcquired = false;
	GameObject target;
	NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		//if (this.gameObject.layer == 30 || this.gameObject.layer == 31)
			//damage = this.gameObject.GetComponent<UnitCreep> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (targetAcquired) {
			timer -= Time.deltaTime;
			enemyAttack ();
			if (target.gameObject.layer == 8)
				agent.SetDestination (agent.gameObject.transform.position);
		} //else
			//agent.SetDestination (destination.position);
	}
	
	void OnTriggerEnter(Collider other){
		if (!targetAcquired || other.gameObject.layer == 8) {
			if (other.gameObject.layer == 9) { //Portal
				Debug.Log ("ROAAAAAARRR!");
				Destroy (other.gameObject);
			} else if (other.gameObject.layer == 29) { //Tower
				Debug.Log ("Dino Dan Roars at " + other.gameObject.name + "!");
				targetAcquired = true;
				//target = other.gameObject.GetComponent<HealthManager>();
				target = other.gameObject;
				//Destroy (other.gameObject);
			} else if (other.gameObject.layer == 8) { //Ground Unit
				Debug.Log ("Dino Dan Roars at " + other.gameObject.name + "!");
				targetAcquired = true;
				//target = other.gameObject.GetComponent<HealthManager>();
				//Destroy (other.gameObject);
			}
		}
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject == target.gameObject) {
			target = null;
			targetAcquired = false;
			timer = 0f;
		}
	}

	void enemyAttack(){
		if (timer <= 0) {
			Debug.Log ("Dino Dan attacks for " + damage + " damage!");
			timer = reloadTime;
			bool targetKilled = false;
			if (target.layer == 29)
				targetKilled = target.GetComponent<UnitTower>().takeDamage (damage);
			else if(target.layer == 8)
				targetKilled = target.GetComponent<HealthManager>().takeDamage(damage);
			if (targetKilled)
				targetAcquired = false;
		}
	}
}
