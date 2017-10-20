using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundUnitController : MonoBehaviour {

	public Camera camera;
	public Transform destination;
	NavMeshAgent agent;

	public int damage;
	public float reloadTime;
	float timer = 0f;

	bool targetAcquired = false;
	HealthManager target;

	CapsuleCollider combatTrigger;

	// Use this for initialization
	void Start () {
		combatTrigger = this.GetComponent<CapsuleCollider>();
		agent = GetComponent<NavMeshAgent> ();
		camera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		//if(agent.remainingDistance <= 0)
		//	moving = false;
		if (targetAcquired) {
			timer -= Time.deltaTime;
			enemyAttack ();
		}
	}

	void OnTriggerEnter(Collider other){
		if (!targetAcquired) {
			if (other.gameObject.tag == "Dino") {
				target = other.gameObject.GetComponent<HealthManager> ();
				targetAcquired = true;
				Debug.Log ("Clever Girl!");
				//Destroy (other.gameObject);
			}
		}
	}

	void OnTriggerExit(Collider other){
		target = null;
		targetAcquired = false;
		timer = 0f;
	}

	public void moveUnit(){
		//moving = true;
		Ray clickedRay = camera.ScreenPointToRay (Input.mousePosition);
		RaycastHit hitpoint;

		if (Physics.Raycast (clickedRay, out hitpoint))
			agent.SetDestination (hitpoint.point);
	}

	void enemyAttack(){
		if (timer <= 0) {
			Debug.Log (this.gameObject.name + " attacks for " + damage + " damage!");
			timer = reloadTime;
			bool targetKilled = target.takeDamage (damage);
			if (targetKilled)
				targetAcquired = false;
		}
	}
}
