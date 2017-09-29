using UnityEngine;
using System.Collections;
using System;

public class WinRestarter : MonoBehaviour {



	private ScoreManager ScoreManager;

	void Awake ()
	{
		ScoreManager = GetComponent<ScoreManager> ();



	}

	

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			GetComponent<AudioSource>().Play();


			Vector3 temp = other.transform.position;
			

			
			temp.x = -8f;
			temp.y = 1.8f;
			
			other.transform.position = temp; 



			ScoreManager.score += .5;
			//ScoreManager.score = Math.Ceiling (ScoreManager.score);

		}
	}



}
