using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class BrewDrink : MonoBehaviour {

    public PersistentScript persistentScript;
    //public int drinkPrice;

    public int ammountBrewed = 1;
    // Use this for initialization
    void Start () {
        persistentScript = GameObject.FindGameObjectWithTag("Persistent Object").GetComponent<PersistentScript>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        if (persistentScript.playerWater >= persistentScript.playerDrink1WaterCost * ammountBrewed
            && persistentScript.playerBarley >= persistentScript.playerDrink1BarleyCost * ammountBrewed
            && persistentScript.playerYeast >= persistentScript.playerDrink1YeastCost * ammountBrewed
            && persistentScript.playerHops >= persistentScript.playerDrink1HopsCost * ammountBrewed)
        {
            persistentScript.playerWater -= persistentScript.playerDrink1WaterCost * ammountBrewed;
            persistentScript.playerBarley -= persistentScript.playerDrink1BarleyCost * ammountBrewed;
            persistentScript.playerYeast -= persistentScript.playerDrink1YeastCost * ammountBrewed;
            persistentScript.playerHops -= persistentScript.playerDrink1HopsCost * ammountBrewed;

            //persistentScript.playerGold += drinkPrice;
            persistentScript.playerDrink1 += ammountBrewed;

            this.GetComponent<AudioSource>().Play();
        }
    }
}
