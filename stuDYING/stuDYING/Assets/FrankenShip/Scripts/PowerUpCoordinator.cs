using UnityEngine;
using UnityEngine.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PowerUpCoordinator : MonoBehaviour {

	public GameObject myPupUI;
	Sprite myStartSprite;
	ShipBaseMovement moveScript;
	PowerUpUICooldown cdScript;
	public Image myPupImage;
	public GameObject currentPup;
	public GameObject pupAttack;
	public GameObject pupSpeed;
	public GameObject pupRepair;
	public GameObject pupShield;
    public GameObject pupMine;
	public GameObject pupSlime;
    public GameObject battleMine;
    public GameObject slime;
	public GameObject pupAttackParticles;
	public GameObject pupRepairParticles;
	public GameObject pupShieldOverlay;
	public GameObject spaceSlimeTrail;
	public GameObject myFaceSlime;
	public GameObject myPupTexter;
	public List <Color> pupTextColors;
	Text myPupText;
	public  bool timerStarted;
	TrailRenderer moveSpeedTrail;
    //audio variables
	public InAudioNode pupCollectSound;
	public InAudioNode pupActivateSound;
    public InAudioNode p1AttackspeedSound;
    public InAudioNode p2AttackspeedSound;
    public InAudioNode p1MovespeedSound;
    public InAudioNode p2MovespeedSound;
    public InAudioNode p1RepairSound;
    public InAudioNode p2RepairSound;
    public InAudioNode p1ShieldSound;
    public InAudioNode p2ShieldSound;
	bool colorText;
	float newScale;


	// Use this for initialization
	void Start () {
		myPupImage = myPupUI.GetComponent<Image> ();
		myStartSprite = myPupImage.sprite;
		moveSpeedTrail = gameObject.GetComponentInChildren<TrailRenderer> ();
		myPupText = myPupTexter.GetComponent<Text> ();
		cdScript = myPupImage.GetComponentInChildren<PowerUpUICooldown> ();
		moveScript = gameObject.GetComponentInChildren<ShipBaseMovement> ();
		myPupText.text = "POWER UP";
	}
	
	// Update is called once per frame
	void Update () {
		if (!timerStarted && currentPup != null && 
			((transform.tag == "Player01" && ControlManager.P1X) || (transform.tag == "Player02" && ControlManager.P2X))) {
				StartCoroutine(ActivatePup(currentPup));
		}
	}

	public void CollectPup(GameObject pup){
		InAudio.Play (gameObject, pupCollectSound, null);
		PowerUp pwrScript = pup.GetComponent<PowerUp> ();
		if (pwrScript.powerUpType == PowerUp.PowerUpTypes.Attack) {
			currentPup = pupAttack;
			myPupText.text = "ATTACK SPEED";
			myPupText.color = Color.red;
			myPupText.color = pupTextColors[0];
		} else if (pwrScript.powerUpType == PowerUp.PowerUpTypes.Speed) {
			currentPup = pupSpeed;
			myPupText.text = "MOVE SPEED";
			myPupText.color = pupTextColors[1];
		} else if (pwrScript.powerUpType == PowerUp.PowerUpTypes.RepairKit) {
			currentPup = pupRepair;
			myPupText.text = "REPAIR KIT";
			myPupText.color = pupTextColors[2];
		} else if (pwrScript.powerUpType == PowerUp.PowerUpTypes.Shield) {
			currentPup = pupShield;
			myPupText.text = "INVULNERABLE";
			myPupText.color = pupTextColors[3];
		} else if (pwrScript.powerUpType == PowerUp.PowerUpTypes.Mine) {
			currentPup = pupMine;
			myPupText.text = "SPACE MINE";
			myPupText.color = pupTextColors[4];
		} else if (pwrScript.powerUpType == PowerUp.PowerUpTypes.Slime) {
			currentPup = pupSlime;
			myPupText.text = "SPACE SLIME";
			myPupText.color = pupTextColors[5];
		}
        if (transform.tag == "Player01")
        {
            Analytics.CustomEvent("Player 1 Power Up Pickups", new Dictionary<string, object>
            {
                { "player", transform.tag },
                { "powerupType", currentPup }
            });
        }
        else
        {
            Analytics.CustomEvent("Player 2 Power Up Pickups", new Dictionary<string, object>
            {
                { "player", transform.tag },
                { "powerupType", currentPup }
            });
        }
		cdScript.LoadCurrentPup (currentPup.GetComponent<SpriteRenderer> ().sprite);
	}

	IEnumerator ActivatePup(GameObject pup){
		timerStarted = true;
		currentPup = null;
		ResetUI ();
		InAudio.Play (gameObject, pupActivateSound, null);
		PowerUp pwrScript = pup.GetComponent<PowerUp> ();
		cdScript.ActivateCurrentPup (pwrScript.timeActive);
        switch (pwrScript.powerUpType)
        {
		case PowerUp.PowerUpTypes.Attack:
			ShootShoot[] shooters = gameObject.GetComponentsInChildren<ShootShoot> ();
			List<GameObject> attacks = new List<GameObject> ();
			foreach (ShootShoot script in shooters) {
				GameObject newParticles = (GameObject)Instantiate (pupAttackParticles, script.transform.position, script.transform.rotation);
				newParticles.transform.SetParent (script.transform);
				attacks.Add (newParticles.gameObject);
				script.spawnDelay *= 0.5f;
			}
			if (transform.tag == "Player01") { //checks player tag to play proper audio
				InAudio.Play (gameObject, p1AttackspeedSound, null);
			} else if (transform.tag == "Player02") {
				InAudio.Play (gameObject, p2AttackspeedSound, null);
			}
			yield return new WaitForSeconds (pwrScript.timeActive);
			foreach (GameObject system in attacks) {
				Destroy (system.gameObject);
			}
            foreach (ShootShoot script in shooters)
            {
				script.spawnDelay /= 0.5f;
            }
            break;

		case PowerUp.PowerUpTypes.Mine:
			GameObject newMine = (GameObject)Instantiate (battleMine, transform.position + transform.rotation * Vector2.down * 5.0f, transform.root.rotation);
			Vector3 forceDir = newMine.transform.position - transform.position;
			float spawnForce = 1500.0f;
			newMine.GetComponent<Rigidbody2D> ().AddForce (forceDir.normalized * spawnForce, ForceMode2D.Force);
			newMine.GetComponentInChildren<BattleMineTrigger> ().myPlayer = gameObject;
            break;

		case PowerUp.PowerUpTypes.Speed:
			moveSpeedTrail.enabled = true;
            transform.GetComponentInChildren<ShipBaseMovement>().thrustPower *= 1.5f;
			transform.GetComponentInChildren<ShipBaseMovement>().MaxLinVelocity *= 1.5f;
			transform.GetComponentInChildren<ShipBaseMovement>().MaxAngVelocity *= 1.5f;
            if (transform.tag == "Player01") //checks player tag to play proper audio
            {
                InAudio.Play(gameObject, p1MovespeedSound, null);
            }
            else if (transform.tag == "Player02")
            {
                InAudio.Play(gameObject, p2MovespeedSound, null);
            }
			yield return new WaitForSeconds(pwrScript.timeActive);
			moveSpeedTrail.enabled = false;
            transform.GetComponentInChildren<ShipBaseMovement>().thrustPower /= 1.5f;
			transform.GetComponentInChildren<ShipBaseMovement>().MaxLinVelocity /= 1.5f;
			transform.GetComponentInChildren<ShipBaseMovement>().MaxAngVelocity /= 1.5f;
            break;

		case PowerUp.PowerUpTypes.RepairKit:
			HullHealth coreHealth = gameObject.GetComponentInChildren<HullHealth> ();
			int healthHealed = 2;
			if (coreHealth.myHealth <= (coreHealth.startHealth - healthHealed)) {
				coreHealth.myHealth += healthHealed;
			}
			if (transform.tag == "Player01") { //checks player tag to play proper audio
				InAudio.Play (gameObject, p1RepairSound, null);
			} else if (transform.tag == "Player02") {
				InAudio.Play (gameObject, p2RepairSound, null);
			}
			GameObject repairParticles = (GameObject)Instantiate (pupRepairParticles, transform.position, transform.rotation);
			repairParticles.transform.SetParent (transform);
			yield return new WaitForSeconds (pwrScript.timeActive);
			Destroy (repairParticles);
            break;

		case PowerUp.PowerUpTypes.Shield:
			HullHealth[] healths = gameObject.GetComponentsInChildren<HullHealth> ();
			List<GameObject> shields = new List<GameObject> ();
			foreach (HullHealth health in healths) {
				health.shieldHull = true;
				GameObject shieldSprite = (GameObject)Instantiate (pupShieldOverlay, health.transform.position, health.transform.rotation);
				shieldSprite.transform.SetParent (health.transform);
				shields.Add (shieldSprite);
				if (health.GetComponent<SpriteRenderer> ().sortingLayerName == "Weapons" || health.GetComponent<SpriteRenderer> ().sortingLayerName == "Thrusters") {
					shieldSprite.transform.localScale = new Vector3 (0.2f, 0.8f, 1.0f);
				}
			}
			if (transform.tag == "Player01") { //checks player tag to play proper audio
				InAudio.Play (gameObject, p1ShieldSound, null);
			} else if (transform.tag == "Player02") {
				InAudio.Play (gameObject, p2ShieldSound, null);
			}
			yield return new WaitForSeconds (pwrScript.timeActive);
			foreach (HullHealth health in healths) {
				health.shieldHull = false;
			}
			foreach (GameObject shield in shields) {
				Destroy (shield.gameObject);
			}
            break;
		case PowerUp.PowerUpTypes.Slime:
			GameObject slimeTrail = (GameObject)Instantiate (spaceSlimeTrail, transform.position, transform.rotation);
			slimeTrail.transform.SetParent (transform.GetChild (0).transform);
			slimeTrail.transform.localPosition = new Vector3 (0.0f, 1.25f, 0.0f);
			for (int i = 0; i < 5; i++) {
				GameObject newSlime = (GameObject)Instantiate (slime, transform.position + transform.rotation * Vector2.down * 1.5f, transform.rotation);
				newSlime.transform.eulerAngles = new Vector3(0,0, Random.Range (0, 360));
				if (i == 0 || i == 4) {
					newScale = 2.5f;
				} else {
					if (i == 1 || i == 3) {
						newScale = 1.75f;
					} else {
						newScale = 2.0f;
					}
					newSlime.GetComponent<SpriteRenderer> ().sprite = newSlime.GetComponent<SpaceSlimeSlow> ().slimeSprite02;
				}
				newSlime.transform.localScale = new Vector3 (newScale, newScale, 1.0f);
				newSlime.AddComponent<PolygonCollider2D> ();
				newSlime.GetComponent<PolygonCollider2D> ().isTrigger = true;
				newSlime.GetComponent<SpaceSlimeSlow> ().myPlayer = transform;
				yield return new WaitForSeconds (0.35f);
			}
			Destroy (slimeTrail);
            break;



        default:
			yield return new WaitForSeconds(pwrScript.timeActive);
            break;
        }

		timerStarted = false;
	}

	public void StartSlimeSlow(){
		StartCoroutine (SpaceSlimeSlow (moveScript.thrustPower, moveScript.torquePower));

	}

	public IEnumerator SpaceSlimeSlow(float thrustPow, float torquePow) {
		float slowTime = 3.0f;
		float thrustDelta = (thrustPow / 5);
		float torqueDelta = (torquePow / 5);
		moveScript.thrustPower -= thrustDelta;
		moveScript.torquePower -= torqueDelta;
		GameObject faceSlimer = (GameObject)Instantiate (myFaceSlime, transform.position, transform.rotation);
		faceSlimer.transform.SetParent (transform);
		if (transform.tag == "Player01") {
			faceSlimer.transform.localPosition = new Vector3 (0.0f, -0.075f, 0.0f);
		} else {
			faceSlimer.transform.localPosition = new Vector3 (-0.1f, 0.025f, 0.0f);
		}
		faceSlimer.GetComponent<FaceSlimeFade> ().lifeTime = slowTime;
		faceSlimer.GetComponent<FaceSlimeFade> ().enabled = true;
		yield return new WaitForSeconds (slowTime);
		moveScript.thrustPower += thrustDelta;
		moveScript.torquePower += torqueDelta;

	}

	public void ResetUI() {
		myPupText.text = "POWER UP";
		myPupText.color = Color.gray;
	}
}
