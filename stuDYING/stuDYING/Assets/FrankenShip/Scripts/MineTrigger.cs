using UnityEngine;
using System.Collections;

public class MineTrigger : MonoBehaviour {

	MineExplosion expScript;
	SpriteRenderer mySprite;
	BlackHoleObjectState stateScript;
	public Color expStartColor;
	public Color expEndColor;
	public bool expTrigger;
	private float currentTime = 0.0f;
	public float expWaitTime = 2.0f;

	// Use this for initialization
	void Start () {
		expScript = transform.root.GetComponentInChildren<MineExplosion> ();
		mySprite = transform.root.GetComponent<SpriteRenderer> ();
		stateScript = transform.root.GetComponent<BlackHoleObjectState> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (expTrigger) {
			if (currentTime <= expWaitTime) {
				currentTime += Time.deltaTime;
				mySprite.color = Color.Lerp (expStartColor, expEndColor, currentTime / expWaitTime);
			} else {
				expScript.explode = true;
				expTrigger = false;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (!other.isTrigger && other.transform.root != transform.root) {
			expTrigger = true;
		}
	}
}
