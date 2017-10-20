using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActivatorLane : MonoBehaviour {

	public KeyCode key;
	public GameObject leftPad;
	public GameObject midPad;
	public GameObject rightPad;

	public GameObject healthBar;

	public GameObject GameMast;

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

	Vector3 leftPosn = new Vector3(-0.984f, -0.469f, -6.862f);
		Vector3 leftPosnL = new Vector3(-1.288f, -0.445f, -6.862f);
		Vector3 leftPosnM = new Vector3(-0.978f, -0.445f, -6.862f);
		Vector3 leftPosnR = new Vector3(-0.653f, -0.445f, -6.862f);
	Vector3 midPosn = new Vector3(0.112f, -0.469f, -6.862f);
		Vector3 midPosnL = new Vector3(-0.19f, -0.445f, -6.862f);
		Vector3 midPosnM = new Vector3(0.125f, -0.445f, -6.862f);
		Vector3 midPosnR = new Vector3(0.445f, -0.445f, -6.862f);
	Vector3 rightPosn = new Vector3(1.218f, -0.469f, -6.862f);
		Vector3 rightPosnL = new Vector3(0.91f, -0.445f, -6.862f);
		Vector3 rightPosnM = new Vector3(1.22f, -0.445f, -6.862f);
		Vector3 rightPosnR = new Vector3(1.545f, -0.445f, -6.862f);

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
		if (col.gameObject.tag == "LaneNote") {
			active = true;
			note = col.gameObject;
		} else if (col.gameObject.name == "EndSong") {
			Destroy (col.gameObject);
			GameMast.GetComponent<GameMasterRhythmRider> ().EndGame ();
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

	public void LeftLaneShift(){
		this.transform.position = leftPosn;
		start = leftPosn;
		leftPad.transform.position = leftPosnL;
		midPad.transform.position = leftPosnM;
		rightPad.transform.position = leftPosnR;
	}
	public void MidLaneShift(){
		this.transform.position = midPosn;
		start = midPosn;
		leftPad.transform.position = midPosnL;
		midPad.transform.position = midPosnM;
		rightPad.transform.position = midPosnR;
	}
	public void RightLaneShift(){
		this.transform.position = rightPosn;
		start = rightPosn;
		leftPad.transform.position = rightPosnL;
		midPad.transform.position = rightPosnM;
		rightPad.transform.position = rightPosnR;
	}
}		