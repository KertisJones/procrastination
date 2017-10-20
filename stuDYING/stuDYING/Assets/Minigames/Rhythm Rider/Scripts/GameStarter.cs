using UnityEngine;
using System.Collections;

public class GameStarter : MonoBehaviour {

	public GameObject storyCanvas;
	public GameObject playInterface;
	public KeyCode key;
	public AudioSource audioPlayer;

	bool gameStarted;

	// Use this for initialization
	void Start () {
		Time.timeScale = 0f;
		gameStarted = false;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (key) && !gameStarted) {
			Time.timeScale = 1f;
			storyCanvas.SetActive (false);
			playInterface.SetActive (true);
			audioPlayer.Play();
			gameStarted = true;
		}
	}
}
