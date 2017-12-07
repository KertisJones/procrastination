using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthVileFill : MonoBehaviour {

	public GameObject myPlayer;
	public HullHealth coreHealthScript;
	public Image myVile;
	public bool active;
	public float lerp;
	public float myHealth;
	public float myStartHealth;

	// Use this for initialization
	void Start () {
		StartCoroutine (startDelay ());
		coreHealthScript = myPlayer.GetComponentInChildren<HullHealth> ();
		myVile = gameObject.GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (active) {
			myHealth = coreHealthScript.myHealth;
			myStartHealth = coreHealthScript.startHealth;
			lerp = (myHealth/myStartHealth);
			myVile.fillAmount = Mathf.Lerp (0.08f, 0.9f, lerp);
		}
	}

	IEnumerator startDelay(){
		yield return new WaitForSeconds (0.5f);
		coreHealthScript = myPlayer.GetComponentInChildren<HullHealth> ();
		active = true;
	}
}
