using UnityEngine;
using System.Collections;

public class HighlightSpriter : MonoBehaviour {

	public bool p2;
	public Sprite p2HighlightSprite;

	// Use this for initialization
	void Start () {
		if (p2) {
			gameObject.GetComponent<SpriteRenderer> ().sprite = p2HighlightSprite;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
