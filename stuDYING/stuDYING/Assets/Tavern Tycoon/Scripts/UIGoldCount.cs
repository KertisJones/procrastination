using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIGoldCount : MonoBehaviour {

    public Text textGold;
    public PersistentScript persistentScript;

    

    // Use this for initialization
    void Start()
    {
        textGold = GetComponent<Text>();
        persistentScript = GameObject.FindGameObjectWithTag("Persistent Object").GetComponent<PersistentScript>();
    }

    // Update is called once per frame
    void Update()
    {
        textGold.text = "Gold: " + persistentScript.playerGold + "\n"
            + "Barley: " + persistentScript.playerBarley + "\n"
            + "Yeast: " + persistentScript.playerYeast + "\n"
            + "Hops: " + persistentScript.playerHops + "\n"
            + "Drinks: " + persistentScript.playerDrink1;

    }
}
