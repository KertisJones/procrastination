﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public float levelStartDelay = 2f;
	public float turnDelay = .1f;
	public static GameManager instance = null;
	public BoardManager boardScript;
	public int playerFoodPoints = 100;
	[HideInInspector] public bool playersTurn = true;

	private Text levelText;
	private GameObject levelImage;
	public int level = 1;
	private List<Enemy> enemies;
	private bool enemiesMoving;
	private bool doingSetup;

	// Use this for initialization
	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
		enemies = new List<Enemy> ();
		boardScript = GetComponent<BoardManager> ();
        if (level == 1)
		    InitGame ();
	}

	void OnLevelWasLoaded (int index)
	{
		//level++;

		//InitGame ();
	}

	public void InitGame()
	{
		doingSetup = true;

		levelImage = GameObject.Find ("LevelImage");
		levelText = GameObject.Find ("LevelText").GetComponent<Text> ();
		levelText.text = "Day " + level;
		levelImage.SetActive (true);
		Invoke ("HideLevelImage", levelStartDelay);

		enemies.Clear ();
		boardScript.SetupScene (level);
	}

	private void HideLevelImage()
	{
		levelImage.SetActive (false);
		doingSetup = false;
	}

	public void GameOver()
	{
		levelText.text = "After " + level + " days, you starved.";
		levelImage.SetActive (true);
		enabled = false;
	}

    private void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name != "Ethan Roguelike Main")
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
	
	// Update is called once per frame
	void Update () {
		if (playersTurn || enemiesMoving || doingSetup)
			return;
        for (int currBaddy = 0; currBaddy < enemies.Count; currBaddy++)
        {
            if (enemies[currBaddy] == null)
            {
                enemies.RemoveAt(currBaddy);
            }
        }

        StartCoroutine (MoveEnemies ());
    }

	public void AddEnemyToList(Enemy script)
	{
		enemies.Add (script);
	}

	IEnumerator MoveEnemies()
	{
        enemiesMoving = true;
		yield return new WaitForSeconds (turnDelay);
		if (enemies.Count == 0) {
			yield return new WaitForSeconds (turnDelay);
		}

		for (int i = 0; i < enemies.Count; i++) {
			enemies [i].MoveEnemy ();
			yield return new WaitForSeconds (enemies [i].moveTime);
		}

		playersTurn = true;
		enemiesMoving = false;
	}
}
