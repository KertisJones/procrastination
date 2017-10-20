using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NoteControl : MonoBehaviour {

	public Transform successBurst;
	public Transform failBurst;

	public GameObject healthBar;

	public AudioClip comboDown;

	Slider health;

	Vector3 start;
	Vector3 end;
	float speed;
	Vector3 vel;

	public GameObject GameMast;

	// Use this for initialization
	void Start () {
		//GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, -2);
		GetComponent<Rigidbody>().freezeRotation = true;
		start = this.transform.position;
		//end = new Vector3 (start.x, -9.689f, -43.009f);
		end = new Vector3 (0, -11.45f, -43.009f);
		//speed = 0.036f;
		speed = 0.05f;
		health = healthBar.GetComponent<Slider> ();
		Vector3 vel = new Vector3 (0, 0, -2);
	}
	
	// Update is called once per frame
	void Update () {
		//this.transform.position = Vector3.Lerp (start, end, speed * Time.deltaTime);
		//transform.position = transform.position + vel *speed * Time.deltaTime;
		GetComponent<Rigidbody>().MovePosition(transform.position + end*speed*Time.deltaTime);
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.name == "MissedNoteCollider") {
			Destroy (gameObject);
			Debug.Log ("FAIL");
			GameMasterRhythmRider.winStreak = 0;
			if (GameMasterRhythmRider.combo > 1) {
				AudioSource.PlayClipAtPoint (comboDown, transform.position);
				GameMast.GetComponent<GameMasterRhythmRider> ().EndCombo ();
			}
			GameMasterRhythmRider.combo = 1;
			Instantiate (failBurst, transform.position, failBurst.rotation);
			GameMasterRhythmRider.totalScore -= 5;
			GameMasterRhythmRider.playerHealth -= 5;
			health.value = GameMasterRhythmRider.playerHealth;
			Debug.Log ("New health: " + GameMasterRhythmRider.playerHealth);
			if (GameMasterRhythmRider.playerHealth <= 0) {
				Application.LoadLevel (0);
			}
		}
		if (other.gameObject.tag == "Success") {
			Destroy (gameObject);
			Debug.Log ("SUCCESS");
			Instantiate (successBurst, transform.position, successBurst.rotation);
			GameMasterRhythmRider.winStreak++;
			GameMasterRhythmRider.totalScore += (GameMasterRhythmRider.combo * 10);
			GameMasterRhythmRider.playerHealth += 5;
			if (GameMasterRhythmRider.playerHealth >= 100)
				GameMasterRhythmRider.playerHealth = 100;
			health.value = GameMasterRhythmRider.playerHealth;
		}
	}
}
