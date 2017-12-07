using UnityEngine;
using System.Collections;

public class SkillShot : MonoBehaviour {

	Rigidbody2D playerBody;
	LineRenderer[] mySights;
	public GameObject skillShotPrefab;
	public float coolDownTime;
	public float rotationSpeed;
	public bool coolDown;
	public bool skillSights;
	public bool stickAim;
	public Vector3 eulers;
	float RStickHo;
	float RStickVert;
	float lastStickHo;
	float lastStickVert;
	float stickHeading;
	bool activateSights;
	bool activateShot;
	bool enterAim;
	float analogDeadzone = 0.45f;

	public InAudioNode skillShotFx;

	// Use this for initialization
	void Start () {
		playerBody = transform.root.GetComponent<Rigidbody2D> ();
		mySights = gameObject.GetComponentsInChildren<LineRenderer> ();
		if ((transform.root.tag == "Player01" && ControlManager.P1Key) || (transform.root.tag == "Player02" && ControlManager.P2Key)) {
			stickAim = false;
		}
		foreach (LineRenderer liner in mySights) {
			liner.gameObject.SetActive (false);
		}
		gameObject.GetComponent<SkillShot> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 rotationDelta = Vector3.forward * rotationSpeed * Time.deltaTime;
		if (transform.root.tag == "Player01") {
			RStickHo = ControlManager.P1RHriz;
			RStickVert = ControlManager.P1RVert;
			activateSights = ControlManager.P1LTrig;
			activateShot = ControlManager.P1RTrig;
		} else {
			RStickHo = ControlManager.P2RHriz;
			RStickVert = ControlManager.P2RVert;
			activateSights = ControlManager.P2LTrig;
			activateShot = ControlManager.P2RTrig;
		}

		if (stickAim) {
			Vector2 stickInput = new Vector2 (RStickHo, RStickVert);
			if (stickInput.magnitude > analogDeadzone) {
				lastStickHo = RStickHo;
				lastStickVert = RStickVert;
				enterAim = true;
			}
			if (stickInput.magnitude < analogDeadzone) {
				if (enterAim) {
					RStickHo = lastStickHo;
					RStickVert = lastStickVert;
				}
				enterAim = false;
			}
			stickHeading = Mathf.Atan2 (RStickHo, RStickVert);
		}



		if (skillSights) {
			if (activateShot) {
				foreach (LineRenderer liner in mySights) {
					GameObject newLazer = (GameObject)Instantiate (skillShotPrefab, liner.transform.position, mySights [0].transform.rotation);
					newLazer.GetComponent<ProjectileMovement> ().parentObject = transform.root;
				}
				InAudio.Play (gameObject, skillShotFx, null);
				ExitSights ();
				StartCoroutine (SkillCoolDown());
			} else {
				if (!activateSights) {
					ExitSights ();
				}
				foreach (LineRenderer liner in mySights) {
					if (stickAim) {
						if (enterAim) {
							liner.transform.rotation = Quaternion.Euler (0.0f, 0.0f, stickHeading * Mathf.Rad2Deg + 180.0f);
						}
					} else {
						liner.transform.localEulerAngles -= rotationDelta * RStickHo;
					}
				}
			}
		} else if (!coolDown) {
			if (activateSights) {
				playerBody.constraints = RigidbodyConstraints2D.FreezeRotation;
				foreach (LineRenderer liner in mySights) {
					liner.transform.localEulerAngles = Vector3.zero;
					liner.gameObject.SetActive (true);
				}
				skillSights = true;
			}
		}
	}


	public void ExitSights(){
		playerBody.constraints = RigidbodyConstraints2D.None;
		foreach (LineRenderer liner in mySights) {
			liner.gameObject.SetActive (false);
		}
		skillSights = false;
	}

	IEnumerator SkillCoolDown(){
		coolDown = true;
		yield return new WaitForSeconds (coolDownTime);
		coolDown = false;

	}
}
