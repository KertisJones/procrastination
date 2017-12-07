using UnityEngine;
using System.Collections;

public class SpaceSlimeSlow : MonoBehaviour {
	
	public Color slimeColor02;
	public Sprite slimeSprite02;
	public Transform myPlayer;
	SpriteRenderer mySpriter;
	float myLifeTime;
	float myFadeTime;
	float opaqueTime;
	float currentTime01;
	bool fade;
	Color startColor;

	// Use this for initialization
	void Start () {
		myLifeTime = 10.0f;
		myFadeTime = 2.0f;
		opaqueTime = myLifeTime - myFadeTime;
		mySpriter = gameObject.GetComponent<SpriteRenderer> ();
		if (myPlayer.tag == "Player02") {
			mySpriter.color = slimeColor02;
		}
		startColor = mySpriter.color;
		StartCoroutine (OpaqueWait ());
	}
	
	// Update is called once per frame
	void Update () {
		if (fade) {
			if (currentTime01 < myFadeTime) {
				currentTime01 += Time.unscaledDeltaTime;
				float lerp = currentTime01 / myFadeTime;
				mySpriter.color = Color.Lerp (startColor, Color.clear, lerp);
			} else {
				Destroy (gameObject);
			}
		}
	}

	void OnTriggerEnter2D (Collider2D other){
		if (!other.isTrigger && other.transform.root != myPlayer) {
			other.transform.root.GetComponent<PowerUpCoordinator> ().StartSlimeSlow ();
			Destroy (gameObject);
		}
	}

	IEnumerator OpaqueWait(){
		yield return new WaitForSeconds (opaqueTime);
		fade = true;
	}
}
