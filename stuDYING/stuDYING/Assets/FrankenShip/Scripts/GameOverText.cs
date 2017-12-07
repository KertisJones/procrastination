using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverText : MonoBehaviour {

	Text myText;
	GameObject player01;
	GameObject player02;
	HullHealth p1Health;
	HullHealth p2Health;

	// Use this for initialization
	void Start () {
		myText = gameObject.GetComponent<Text> ();
		player01 = GameObject.FindWithTag ("Player01");
		player02 = GameObject.FindWithTag ("Player02");
		p1Health = player01.transform.GetChild(0).GetComponent<HullHealth> ();
		p2Health = player02.transform.GetChild(0).GetComponent<HullHealth> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (p1Health.myHealth <= 0) {
			myText.text = "Game Over!! Player Two Wins!";
		}
		else if (p2Health.myHealth <= 0) {
			myText.text = "Game Over!! Player One Wins!";

		}
	}
}
