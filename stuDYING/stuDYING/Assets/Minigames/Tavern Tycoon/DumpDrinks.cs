using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumpDrinks : MonoBehaviour {

    public PersistentScript persistentScript;

    public bool dumpDrink = true;

	// Use this for initialization
	void Start () {
        persistentScript = GameObject.FindGameObjectWithTag("Persistent Object").GetComponent<PersistentScript>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        if (dumpDrink)
        {
            persistentScript.playerDrink1 = 0;
            this.transform.parent.transform.position = new Vector3(60, -20, 0);
        }
        else
        {
            this.transform.parent.transform.position = new Vector3(60, -20, 0);
        }
    }
}
