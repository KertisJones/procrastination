using UnityEngine;
using System.Collections;

public class HullHealth : MonoBehaviour {

	public float myCollDensity;
	public int startHealth;
	public int myHealth;
	PhaseNavigator navScript;
	ProjectileMovement projScript;
	SpriteRenderer mySprite;
	PolygonCollider2D myColl;
	GameObject shipExplosion;
	Animator myAnim;
	public InAudioNode coreDamageSFX;
	bool damageTimer;
	public bool shieldHull;
	private bool alive = true;
	public GameObject faceSplit;
	public FaceCamShake faceScript;
	public GameObject partExplosion;
	public GameObject hullSplit;

	public InAudioNode pieceDamaged;
	public InAudioNode weaponBroken;
	public InAudioNode hullBroken;

	// Use this for initialization
	void Start () {
		navScript = GameObject.FindWithTag ("GameMaster").GetComponent<PhaseNavigator> ();
        myHealth = startHealth;
		mySprite = gameObject.GetComponent<SpriteRenderer> ();
		myAnim = gameObject.GetComponent<Animator> ();
		myColl = gameObject.GetComponent<PolygonCollider2D>();
		myColl.density = myCollDensity;
		shipExplosion = transform.GetChild (0).gameObject;
		if (transform.root.tag == "Player02") {
			myAnim.SetTrigger ("Player02");
		}
		foreach (Transform child in transform.root.GetChild(0)) {
			if (child.tag == "ShieldHull") {
				hullSplit = child.GetComponent<HullHealth> ().hullSplit;
			}
		}
    }
	
	// Update is called once per frame
	void Update () {
		if (alive && myHealth <= 0) {
			if (gameObject.tag == "Core") {
				StartCoroutine (CoreDeath ());
			} else {
				StartCoroutine (PartDeath());
			}

		}
	}

	public void DoDamage(int damage){
		if (!shieldHull) {
			myHealth -= damage;
			StartCoroutine (IndicateDamage ());
		}
	}

	IEnumerator IndicateDamage(){
		mySprite.color = Color.red;

		if (!damageTimer) {
			if (gameObject.tag == "Core") {
				StartCoroutine (SoundDamage (coreDamageSFX));
				StartCoroutine (faceScript.FaceShakeDuration ());
			} else {
				StartCoroutine (SoundDamage (pieceDamaged));
			}
		}
		yield return new WaitForSeconds (1);
		mySprite.color = Color.white;

	}

	IEnumerator SoundDamage(InAudioNode sound){
		damageTimer = true;
		InAudio.Play (gameObject, sound, null);
		yield return new WaitForSeconds (0.5f);
		damageTimer = false;
	}

	IEnumerator CoreDeath(){
		alive = false;
		myAnim.SetTrigger ("Death");
		yield return new WaitForSeconds (2);
		shipExplosion.SetActive (true);
		Instantiate (faceSplit, transform.position, transform.rotation);
		myAnim.SetTrigger ("Empty");
		navScript.TerminateBattle (transform.root.gameObject);
		gameObject.GetComponent<HullHealth> ().enabled = false;
	}

	IEnumerator PartDeath(){
		alive = false;
		myColl.enabled = false;
		if (mySprite.sortingLayerName == "Weapons") {
			if (transform.tag != "PlasmaTorch") {
				gameObject.GetComponentInChildren<ShootShoot> ().gameObject.SetActive (false);
			} else {
				PlasmaShock shockScript = gameObject.GetComponentInChildren<PlasmaShock> ();
				shockScript.GetComponent<CircleCollider2D> ().enabled = false;
				shockScript.GetComponentInChildren<ParticleSystem> ().enableEmission = false;
				shockScript.enabled = false;
			}
			mySprite.enabled = false;
			InAudio.Play (gameObject, weaponBroken, null);
		} else if (mySprite.sortingLayerName == "Thrusters") {
			mySprite.enabled = false;
			InAudio.Play (gameObject, weaponBroken, null);
		} else if (mySprite.sortingLayerName == "Hulls") {
			StartCoroutine (SplitHull ());

		}
		GameObject explosion = (GameObject)Instantiate (partExplosion, transform.position, transform.rotation);
		explosion.transform.SetParent (transform);
		yield return new WaitForSeconds (2.0f);
		Destroy (explosion);
	}

	IEnumerator SplitHull(){
		yield return new WaitForSeconds (1.5f);
		GameObject newSplit = (GameObject) Instantiate (hullSplit, transform.position, transform.rotation);
		InAudio.Play (gameObject, hullBroken, null);
		if (transform.root.tag == "Player01") {
			myAnim.SetTrigger ("Empty01");
		} else {
			myAnim.SetTrigger ("Empty02");
		}
	}
}
