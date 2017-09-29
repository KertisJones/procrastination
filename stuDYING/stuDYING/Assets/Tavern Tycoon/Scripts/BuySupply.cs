using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class BuySupply : MonoBehaviour {

    public PersistentScript persistentScript;

    public enum SupplyType { Water, Barley, Yeast, Hops };
    public SupplyType supplyType;

    public int price = 10;
    public int yield = 1;


    // Use this for initialization
    void Start () {
        persistentScript = GameObject.FindGameObjectWithTag("Persistent Object").GetComponent<PersistentScript>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        if (supplyType == SupplyType.Water && persistentScript.playerGold >= price)
        {
            persistentScript.playerGold -= price;
            persistentScript.playerWater += yield;
            this.GetComponent<AudioSource>().Play();
        }
        else if (supplyType == SupplyType.Barley && persistentScript.playerGold >= price)
        {
            persistentScript.playerGold -= price;
            persistentScript.playerBarley += yield;
            this.GetComponent<AudioSource>().Play();
        }
        else if (supplyType == SupplyType.Yeast && persistentScript.playerGold >= price)
        {
            persistentScript.playerGold -= price;
            persistentScript.playerYeast += yield;
            this.GetComponent<AudioSource>().Play();
        }
        else if (supplyType == SupplyType.Hops && persistentScript.playerGold >= price)
        {
            persistentScript.playerGold -= price;
            persistentScript.playerHops += yield;
            this.GetComponent<AudioSource>().Play();
        }
    }
}
