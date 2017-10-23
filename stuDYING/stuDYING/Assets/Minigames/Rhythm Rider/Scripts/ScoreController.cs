using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

	Text myText;

	// Use this for initialization
	void Start () {
		myText = GetComponent<Text> ();
		if (this.gameObject.name == "FinalScore") {
			StartCoroutine(FinalScoreOperator ());
		}
	}

	// Update is called once per frame
	void Update () {
		if (this.gameObject.name != "FinalScore") {
			myText.text = "Score : " + GameMasterRhythmRider.totalScore;
		}
	}

	IEnumerator FinalScoreOperator(){
		myText.text = "" + GameMasterRhythmRider.totalScore;
		yield return new WaitForSeconds (5f);
		//Time.timeScale = 0f;
	}
}
