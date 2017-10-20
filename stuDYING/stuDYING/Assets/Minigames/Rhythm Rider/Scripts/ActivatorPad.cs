using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActivatorPad : MonoBehaviour {

	public KeyCode key;

	public GameObject healthBar;

	//SpriteRenderer sr;
	bool active = false;
	GameObject note;
	Color old;
	public bool createMode;
	public GameObject n;
	float step;
	bool lockInput = false;
	Vector3 start;

	Slider health;

	// Use this for initialization
	void Awake () {
		//sr = GetComponent<SpriteRenderer> ();
	}

	void Start(){
		//old = sr.color;
		PlayerPrefs.SetInt ("Score", 0);
		step = 0.05f * Time.deltaTime;
		start = this.transform.position;
		health = healthBar.GetComponent<Slider> ();
	}

	// Update is called once per frame
	void Update () {
		if (createMode) {
			if (Input.GetKeyDown (key)) {
				Instantiate (n, transform.position, Quaternion.identity);
			}
		} else {
			if (Input.GetKeyDown (key)) {
				//lockInput = true;
				//StartCoroutine (Pressed ());
				//AddScore();
			}

			if (Input.GetKeyDown (key) && active) {
				Destroy (note);
				//Instantiate (successBurst, transform.position, successBurst.rotation);
				GameMasterRhythmRider.winStreak++;
				GameMasterRhythmRider.totalScore += (GameMasterRhythmRider.combo * 10);
				Debug.Log ("Hit to the beat...");
				AddScore ();
				//GameMasterRhythmRider.winStreak++;
				//GameMasterRhythmRider.totalScore += (GameMasterRhythmRider.combo * 10);
				GameMasterRhythmRider.playerHealth += 5;
				if (GameMasterRhythmRider.playerHealth >= 100)
					GameMasterRhythmRider.playerHealth = 100;
				health.value = GameMasterRhythmRider.playerHealth;
				active = false;
			}
		}
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "LeftNote" && this.name == "LeftPad") {
			active = true;
			note = col.gameObject;
		} else if (col.gameObject.tag == "MidNote" && this.name == "MidPad") {
			active = true;
			note = col.gameObject;
		} else if (col.gameObject.tag == "RightNote" && this.name == "RightPad") {
			active = true;
			note = col.gameObject;
		}
	}

	void OnTriggerExit(Collider col){
		active = false;
	}

	void AddScore(){
		PlayerPrefs.SetInt ("Score", PlayerPrefs.GetInt ("Score") + 100);
	}

	IEnumerator Pressed(){
		Vector3 upBeat = new Vector3 (start.x, start.y + 0.1f, start.z);
		MoveObject (this.transform, start, upBeat, 1f);
		yield return new WaitForSeconds (0.001f);
		MoveObject (this.transform, upBeat, start, 1f);
		lockInput = false;
	}

	void MoveObject (Transform transform, Vector3 start, Vector3 end, float time){
		float i = 0.0f;
		float rate = (float) 1.0f / time;
		while (i < 1.0f) {
			i += Time.deltaTime * rate;
			transform.position = Vector3.Lerp (start, end, i);
		}
	}
}		