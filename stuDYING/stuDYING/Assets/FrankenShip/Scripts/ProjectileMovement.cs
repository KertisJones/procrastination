using UnityEngine;
using System.Collections;

public class ProjectileMovement : MonoBehaviour {

	/// <summary>
	/// My speed.
	/// </summary>
	public float mySpeed;
	/// <summary>
	/// My damage.
	/// </summary>
	public int myDamage;
	/// <summary>
	/// Transform of the player object this was spawned from.
	/// </summary>
	public Transform parentObject;
	/// <summary>
	/// Does this projectile scale from small to its original size?
	/// </summary>
	bool scaleOnSpawn = true;
	/// <summary>
	/// Will this projectile provide a force upon impact?
	/// </summary>
	public bool physics;
	/// <summary>
	/// How much force this object will apply if (physics).
	/// </summary>
	public float forceStrength;
	float currentTime01;
	float timeToScale = 2.0f;
	Vector3 startScale;
	Vector3 startPos;
	// Use this for initialization
	void Start () {
		startScale = transform.localScale;
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate (Vector3.up * mySpeed * Time.unscaledDeltaTime, Space.Self);
		if (scaleOnSpawn) {
			if (currentTime01 < timeToScale) {
				currentTime01 += Time.unscaledDeltaTime;
				float lerp = currentTime01 / timeToScale;
				transform.localScale = Vector3.Lerp (startScale, startScale * 1.5f, Mathf.SmoothStep (0.0f, 1.0f, lerp));
			} else {
				scaleOnSpawn = false;
			}
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.layer == 8 && !other.isTrigger && other.transform.root != parentObject) {
			other.GetComponent<HullHealth> ().DoDamage (myDamage);
			if (physics) {
				Vector3 forceDir = transform.position - startPos;
				other.transform.root.GetComponent<Rigidbody2D> ().AddForceAtPosition (forceDir.normalized * forceStrength, other.transform.position, ForceMode2D.Force);
			}
			Destroy (gameObject);
		}
		if (other.gameObject.layer == 25) {
			Destroy (gameObject);
		}
	}
}
