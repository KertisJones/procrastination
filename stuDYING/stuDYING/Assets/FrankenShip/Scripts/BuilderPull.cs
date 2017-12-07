using UnityEngine;
using System.Collections;

public class BuilderPull : MonoBehaviour {

	private GameObject pulledPlayer;
	public GameObject pulledIGOR;
	private Vector3 igorStartPos;
	public bool pullShip = true;
	public bool pullIgor;
	public bool shrinkIgor;
	float currentTime01;

	// Use this for initialization
	void Start () {
		if (transform.root.tag == "ShipBuilder01") {
			pulledPlayer = GameObject.FindWithTag ("Player02");
			pulledIGOR = GameObject.FindWithTag ("Player01").transform.GetChild(0).gameObject;
		} else {
			pulledPlayer = GameObject.FindWithTag ("Player01");
			pulledIGOR = GameObject.FindWithTag ("Player02").transform.GetChild(0).gameObject;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (pullShip) {
			float forceStrength = 20.0f;
			Vector3 forceDir = transform.position - pulledPlayer.transform.position;
			Vector3 lookDir = transform.position + (Vector3.up * 25.0f);// pulledPlayer.transform.position;
			pulledPlayer.GetComponent<Rigidbody2D> ().AddForceAtPosition (forceDir.normalized * forceStrength, Vector3.up * 15);
			var angle = Mathf.Atan2 (lookDir.y, forceDir.x) * Mathf.Rad2Deg;
			pulledPlayer.transform.rotation = Quaternion.AngleAxis (angle - 90, Vector3.forward);
		} else if (pullIgor) {
			float crossTime = 2.0f;
			if (currentTime01 <= crossTime) {
				currentTime01 += Time.unscaledDeltaTime;
				float lerp = currentTime01 / crossTime;
				Vector3 forceDir = transform.position + (Vector3.down * 5.0f);
				Vector3 lookDir = (transform.position + Vector3.up * 5.0f) - pulledIGOR.transform.position;
				var angle = Mathf.Atan2 (lookDir.y, lookDir.x) * Mathf.Rad2Deg;
				pulledIGOR.transform.rotation = Quaternion.AngleAxis (angle - 90, Vector3.forward);
				pulledIGOR.transform.position = Vector3.Lerp (igorStartPos, forceDir, lerp);
			} else {
				currentTime01 = 0.0f;
				pullIgor = false;
				shrinkIgor = true;
			}
		} else if (shrinkIgor) {
			float shrinkTime = 2.0f;
			if (currentTime01 <= shrinkTime) {
				currentTime01 += Time.unscaledDeltaTime;
				float lerp = currentTime01 / shrinkTime;
				pulledIGOR.transform.localScale = Vector3.Lerp (Vector3.one, Vector3.one * 0.75f, Mathf.SmoothStep (0.0f, 1.0f, lerp));
			} else {
				shrinkIgor = false;
				Destroy (pulledIGOR.gameObject);
				GameObject.FindWithTag ("GameMaster").GetComponent<PhaseNavigator> ().InitiateBuild ();
				gameObject.GetComponent<BuilderPull> ().enabled = false;
			}
		}

	}

	void OnTriggerEnter2D (Collider2D other){
		if (other.transform.root.gameObject == pulledPlayer && other.transform.parent.tag == "Core") {
			other.transform.root.position = transform.position;
			other.transform.root.rotation = transform.rotation;
			other.transform.root.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;
			gameObject.GetComponent<CircleCollider2D> ().enabled = false;
			gameObject.GetComponent<BoxCollider2D> ().enabled = true;
			pulledIGOR.GetComponent<LockLocalPos> ().enabled = false;
			pulledIGOR.transform.SetParent (null);
			igorStartPos = pulledIGOR.transform.position;
			pullShip = false;
			pullIgor = true;
		}
	}
}
