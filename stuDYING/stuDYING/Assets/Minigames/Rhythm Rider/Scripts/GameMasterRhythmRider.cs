using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMasterRhythmRider : MonoBehaviour {

	List<float> whichNote = new List<float>() {1,6,3,4,2,5,2,1,2,3,5,6,4,6,5,5,1,2,4,1,1,4,5,5};

	public int noteMark = 0;

	public Transform noteObj;

	public string timerReset="y";

	public float xPos;

	public static int winStreak = 0;

	public Transform fountainFW;

	public bool fountainSpawnL = false;
	
	public bool fountainSpawnR = false;

	public int choose2x = 0;

	public static float totalScore = 0;

	public static int playerHealth = 100;

	public static int combo = 1;

	public AudioClip comboUp;

	public ParticleSystem leftComboPart;
	public ParticleSystem rightComboPart;

	public GameObject inGameUI;
	public GameObject endGameUI;

	// Use this for initialization
	void Start () {
		leftComboPart.Stop ();
		rightComboPart.Stop ();
	}

	// Update is called once per frame
	void Update () {
		if (timerReset == "y" && noteMark < 22) {
			//StartCoroutine (spawnNote ());
			timerReset = "n";
		}

		if (winStreak == 10 && !fountainSpawnL) {
			fountainSpawnL = true;
			//Instantiate (fountainFW, fountainFW.transform.position, fountainFW.rotation);
			leftComboPart.Play();
			rightComboPart.Play ();
			//leftComboParticle = Instantiate (leftCombo, leftCombo.transform.position, leftCombo.transform.rotation);
			//rightComboParticle = Instantiate (rightCombo, rightCombo.transform.position, rightCombo.transform.rotation);
			combo = 2;
			AudioSource.PlayClipAtPoint (comboUp, transform.position);
		}else if (winStreak == 20 && !fountainSpawnR) {
			fountainSpawnR = true;
			//Instantiate (fountainFW, new Vector3(2.019f, 0.701f, -3.739f), fountainFW.rotation);
			combo = 4;
		}
	}

	IEnumerator spawnNote(){
		yield return new WaitForSeconds (1);


		if (whichNote [noteMark] == 1) {
			xPos = -1.34f;
		}
		if (whichNote [noteMark] == 2) {
			xPos = -.7f;
		}
		if (whichNote [noteMark] == 3) {
			xPos = -.1f;
		}
		if (whichNote [noteMark] == 4) {
			xPos = .4f;
		}
		if (whichNote [noteMark] == 5) {
			xPos = .95f;
		}
		if (whichNote [noteMark] == 6) {
			xPos = 1.45f;
		}

		noteMark += 1;
		timerReset = "y";
		Instantiate (noteObj, new Vector3 (xPos, 1.2f, -2.18f), noteObj.rotation);
	}

	public void EndCombo(){
		//Destroy (leftComboParticle);
		//Destroy (rightComboParticle);
		fountainSpawnL = false;
		fountainSpawnR = false;
		leftComboPart.Stop ();
		rightComboPart.Stop ();
	}

	public void EndGame(){
		inGameUI.SetActive (false);
		endGameUI.SetActive (true);
	}
}
