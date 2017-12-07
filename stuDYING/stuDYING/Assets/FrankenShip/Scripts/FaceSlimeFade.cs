using UnityEngine;
using System.Collections;

public class FaceSlimeFade : MonoBehaviour {

	float currentTime01;
	public float lifeTime;
	Color startColor;

	// Use this for initialization
	void Start () {
		startColor = gameObject.GetComponent<SpriteRenderer> ().color;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentTime01 < lifeTime) {
			currentTime01 += Time.unscaledDeltaTime;
			float lerp = currentTime01 / lifeTime;
			gameObject.GetComponent<SpriteRenderer> ().color = Color.Lerp (startColor, Color.clear, lerp);
		}
	}
}
