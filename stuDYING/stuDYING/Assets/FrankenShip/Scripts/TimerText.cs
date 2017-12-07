using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerText : MonoBehaviour {

	PhaseNavigator navScript;
	Text myTextBox;


	// Use this for initialization
	void Start () {
		myTextBox = gameObject.GetComponent<Text> ();
		navScript = GameObject.FindWithTag ("GameMaster").GetComponent<PhaseNavigator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (navScript.gameTime != 0) {
			myTextBox.text = "" + navScript.gameTime;
		} else {
            if (PhaseNavigator.buildToBattlePhase)
            {
                myTextBox.text = "GO!";
            }
            else
            {
                myTextBox.text = "END";
            }
		}
	}
}
