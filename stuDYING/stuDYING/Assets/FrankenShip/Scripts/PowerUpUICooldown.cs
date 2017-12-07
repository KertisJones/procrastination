using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerUpUICooldown : MonoBehaviour {

	bool activatePup;
	bool fadePup;
	public bool coolDownActive;
	public bool nextPup;
	public float activateTime;
	float fadeTime;
	float currentTime01;
	public Color startColor;
	Image myPupImage;
	Image myPupBG;
	Sprite greyBGSprite;

	// Use this for initialization
	void Start () {
		myPupImage = gameObject.GetComponent<Image> ();
		myPupBG = transform.parent.GetComponent<Image> ();
		startColor = myPupImage.color;
		greyBGSprite = myPupBG.sprite;

	}
	
	// Update is called once per frame
	void Update () {
		if (activatePup) {
			if (currentTime01 < activateTime) {
				currentTime01 += Time.unscaledDeltaTime;
				float lerp = currentTime01 / activateTime;
				myPupImage.color = Color.Lerp (startColor, Color.white, Mathf.SmoothStep (0.0f, 1.0f, lerp)); 
				myPupImage.transform.localScale = Vector3.Lerp (Vector3.one, Vector3.one * 1.15f, Mathf.SmoothStep (0.0f, 1.0f, lerp));
			} else {
				currentTime01 = 0.0f;
				activatePup = false;
				fadePup = true;
			}
		}
		if (fadePup) {
			if (currentTime01 < fadeTime) {
				currentTime01 += Time.unscaledDeltaTime;
				float lerp = currentTime01 / fadeTime;
				myPupImage.fillAmount = Mathf.Lerp (1.0f, 0.0f, lerp);
				myPupImage.color = Color.Lerp (Color.white, Color.white * 0.5f, Mathf.SmoothStep (0.0f, 1.0f, lerp));
				myPupBG.color = Color.Lerp (Color.clear, Color.white, lerp);
				myPupImage.transform.localScale = Vector3.Lerp (Vector3.one * 1.15f, Vector3.one, Mathf.SmoothStep (0.0f, 1.0f, lerp * 0.5f));
			} else {
				currentTime01 = 0.0f;
				fadePup = false;
				myPupImage.color = startColor;
				myPupImage.transform.localScale = Vector3.one;
				coolDownActive = false;
				if (nextPup) {
					LoadCurrentPup (myPupBG.sprite);
					myPupBG.sprite = greyBGSprite;
				}
			}
		}
	}

	public void ActivateCurrentPup(float coolTime){
		fadeTime = coolTime - activateTime;
		activatePup = true;
		coolDownActive = true;
	}

	public void LoadCurrentPup(Sprite pupUI){
		if (coolDownActive) {
			myPupBG.sprite = pupUI;
			nextPup = true;
		} else {
			myPupImage.sprite = pupUI;
			myPupImage.fillAmount = 1.0f;
		}
	}
}
