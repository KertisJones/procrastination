using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStat : MonoBehaviour {

    private Text txt;
    private PersistentScript persistentScript;

    public enum StatType { Water, Barley, Yeast, Hops, Gold, Drink1, Drink1WaterCost, Drink1BarleyCost, Drink1YeastCost, Drink1HopsCost };
    public StatType statType;
    // Use this for initialization
    void Start()
    {
        txt = GetComponent<Text>();
        persistentScript = GameObject.FindGameObjectWithTag("Persistent Object").GetComponent<PersistentScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (statType == StatType.Water)
        {

            txt.text = "Water: " + persistentScript.playerWater;
        }
        else if (statType == StatType.Barley)
        {
            txt.text = "Barley: " + persistentScript.playerBarley;
        }
        else if (statType == StatType.Yeast)
        {
            txt.text = "Yeast: " + persistentScript.playerYeast;
        }
        else if (statType == StatType.Hops)
        {
            txt.text = "Hops: " + persistentScript.playerHops;
        }
        else if (statType == StatType.Gold)
        {
            txt.text = "Gold: " + persistentScript.playerGold;
        }
        else if (statType == StatType.Drink1)
        {
            txt.text = "Drinks: " + persistentScript.playerDrink1;
        }
        else if (statType == StatType.Drink1WaterCost)
        {
            txt.text = "Ammount of \n Water: " + persistentScript.playerDrink1WaterCost;
        }
        else if (statType == StatType.Drink1BarleyCost)
        {
            txt.text = "Ammount of \n Barley: " + persistentScript.playerDrink1BarleyCost;
        }
        else if (statType == StatType.Drink1YeastCost)
        {
            txt.text = "Ammount of \n Yeast: " + persistentScript.playerDrink1YeastCost;
        }
        else if (statType == StatType.Drink1HopsCost)
        {
            txt.text = "Ammount of \n Hops: " + persistentScript.playerDrink1HopsCost;
        }
    }
}
