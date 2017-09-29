using UnityEngine;
using System.Collections;

public class PlayAudio : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D other)
	{
		
		
		if (other.tag == "Player")
		{
			GetComponent<AudioSource>().Play();

		}
	}
}
