using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuildAnimHandler : MonoBehaviour {

	ShipBuilder buildScript;
	ShipBuilder otherBuild;
	BuildAnimHandler otherAnim;
	DevPartSpawn[] invDevScripts;
	SpriteRenderer[] boundSprites;
	public Color minShockColor;
	public Color maxShockColor;
	public GameObject otherBuilder;
	public Button myButton;
	public RectTransform myCursor;
	public GameObject mySwitchLight;
	public GameObject lifeShocker;
	public Animator coreAnim;
	PhaseNavigator navScript;
	Animator lifeAnim;
	Image cursorImage;
	string myBoolName;
	string otherBoolName;
	bool shockBounds;
	bool lastUsedParts;
	/// <summary>
	/// Is the cursor locked into the shock bulb, confirming the players' finished state?
	/// </summary>
	public bool lockShock;
	/// <summary>
	/// Move my Cursor over the lock shock bulb.
	/// </summary>
	public bool pullCursor;
	float currentTime01;
	float lockShockTime = 0.25f;
	float shockBoundTime = 2.0f;
	Vector3 cursorStartPos;
	Vector3 cursorStartRot;
	public InAudioNode coreShock;

	// Use this for initialization
	void Start () {
		buildScript = gameObject.GetComponent<ShipBuilder> ();
		otherBuild = otherBuilder.GetComponent<ShipBuilder> ();
		otherAnim = otherBuilder.GetComponent<BuildAnimHandler> ();
		lifeAnim = myButton.transform.parent.GetComponent<Animator> ();
		cursorImage = myCursor.GetComponentInChildren<Image> ();
		navScript = GameObject.FindWithTag ("GameMaster").GetComponent<PhaseNavigator> ();
		invDevScripts = myCursor.transform.parent.GetComponentsInChildren<DevPartSpawn> ();
		boundSprites = gameObject.GetComponentInChildren<BuilderPull> ().GetComponentsInChildren<SpriteRenderer> ();
		if (transform.tag == "ShipBuilder01") {
			myBoolName = "Green";
			otherBoolName = "Blue";
		} else {
			myBoolName = "Blue";
			otherBoolName = "Green";
		}
		if (GameObject.FindWithTag ("GameMaster").GetComponent<PhaseNavigator> ().DEVMODE_Build) {
			mySwitchLight.SetActive (true);
		}
	}

	// Update is called once per frame
	void Update () {
		if (buildScript.usedAllParts != lastUsedParts) {
			if (buildScript.usedAllParts) {
				myButton.enabled = true;
				if (otherBuild.usedAllParts) {
					lifeAnim.SetTrigger ("Both");
					mySwitchLight.SetActive (true);
				} else {
					lifeAnim.SetTrigger (myBoolName);
					mySwitchLight.SetActive (true);
				}
			} else {
				myButton.enabled = false;
				if (otherBuild.usedAllParts) {
					lifeAnim.SetTrigger (otherBoolName);
					mySwitchLight.SetActive (false);
				} else {
					lifeAnim.SetTrigger ("Idle");
					mySwitchLight.SetActive (false);
				}
			}
		}
			
		if (pullCursor) {
			if (currentTime01 < lockShockTime) {
				currentTime01 += Time.unscaledDeltaTime;
				float lerp = currentTime01 / lockShockTime;
				if (transform.tag == "ShipBuilder01") {
					myCursor.localPosition = Vector3.Lerp (cursorStartPos, Vector3.down * 97, lerp);
					myCursor.localEulerAngles = Vector3.Lerp (cursorStartRot, Vector3.forward * 180, lerp);
				} else {
					myCursor.localPosition = Vector3.Lerp (cursorStartPos, Vector3.up * 97 + Vector3.left * 23.0f, lerp);
					myCursor.localEulerAngles = Vector3.Lerp (cursorStartRot, Vector3.zero, lerp);
				}
				cursorImage.color = Color.Lerp (Color.white, Color.gray * 0.75f, lerp);
			} else {
				CheckBothLocked ();
				GrayOutButtons ();
				pullCursor = false;
				lockShock = true;
				currentTime01 = 0.0f;
			}
		}

		if (shockBounds) {
			if (currentTime01 <= shockBoundTime) {
				currentTime01 += Time.unscaledDeltaTime;
				foreach (SpriteRenderer boundSprite in boundSprites) {
					Color shockColor = new Color (Random.Range (minShockColor.r, maxShockColor.r), Random.Range (minShockColor.g, maxShockColor.g), Random.Range (minShockColor.b, maxShockColor.b));
					boundSprite.color = shockColor;
				}
			} else {
				shockBounds = false;
				InAudio.Break (gameObject, coreShock);
			}
		}

		lastUsedParts = buildScript.usedAllParts;
	}

	public void LockShockers (){
		pullCursor = true;
		buildScript.enabled = false;
		myCursor.GetComponent<CursorMovement> ().enabled = false;
		cursorStartPos = myCursor.localPosition;
		cursorStartRot = myCursor.localEulerAngles;
	}

	public void CheckBothLocked(){
		if (otherAnim.lockShock) {
			StartCoroutine(BringToLife ());
		}
	}

	public void GrayOutButtons(){
		if (navScript.DEVMODE_Build) {
			foreach (DevPartSpawn devScript in invDevScripts) {
				devScript.GreyButton ();
			}
		}
	}

	IEnumerator BringToLife(){
		lifeAnim.SetTrigger ("Switch");
		yield return new WaitForSeconds (1.0f);
		StartCoroutine (ShockGridBounds ());
		StartCoroutine (otherAnim.ShockGridBounds ());
	}

	IEnumerator ShockGridBounds(){
		shockBounds = true;
		lifeShocker.SetActive (true);
		coreAnim.SetTrigger ("Laugh");
		yield return new WaitForSeconds (shockBoundTime);
		StartCoroutine (FinishBuild ());
	}

	IEnumerator FinishBuild(){
		yield return new WaitForSeconds (0);
		navScript.TerminateBuild ();
	}
}
