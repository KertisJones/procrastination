using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ObstacleControl : MonoBehaviour {

	public Transform successBurst;
	public Transform failBurst;

	public GameObject healthBar;

	Slider health;

	Vector3 start;
	Vector3 end;
	float speed;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, -2);
		GetComponent<Rigidbody>().freezeRotation = true;
		start = this.transform.position;
		end = new Vector3 (0, -11.45f, -43.009f);
		speed = 0.05f;
		health = healthBar.GetComponent<Slider> ();
	}

	// Update is called once per frame
	void Update () {
		//this.transform.position = Vector3.Lerp (this.transform.position, end, speed * Time.deltaTime);
		GetComponent<Rigidbody>().MovePosition(transform.position + end*speed*Time.deltaTime);
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.name == "FailCollider") {
			Destroy (gameObject);
			Debug.Log ("Dodged Obstacle");
			GameMasterRhythmRider.totalScore += 5;
			GameMasterRhythmRider.playerHealth += 5;
			health.value = GameMasterRhythmRider.playerHealth;
		}
		if (other.gameObject.name == "NoteHitter") {
			Destroy (gameObject);
			Debug.Log ("Hit Obstacle");
			//GameMasterRhythmRider.winStreak++;
			//GameMasterRhythmRider.totalScore += 10;
			GameMasterRhythmRider.playerHealth -= 10;
			health.value = GameMasterRhythmRider.playerHealth;
		}
	}
}