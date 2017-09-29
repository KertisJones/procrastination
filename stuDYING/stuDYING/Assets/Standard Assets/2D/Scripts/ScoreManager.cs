using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ScoreManager : MonoBehaviour
{
	public static double score;        // The player's score.
    public GameObject obsticle;
	
	Text text;                      // Reference to the Text component.
	
	
	void Awake ()
	{
		// Set up the reference.
		text = GetComponent <Text> ();
		
		// Reset the score.
		score = 0;
	}
	
	
	void Update ()
	{
		//Exit the game on Escape
		//if (Input.GetKey(KeyCode.Escape)) { Application.Quit(); } 

		// Set the displayed text to be the word "Score" followed by the score value.
		ScoreManager.score = Math.Ceiling (ScoreManager.score);
		//text.text = "Score: " + score;
        if (score > 0 && obsticle != null)
        {
            obsticle.SetActive(true);
        }

		text.text = "Score: " + score;
	}
}