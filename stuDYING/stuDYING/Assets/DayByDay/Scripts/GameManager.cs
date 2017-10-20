using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	//AudioManager
	public GameObject audioManager;
	public GameObject soundEffectManager;
	public Player player;
	public Camera camera;

	//Day Variables
	public static int customersLeft = 15;
	float dayTimer = 60;
	float timeLeft;

	bool finished = false;

	void Awake(){
		DontDestroyOnLoad (this);
		DontDestroyOnLoad (player);
		DontDestroyOnLoad (audioManager);
		DontDestroyOnLoad (soundEffectManager);
		DontDestroyOnLoad (camera);
	}

	// Use this for initialization
	void Start () {
		this.timeLeft = dayTimer;
		//Instantiate (player);
		if (SceneManager.GetActiveScene ().name == "StarterScene")
			Application.LoadLevel ("DayByDay/_Scenes/Day");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		timeLeft -= Time.deltaTime;
		if (this.timeLeft <= 0 && SceneManager.GetActiveScene ().name == "Workshop") {
			Time.timeScale = 0;
			GameObject.FindGameObjectWithTag("UINight").GetComponent<NightManager>().CompleteSceneNight();
		}else if (SceneManager.GetActiveScene().name == "Day" && (this.timeLeft <= 0 || customersLeft == 0)){
			Time.timeScale = 0;
			GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>().CompleteSceneDay();
		}
	}

	/**
	 * Checks whether the player is either out of time or customers.
	 * If so, then the day is over
	 * (NOTE: Still need to set up transition between day and night)
	 */
	public void ManagerNight(){
		Debug.Log ("Day Over!!!");
		timeLeft = dayTimer;
		customersLeft = 15;
		//UnityEditor.EditorApplication.isPlaying = false;
		audioManager.GetComponent<AudioManager>().ChangeToNight();
		Application.LoadLevel("DayByDay/_Scenes/Workshop");
		Player.LumberSold = 0;
		Player.BerrySold = 0;
		Player.CoalSold = 0;
		Player.MoneyMade = 0;
		Player.CustomersServed = 0;
		Time.timeScale = 1;
	}

	public void ManagerDay(){
		Debug.Log ("Night Over!!!");
		timeLeft = dayTimer;
		audioManager.GetComponent<AudioManager>().ChangeToDay();
		Application.LoadLevel("DayByDay/_Scenes/Day");
		Player.LumberSold = 0;
		Player.BerrySold = 0;
		Player.CoalSold = 0;
		Time.timeScale = 1;
	}

	void CompleteScreen(){
		
	}
}
