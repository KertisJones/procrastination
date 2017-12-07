using UnityEngine;
using System.Collections;

public class ShootShoot : MonoBehaviour {

	/// <summary>
	/// The bullet projectile spawned by this weapon!
	/// </summary>
	public GameObject myBullet;
	/// <summary>
	/// The glow shot spawned by this weapon!
	/// </summary>
	public GameObject myGlowShot;
	/// <summary>
	/// Is the spawn cooldown started?
	/// </summary>
	bool timeStarted;
	/// <summary>
	/// Speed of the projectile spawned.
	/// </summary>
	public float bulletSpeed;
	/// <summary>
	/// Damage dealt by the bullet upon impact.
	/// </summary>
	public int bulletDamage;
	/// <summary>
	/// Delay in seconds between each consecutive shot.
	/// </summary>
	public float spawnDelay;
	/// <summary>
	/// Should this weapon spawn the new Glowing Projectiles?
	/// </summary>
	bool glowShots = true;
	/// <summary>
	/// Reference to the newly spawned projectile!
	/// </summary>
	float currentTime01;
	float currentTime02;
	public GameObject newProj;
	ProjectileMovement projScript;
	SpriteRenderer projSprite;
	Vector3 myStartPos;
	Vector3 myStartScale;

    //audio variables
    public InAudioNode lazerGunSFX;
    public InAudioNode shotGunSFX;
    public InAudioNode machineGunSFX;

	// Use this for initialization
	void Start () {
		myStartPos = transform.localPosition;
		myStartScale = transform.localScale;
		StartCoroutine (StartDelay ());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (currentTime01 < spawnDelay) {
			currentTime01 += Time.unscaledDeltaTime;
			float lerp = currentTime01 / spawnDelay;
			switch (transform.parent.tag) {
			case "MachineGun":
				projSprite.color = Color.Lerp (Color.clear, Color.white, Mathf.SmoothStep (0.0f, 1.0f, lerp * 3.0f));
				transform.localPosition = Vector3.Lerp (myStartPos, Vector3.up * 3.5f, lerp);
				transform.localScale = Vector3.Lerp (myStartScale, Vector3.one * 0.8f, Mathf.SmoothStep (0.0f, 1.0f, lerp * 1.25f));
				break;
			case "MissileLauncher":
				projSprite.color = Color.Lerp (Color.clear, Color.white, Mathf.SmoothStep (0.0f, 1.0f, lerp));
				transform.localPosition = Vector3.Lerp (myStartPos, Vector3.up * 1.03f, Mathf.SmoothStep (0.0f, 1.0f, lerp));
				transform.localScale = Vector3.Lerp (new Vector3 (0.25f, 0.5f, 1.0f), Vector3.one, Mathf.SmoothStep (0.0f, 1.0f, lerp * 1.25f));
				break;
			case "ShotGun":
				projSprite.color = Color.Lerp (Color.white * 0.5f, Color.white, Mathf.SmoothStep (0.0f, 1.0f, lerp * 2.0f));
				transform.localPosition = Vector3.Lerp (myStartPos, Vector3.up * 2.12f, lerp);
				transform.localScale = Vector3.Lerp (new Vector3 (1.4f, 1.0f, 1.0f), Vector3.one, Mathf.SmoothStep (0.0f, 1.0f, lerp * 1.25f));
				break;
			case "LazerGun":
				projSprite.color = Color.Lerp (Color.clear, Color.white, Mathf.SmoothStep (0.0f, 1.0f, lerp * 2.0f));
				transform.localPosition = Vector3.Lerp (myStartPos, Vector3.up * 1.8f, lerp);
				transform.localScale = Vector3.Lerp (new Vector3 (1.0f, 0.0f, 1.0f), Vector3.one, Mathf.SmoothStep (0.0f, 1.0f, lerp * 1.25f));
				break;
			}
		} else {
			FireShot ();
		}
	}

	IEnumerator StartDelay(){
		float delay = Random.Range (spawnDelay * 0.25f, spawnDelay * 0.75f);
		yield return new WaitForSeconds (delay);
		SpawnShot ();
	}

	public void SpawnShot(){
		currentTime01 = 0.0f;
		transform.localPosition = myStartPos;
		transform.localScale = Vector3.one;
		if (glowShots) {
			newProj = (GameObject) Instantiate (myGlowShot, transform.position, transform.rotation);
			if (transform.root.tag == "Player02") {
				newProj.GetComponent<Animator> ().SetTrigger ("Glow02");
				foreach (ParticleSystem system in newProj.GetComponentsInChildren<ParticleSystem>()) {
					system.startColor = Color.cyan;
				}
			}
		} else {
			newProj = (GameObject) Instantiate (myBullet, transform.position, transform.rotation);
		}
		newProj.GetComponent<SpriteRenderer> ().enabled = true;
		newProj.transform.SetParent (transform);
		projSprite = newProj.GetComponent<SpriteRenderer> ();
	}

	public void FireShot() {
		newProj.transform.SetParent (null);
		newProj.GetComponent<PolygonCollider2D> ().enabled = true;
		ProjectileMovement projMove = newProj.GetComponent<ProjectileMovement> ();
		projMove.enabled = true;
		projMove.parentObject = transform.root;
		projMove.mySpeed = bulletSpeed;
		projMove.myDamage = bulletDamage;
		foreach (ParticleSystem system in newProj.GetComponentsInChildren<ParticleSystem>()) {
			system.enableEmission = true;
		}

		if (transform.parent.tag == "LazerGun") {
			InAudio.Play (gameObject, lazerGunSFX, null);
		} else if (transform.parent.tag == "ShotGun") {
			InAudio.Play (gameObject, shotGunSFX, null);
		} else if (transform.parent.tag == "PlasmaTorch") {
			
		} else if (transform.parent.tag == "MachineGun") {
			InAudio.Play (gameObject, machineGunSFX, null);
		} else {
			
		}
		SpawnShot ();
	}

	public void DestroySelf(){
		if (currentTime01 < spawnDelay) {
			Destroy (newProj);
			gameObject.GetComponent<ShootShoot> ().enabled = false;
		}
	}
}
