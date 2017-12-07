using UnityEngine;
using System.Collections;

public class BattleMineTrigger : MonoBehaviour {

	BattleMineExplosion expScript;
	public GameObject myPlayer;
	bool triggerActive = false;
	float currentTime;
	float triggerWaitTime;
	public Color expStartColor;
	public Color expEndColor;
	SpriteRenderer mySprite;

	// Use this for initialization
	void Start () {
		triggerWaitTime = 1.0f;
		expScript = transform.parent.GetComponentInChildren<BattleMineExplosion> ();
		mySprite = transform.parent.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (currentTime <= triggerWaitTime) {
			currentTime += Time.deltaTime;
			mySprite.color = Color.Lerp (expStartColor, expEndColor, currentTime / triggerWaitTime);
		} else {
			triggerActive = true;
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.layer == 8 && triggerActive) {
			expScript.enabled = true;
		}
	}
}
