using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour {
	public GameObject customer;
	public GameObject[] line;
	public int customersLeft;
	//public Player Player;
	public UIManager costs;
	//public GameObject AudioManager;
	public AudioManager audioManager;

	public GameObject gameManager;

	// Use this for initialization
	void Start () {
		//Player = GameManager.player;
		//Player = gameManager.GetComponent<GameManager>().player;
		audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
		this.customersLeft = GameManager.customersLeft;
		this.line = new GameObject[customersLeft];
		Debug.Log (customersLeft);
		for (int i = 0; i < customersLeft; i++) {
			line [i] = Instantiate (customer,
									new Vector3 (this.transform.position.x - (i * 1),
												 this.transform.position.y),
									this.transform.rotation);
			line [i].GetComponent<Customer> ().transform.localScale = new Vector3 (0.2f, 0.2f, 0.2f);
		}
		//Makes first customer state their request
		if (customersLeft>0)
			line [0].GetComponent<Customer> ().Request ();
	}

	//Deals with variables once a customer has been served
	public void CustomerServed(){
		Destroy (line [0]);
		//Shifts all customers down the line after the lead customer is served
		for (int i = 0; i < this.customersLeft; i++) {
			//If on the last customer on the list, sets it to null
			if (i == customersLeft - 1) {
				line [i] = null;
				this.customersLeft--;

			//Otherwise, move the customer up one place in the line
			} else if (line [i] != null) {
				line [i] = line [i + 1];
				line [i].transform.position = new Vector3 (line [i].transform.position.x + 1,
															line [i].transform.position.y,
															line [i].transform.position.z);
			}
		}
		//Removes one from the remaining customer count
		GameManager.customersLeft--;
		Debug.Log ("Customers Remaining: " + GameManager.customersLeft);

		//Has the next customer at the head state their request
		if(GameManager.customersLeft > 0)
			line [0].GetComponent<Customer> ().Request ();
	}

	public void TransAccept(){
		if (GameManager.customersLeft > 0) {
			int amount = line [0].GetComponent<Customer> ().amount;
			int max = line [0].GetComponent<Customer> ().maxVal;
			switch (line [0].GetComponent<Customer> ().resource) {
			case "wood":
				if (Player.LumberSupply >= amount && costs.wCost <= max) {
					Player.LumberSupply -= amount;
					Player.LumberSold += amount;
					Player.MoneySupply += costs.wCost * amount;
					Player.MoneyMade += costs.wCost * amount;
					Player.CustomersServed++;
					audioManager.AcceptSale ();
				} else {
					audioManager.DeclineSale ();
					//Debug.Log ("The customer leaves in a rage, ranting about your lack of stock at a reasonable price...");
				}
				break;
			case "berries":
				if (Player.BerrySupply >= amount && costs.bCost <= max) {
					Player.BerrySupply -= amount;
					Player.BerrySold += amount;
					Player.MoneySupply += costs.bCost * amount;
					Player.MoneyMade += costs.bCost * amount;
					audioManager.AcceptSale ();
					Player.CustomersServed++;
				} else {	
					audioManager.DeclineSale ();
					//Debug.Log ("The customer leaves in a rage, ranting about your lack of stock at a reasonable price...");
				}
				break;
			default:
				if (Player.CoalSupply >= amount && costs.cCost <= max) {
					Player.CoalSupply -= amount;
					Player.CoalSold += amount;
					Player.MoneySupply += costs.cCost * amount;
					Player.MoneyMade += costs.cCost * amount;
					audioManager.AcceptSale ();
					Player.CustomersServed++;
				} else {
					audioManager.DeclineSale ();
					//Debug.Log ("The customer leaves in a rage, ranting about your lack of stock at a reasonable price...");
				}
				break;
			}
		}
		CustomerServed ();
	}

	public void TransDecline(){
		//Debug.Log ("Declined");
		audioManager.GetComponent<AudioManager> ().DeclineSale ();
		CustomerServed ();
	}

	// Update is called once per frame
	void Update () {
//		//If the space key is pressed, then a customer is served
//		if (Input.GetKeyDown (KeyCode.Space)) {
//			Debug.Log ("Key Pressed!");
//			CustomerServed ();
//		}
	}
}
