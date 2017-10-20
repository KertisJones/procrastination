using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {

	public int health;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			Destroy (this.gameObject);
			GameObject gameControl = GameObject.Find("GameControl");
			BuyGU gScript = gameControl.GetComponent<BuyGU>();
			gScript.unitCount--;
		}
	}

	public bool takeDamage(int damage){
		health -= damage;
		Debug.Log ("Unit down to " + health + " health!!!");
		if (health <= 0)
			return true;
		return false;
	}
}
