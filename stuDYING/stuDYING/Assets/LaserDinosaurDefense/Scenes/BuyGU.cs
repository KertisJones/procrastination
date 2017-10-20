using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TDTK;
public class BuyGU : MonoBehaviour {
	//public ResourceManager ResourceManager;
	public GameObject GUnit;
	//public List<int> price = new List<int>();
	public float x = -10f;
	public float y = 0.4f;
	public float z = -15f;
	public int unitCount = 0;
	bool boughtFirst = false;
	public GroundUnitInputManager manager;

	// Use this for initialization
	void Start () {
		//price.Add(200);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")) {
			//int suffCost=ResourceManager.HasSufficientResource(price);
			//Debug.Log (suffCost);
			//if (suffCost == -1) {
			//	ResourceManager.SpendResource (price);
			if(manager.hasMoved == true || boughtFirst == false){
				if (unitCount < 17) {
						if (boughtFirst == false) {
							boughtFirst = true;
						}
						Instantiate (GUnit, new Vector3 (x, y, z), GUnit.transform.rotation);
						unitCount++;
						manager.hasMoved = false;
					}
				}
			//}
			//Debug.Log (price.Count);
		}
		//Debug.Log (unitCount);
	}
}
