using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellDrink : MonoBehaviour {

    public PersistentScript persistentScript;
    public CustomerInteractionMaster customerInterationMaster;

    public Text sellDrinkText;
    private bool sellWater = false;

    //public int ammountSold = 1;
    private float randomTotal;
    private float randomB;
    private float randomY;
    private float randomH;

    private int ingrTotal;

    public float waterPerc;
    public float barleyPerc;
    public float yeastPerc;
    public float hopsPerc;

    private float drinkPrice;

    // Use this for initialization
    void Start()
    {
        persistentScript = GameObject.FindGameObjectWithTag("Persistent Object").GetComponent<PersistentScript>();
        customerInterationMaster = GameObject.FindGameObjectWithTag("Customer Interface").GetComponent<CustomerInteractionMaster>();
        sellDrinkText = GameObject.FindGameObjectWithTag("Sell Drink Text").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        ingrTotal = persistentScript.playerDrink1WaterCost + persistentScript.playerDrink1BarleyCost + persistentScript.playerDrink1YeastCost + persistentScript.playerDrink1HopsCost;
        waterPerc = (persistentScript.playerDrink1WaterCost / ingrTotal) * 100;
        barleyPerc = (persistentScript.playerDrink1BarleyCost / ingrTotal) * 100;
        yeastPerc = (persistentScript.playerDrink1YeastCost / ingrTotal) * 100;
        hopsPerc = (persistentScript.playerDrink1HopsCost / ingrTotal) * 100;


        if (persistentScript.playerDrink1 == 0 || waterPerc == 100)
        {
            sellWater = true;
            sellDrinkText.text = "Sell Water";
        }
        else
        {
            sellWater = false;
            sellDrinkText.text = "Sell Drink";
        }
    }

    void OnMouseDown()
    {
        if (customerInterationMaster.isCustomerMenuOpen)
        {
            randomB = Random.Range(15, 100); //Random number representing Barley, Yeast, and Hops
            randomY = Random.Range(15, 100);
            randomH = Random.Range(15, 100);
            randomTotal = randomB + randomY + randomH; //Add the total of all 3 random numbers
            randomB = (randomB / randomTotal) * 100; //Get the percentage of each random number (min is 6%)
            randomY = (randomY / randomTotal) * 100;
            randomH = (randomH / randomTotal) * 100;
            randomB = Mathf.Abs(barleyPerc - randomB); //Calculate the distance between the actual percentage and the random percentage
            randomY = Mathf.Abs(yeastPerc - randomY);
            randomH = Mathf.Abs(hopsPerc - randomH);

            drinkPrice = persistentScript.playerDrink1Price;

            if (ingrTotal > persistentScript.playerDrink1WaterCost)
            {
                drinkPrice += persistentScript.playerDrink1BarleyCost;
                drinkPrice += persistentScript.playerDrink1YeastCost;
                drinkPrice += persistentScript.playerDrink1HopsCost;
            }
            else
            {
                drinkPrice /= 2;
            }

            if (randomB <= 1)
            {
                drinkPrice = drinkPrice * 2;
            }
            else if (randomB <= 5)
            {
                drinkPrice = drinkPrice * 1.5f;
            }
            else if (randomB <= 10)
            {
                drinkPrice = drinkPrice * 1.25f;
            }

            if (randomY <= 1)
            {
                drinkPrice = drinkPrice * 2;
            }
            else if (randomY <= 5)
            {
                drinkPrice = drinkPrice * 1.5f;
            }
            else if (randomY <= 10)
            {
                drinkPrice = drinkPrice * 1.25f;
            }

            if (randomH <= 1)
            {
                drinkPrice = drinkPrice * 2;
            }
            else if (randomH <= 5)
            {
                drinkPrice = drinkPrice * 1.5f;
            }
            else if (randomH <= 10)
            {
                drinkPrice = drinkPrice * 1.25f;
            }

            if (ingrTotal > persistentScript.playerDrink1WaterCost)
            {
                drinkPrice += persistentScript.playerDrink1BarleyCost;
                drinkPrice += persistentScript.playerDrink1YeastCost;
                drinkPrice += persistentScript.playerDrink1HopsCost;
            }

            drinkPrice = drinkPrice * (1 - (randomB / 100));
            drinkPrice = drinkPrice * (1 - (randomY / 100));
            drinkPrice = drinkPrice * (1 - (randomH / 100));

            drinkPrice = drinkPrice * (1 - (waterPerc / 100));


            if (drinkPrice < 1)
            {
                drinkPrice = 1;
            }
            else if (drinkPrice > 30)
            {
                drinkPrice = 30;
            }

            //--------------------------------------------------------------------------------------------

            if (persistentScript.playerDrink1 >= 1)
            {
                persistentScript.playerDrink1 -= 1;

                if (sellWater)
                {
                    persistentScript.playerGold += 1;
                }
                else
                {
                    persistentScript.playerGold += Mathf.FloorToInt(drinkPrice);
                    //persistentScript.playerDrink1Price;
                }

                this.GetComponent<AudioSource>().Play();
                customerInterationMaster.soldDrinkTrigger = true;
            }
            else if (sellWater)
            {
                persistentScript.playerGold += 1;

                this.GetComponent<AudioSource>().Play();
                customerInterationMaster.soldDrinkTrigger = true;
            }
        }
    }
}
