using System;
using System.Diagnostics;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Analytics;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PhaseNavigator : MonoBehaviour {

    /// <summary>
    /// A timer that tracks the length of each game (in milliseconds)
    /// </summary>
    Stopwatch totalGameTime = new Stopwatch();
    /// <summary>
    /// Sets a special developer mode that allows for skipping the collection phase and selection of parts in the build phase. The battle phase is normal.
    /// </summary>
	public bool DEVMODE_Build;
	public bool DEVMODE_Battle;
	bool DEVMODE;
	public bool devTime;
	public float pauseTimeScale;
	public float timeScale;
	public GameObject currentSelect;
    /// <summary>
    /// Timer for the build phase.
    /// </summary>
	public GameObject buildTimer;
    /// <summary>
    /// Timer for the build-to-battle phase transition.
    /// </summary>
    public GameObject buildToBattleTimer;
    /// <summary>
    /// Sets the length of the collection phase timer in seconds.
    /// </summary>
	public int collectionStartTime;
    /// <summary>
    /// Sets the length of the build phase timer in seconds.
    /// </summary>
	public int buildStartTime;
    /// <summary>
    /// Sets the length of the build phase to battle phase transition timer in seconds.
    /// </summary>
    public int buildToBattleStartTime;
	public int gameTime;
	private float singSize;
	private float holeSize;
	public float camZoomTime;
	private float motherTime = 2.0f;
	private float blackHoleTime = 7.0f;
	private float currentTime01 = 0.0f;
	private float currentTime02 = 0.0f;
	private float buildEndTime = 2.5f;
	/// <summary>
	/// UI Panel for the build phase.
	/// </summary>
	public GameObject collectionPanel;
    /// <summary>
    /// UI Panel for the build phase.
    /// </summary>
	public GameObject buildPanel;
    /// <summary>
    /// UI Panel for the transition from the build phase to the battle phase.
    /// </summary>
    public GameObject buildToBattlePanel;
    /// <summary>
    /// UI Panel for the battle phase.
    /// </summary>
	public GameObject battlePanel;
    /// <summary>
    /// UI Panel for end of game.
    /// </summary>
	public GameObject gameOverPanel;
    /// <summary>
    /// UI element for the button to start the battle phase.
    /// </summary>
	public GameObject lifeSwitchButton;
    /// <summary>
    /// UI element for the Xbox Control Scheme
    /// </summary>
    public GameObject controlPanelXbox;
    /// <summary>
    /// UI element for the Keyboard Control Scheme
    /// </summary>
    public GameObject controlPanelKey;
    /// <summary>
    /// UI Panel for the pause menu.
    /// </summary>
	public GameObject pauseMenu;
	public GameObject council01;
	public GameObject council02;
	public GameObject battleBoundary;
	public GameObject battleField;
    /// <summary>
    /// The black hole object for the collection phase.
    /// </summary>
	public GameObject blackHole;
    /// <summary>
    /// The center of the black hole object for the collection phase.
    /// </summary>
	public GameObject singularity;
	public GameObject p1BattleCore;
	public GameObject p2BattleCore;
	public List<GameObject> playerList;
	public List<GameObject> motherList;
	public List<GameObject> buildList;
	public List<GameObject> buildCanvasList;
	public GameObject tutorialPanel;
	public GameObject collectionTutorial;
	public GameObject buildTutorial;
	public GameObject battleTutorial;
	public GameObject tutorialButton;
	public GameObject motherFace01;
	public GameObject motherFace02;
    /// <summary>
    /// True if the game is in the collection phase.
    /// </summary>
	public static bool collectionPhase;
	/// <summary>
	/// True if the game is in the collection phase.
	/// </summary>
	public bool collectionToBuildPhase;
    /// <summary>
    /// True if the game is in the build phase.
    /// </summary>
	public bool buildPhase;
    /// <summary>
    /// True if the game is in the battle phase.
    /// </summary>
	public static bool battlePhase;
    /// <summary>
    /// True if the game is transitioning from the build phase to the battle phase.
    /// </summary>
    public static bool buildToBattlePhase;
	public bool zoomCamIn;
	public bool zoomCamOut;
    /// <summary>
    /// True if the game is paused.
    /// </summary>
    public bool pause;
    /// <summary>
    /// True if tutorial mode is active.
    /// </summary>
    public bool lclTutorialMode; 
    /// <summary>
    /// True if a tutorial is active.
    /// </summary>
    public bool tutorial;
    /// <summary>
    /// True if the timer has started.
    /// </summary>
	public bool timerStarted;
	private bool fadeBuild;
	GameObject alivePlayer;
    //music things
	public InMusicGroup collectionMusic;
	public InMusicGroup buildMusic;
	public InMusicGroup battleMusic;
	public InMusicGroup musicgroup;
	public InAudioNode continueWhoosh;
    public InAudioNode randomLaughter;
	public InAudioNode gameOverSfx;
	public InAudioNode victoryP1;
	public InAudioNode deathP1;
	public InAudioNode victoryP2;
	public InAudioNode deathP2;
    
    // Use this for initialization
	void Start () {
		if (DEVMODE_Build || DEVMODE_Battle) {
			DEVMODE = true;
		}

        totalGameTime.Start(); //start timer for full round time.
		singSize = singularity.GetComponentInChildren<ParticleSystem> ().startSize;
		holeSize = blackHole.GetComponentInChildren<ParticleSystem> ().maxParticles;
		if (!DEVMODE) {
			StartCoroutine (StartCollection ());
		} else if (DEVMODE_Build) {
			StartCoroutine (EndCollection ());
		} else if (DEVMODE_Battle) {
			StartCoroutine (CollectionToBattle ());
		}
        pauseMenu.SetActive(false);
        //tutorial window initialization
		if (!lclTutorialMode) {
			lclTutorialMode = MainMenuNavigator.tutorialMode;
		}
    }

    // Update is called once per frame
    void Update() {
		currentSelect = EventSystem.current.currentSelectedGameObject;
		if (devTime)
        {
			timeScale = Mathf.Clamp (timeScale, 1.0f, 20.0f);
			Time.timeScale = timeScale;
		}
        else
        {
			timeScale = Time.timeScale;
		}
        if (collectionPhase)
        {
            if (gameTime == 0)
            {
                StartCoroutine(EndCollection());
            }
            else if (!timerStarted)
            {
                timerStarted = true;
                StartCoroutine(MoveTimer());
            }
        }
        else if (buildPhase)
        {
			
        }
        else if (battlePhase)
        {

        }
        else if (buildToBattlePhase)
        {
            if(gameTime == 0)
            {
                ; //Ah, the good ole fashioned "Do Nothing" statement.
            }
            else if (!timerStarted)
            {
                timerStarted = true;
                StartCoroutine(TransitionTimer());
            }
        }
        if (zoomCamIn) {
            if (currentTime01 <= camZoomTime) {
				currentTime01 += Time.unscaledDeltaTime;
                float lerp = currentTime01 / camZoomTime;
                Camera.main.orthographicSize = Mathf.Lerp(75, 15, lerp);
                singularity.GetComponentInChildren<ParticleSystem>().startSize = Mathf.Lerp(singSize, 0, lerp);
				blackHole.GetComponentInChildren<ParticleSystem>().maxParticles = Mathf.RoundToInt(Mathf.Lerp(holeSize, 250f, lerp));
				blackHole.transform.GetChild (1).localScale = Vector3.Lerp (Vector3.one, Vector3.one * 5, lerp * 0.75f);
				blackHole.transform.GetChild (2).localScale = Vector3.Lerp (Vector3.one, Vector3.zero, lerp);
				SpriteRenderer mother01 = motherFace01.GetComponent<SpriteRenderer> ();
				SpriteRenderer mother02 = motherFace02.GetComponent<SpriteRenderer> ();
				mother01.color = Color.Lerp (new Color (mother01.color.r, mother01.color.g, mother01.color.b, 1.0f), new Color (mother01.color.r, mother01.color.g, mother01.color.b, 0.0f), lerp * 5.0f);
				mother02.color = Color.Lerp (new Color (mother02.color.r, mother02.color.g, mother02.color.b, 1.0f), new Color (mother02.color.r, mother02.color.g, mother02.color.b, 0.0f), lerp * 5.0f);
				foreach (SpriteRenderer spriter in blackHole.transform.GetChild(1).GetComponentsInChildren<SpriteRenderer>()) {
					spriter.color = Color.Lerp (new Color (spriter.color.r, spriter.color.g, spriter.color.b, 0.3f), new Color (spriter.color.r, spriter.color.g, spriter.color.b, 0.1f), lerp);
				}

				foreach (GameObject builder in buildList) {
					SpriteRenderer sprite = builder.GetComponentInChildren<SpriteRenderer> ();
					float fullTransparency = 0.175f;
					sprite.transform.localScale = Vector3.Lerp (new Vector3(0.0f, 0.0f, 1.0f) , new Vector3 (15.8f, 9.0f, 1.0f), lerp);
					sprite.color = Color.Lerp (new Color (sprite.color.r, sprite.color.g, sprite.color.b, 0.0f), new Color (sprite.color.r, sprite.color.g, sprite.color.b, fullTransparency), lerp);
				}

				foreach (GameObject mother in motherList) {
					foreach (ParticleSystem system in mother.GetComponentsInChildren<ParticleSystem>()) {
						system.enableEmission = false;
					}
					foreach (Transform child in mother.transform) {
						if (child.tag == "StasisField01" || child.tag == "StasisField02") {
							child.transform.localScale = Vector3.Lerp(Vector3.one, new Vector3(1, 0, 1), lerp * 10.0f);
						}
					}
				}
				float lerpRatio = 0.25f;
				if (lerp <= lerpRatio) {
					Time.timeScale = Mathf.SmoothStep (1.0f, 20.0f, Mathf.SmoothStep(0.0f, 1.0f, lerp/lerpRatio));
				} else {
					Time.timeScale = Mathf.SmoothStep (20.0f, 1.0f, Mathf.SmoothStep(0.0f, 1.0f, (lerp - lerpRatio) / (1 - lerpRatio)));
				}
            } else {
				
                zoomCamIn = false;
                currentTime01 = 0.0f;
            }
        }
        if (zoomCamOut) {
            if (currentTime01 <= camZoomTime) {
				currentTime01 += Time.unscaledDeltaTime;
                float lerp = currentTime01 / camZoomTime;
                Camera.main.orthographicSize = Mathf.Lerp(15, 75, lerp);
                singularity.GetComponentInChildren<ParticleSystem>().startSize = Mathf.Lerp(singSize, 0, lerp);
                blackHole.GetComponentInChildren<ParticleSystem>().maxParticles = Mathf.RoundToInt(Mathf.Lerp(150f, 0, lerp));
				blackHole.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, currentTime01 / blackHoleTime);
				singularity.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, currentTime01 / blackHoleTime);
				council01.transform.localPosition = Vector3.Lerp (Vector3.left * 1100.0f, Vector3.left * 830.0f, Mathf.SmoothStep (0.0f, 1.0f, lerp));
				council02.transform.localPosition = Vector3.Lerp (Vector3.right * 1100.0f, Vector3.right * 840.0f, Mathf.SmoothStep (0.0f, 1.0f, lerp));
				Time.timeScale = Mathf.SmoothStep (25.0f, 1.0f, lerp);
            } else {
                zoomCamOut = false;
				currentTime01 = 0.0f;
                StartCoroutine(StartBattle());
            }
        }

		if (fadeBuild) {
			if (currentTime02 <= buildEndTime) {
				currentTime02 += Time.unscaledDeltaTime;
				float lerp = currentTime02 / buildEndTime;
				foreach (GameObject builder in buildList) {
					foreach (ParticleSystem system in builder.GetComponentsInChildren<ParticleSystem>()) {
						system.startColor = Color.Lerp (Color.white, Color.clear, lerp);
					}
					foreach (SpriteRenderer spriter in builder.GetComponentsInChildren<SpriteRenderer>()) {
						spriter.color = Color.Lerp (new Color (spriter.color.r, spriter.color.g, spriter.color.b, 0.5f),
							new Color (spriter.color.r, spriter.color.g, spriter.color.b, 0.0f), lerp);
					}
				}
			} else {
				fadeBuild = false;
			}
		}

        if (ControlManager.P1Start || ControlManager.P2Start)
        {
			if (!tutorial) {
				if (pause) {
					EndPause ();
				} else {
					StartPause ();
				}
			}
        }

		if (pause) {
			if (collectionPhase) {
				foreach (GameObject player in playerList) {
					player.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;
					player.GetComponentInChildren<GravGunPull> ().GetComponent<PolygonCollider2D> ().enabled = false;
					player.GetComponentInChildren<GravGunPull> ().enabled = false;
				}
			} else if (buildPhase) {
				foreach (GameObject canvas in buildCanvasList) {
					canvas.GetComponentInChildren<CursorMovement> ().enabled = false;
					Button[] buttons = canvas.GetComponentsInChildren<Button> ();
					foreach (Button button in buttons) {
						button.interactable = false;
					}
					Button[] lifeButtons = lifeSwitchButton.GetComponentsInChildren<Button> ();
					foreach (Button lifeButton in lifeButtons) {
						lifeButton.interactable = false;
					}
				}
				foreach (GameObject builder in buildList) {
					builder.GetComponent<ShipBuilder> ().enabled = false;
				}
			}
            else if (battlePhase)
            {
                Time.timeScale = 0.0f;
			}
		}
    }

	public void PauseGame(){
		pause = true;
		pauseTimeScale = Time.timeScale;
		Time.timeScale = 0.0f;
	}

	public void UnPauseGame(){
		Time.timeScale = 1.0f;
		controlPanelXbox.SetActive(false);
		if (collectionPhase) {
			foreach (GameObject player in playerList) {
				player.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.None;
				player.GetComponentInChildren<GravGunPull> ().GetComponent<PolygonCollider2D> ().enabled = true;
				player.GetComponentInChildren<GravGunPull> ().enabled = true;
			}
		}
        else if (buildPhase) {
			foreach (GameObject canvas in buildCanvasList) {
				canvas.GetComponentInChildren<CursorMovement> ().enabled = true;
				Button[] buttons = canvas.transform.parent.GetComponentsInChildren<Button> ();
				foreach (Button button in buttons) {
					button.interactable = true;
				}
				Button[] lifeButtons = lifeSwitchButton.GetComponentsInChildren<Button> ();
				foreach (Button lifeButton in lifeButtons) {
					lifeButton.interactable = true;
				}
				foreach (GameObject builder in buildList) {
					builder.GetComponent<ShipBuilder> ().enabled = true;
				}
			}
			EventSystem.current.GetComponent<CursorObjectEvents> ().enabled = true;
		}
        else if (battlePhase)
        {
            foreach (GameObject player in playerList)
            {
                //player.transform.FindChild("Core").gameObject.SetActive(true);
            }
        }
        pause = false;
	}

    //pause menu shennanigans
    public void StartPause()
    {
		
		PauseGame ();
		pauseMenu.SetActive(true);
		if (buildPhase) {
			EventSystem.current.GetComponent<CursorObjectEvents> ().enabled = false;
		}
        if (battlePhase)
        {
            foreach (GameObject player in playerList)
            {
                //player.transform.FindChild("Core").gameObject.SetActive(false);
            }
        }
		EventSystem.current.SetSelectedGameObject (pauseMenu.GetComponentInChildren<Button> ().gameObject);
		EventSystem.current.firstSelectedGameObject = pauseMenu.GetComponentInChildren<Button> ().gameObject;
		StartCoroutine (PauseWait ());
    }

	IEnumerator PauseWait(){
		yield return new WaitForSeconds (1);

	}

    public void EndPause()
    {
		
		UnPauseGame ();
		pauseMenu.SetActive (false);
        EventSystem.current.firstSelectedGameObject = null;
		InAudio.Play (gameObject, continueWhoosh, null);
        
    }

	//tutorial control
	public void StartTutorial()
	{
		tutorial = true;
		tutorialPanel.SetActive (true);
		PauseGame ();

		if (collectionPhase) {
			collectionTutorial.SetActive (true);

		} else if (buildPhase) {
			buildTutorial.SetActive (true);
		} else {
			battleTutorial.SetActive(true);
		}

		EventSystem.current.SetSelectedGameObject (tutorialButton);
	}

	public void EndTutorial()
	{
		InAudio.Play (gameObject, continueWhoosh, null);


		if (collectionPhase) {
			if (collectionTutorial.GetComponent<TutorialPageFlip> ().tutPageNum != collectionTutorial.GetComponent<TutorialPageFlip> ().pageTexts.Count - 1) {
				collectionTutorial.GetComponent<TutorialPageFlip> ().tutPageNum++;
			} else {
				collectionTutorial.SetActive (false);
				tutorialPanel.SetActive (false);
				UnPauseGame ();
				tutorial = false;
			}
		} else if (buildPhase) {
			if (buildTutorial.GetComponent<TutorialPageFlip> ().tutPageNum != buildTutorial.GetComponent<TutorialPageFlip> ().pageTexts.Count - 1) {
				buildTutorial.GetComponent<TutorialPageFlip> ().tutPageNum++;
			} else {
				buildTutorial.SetActive (false);
				tutorialPanel.SetActive (false);
				UnPauseGame ();
				tutorial = false;
			}
		} else if (battlePhase) {
			if (battleTutorial.GetComponent<TutorialPageFlip> ().tutPageNum != battleTutorial.GetComponent<TutorialPageFlip> ().pageTexts.Count - 1) {
				battleTutorial.GetComponent<TutorialPageFlip> ().tutPageNum++;
			} else {
				battleTutorial.SetActive (false);
				tutorialPanel.SetActive (false);
				UnPauseGame ();
				tutorial = false;
			}
		}
	}

	//end tutorial control
    
    public void LoadControls()
    {
        controlPanelXbox.SetActive(true);
        EventSystem.current.SetSelectedGameObject(controlPanelXbox.GetComponentInChildren<Button>().gameObject);
    }

    public void UnloadControl()
    {
        controlPanelXbox.SetActive(false);
        EventSystem.current.SetSelectedGameObject(pauseMenu.GetComponentInChildren<Button>().gameObject);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

	public void InitiateBuild(){
		StartCoroutine (StartBuild ());
	}

	public void TerminateBuild(){
		StartCoroutine (EndBuild());
	}

    public void InitiateBattle(){
		StartCoroutine (BuildToBattle ());
	}

	public void TerminateBattle(GameObject winner){
		StartCoroutine (EndBattle (winner));
	}

	public void RestartGame(){
		InAudio.StopAllAndMusic ();
		SceneManager.LoadScene("FrankenScene");
		Time.timeScale = 1.0f;
	}

    

    IEnumerator MoveTimer() {
        yield return new WaitForSeconds(1);
        if(!pause)
        {
            if (!tutorial)
            {
                gameTime--;
            }
        }
		timerStarted = false;
	}

    IEnumerator TransitionTimer()
    {
        while (true)
        {
            float transition = Time.realtimeSinceStartup + 1.0f;
            while (Time.realtimeSinceStartup < transition)
            {
                yield return 0;
            }
            gameTime--;
            if(gameTime == 0)
            {
                StopCoroutine("TransitionTimer");
            }
        }
    }

	IEnumerator CollectionToBattle() {
		foreach (GameObject mother in motherList) {
			mother.SetActive (false);
		}
		blackHole.SetActive (false);
		singularity.SetActive (false);
		foreach (GameObject player in playerList) {
			player.transform.GetChild (1).gameObject.SetActive (true);
			Destroy (player.transform.GetChild (0).gameObject);
			foreach (CheckConnection checker in player.GetComponentsInChildren<CheckConnection>()) {
				checker.GetComponent<CircleCollider2D> ().enabled = false;
				checker.enabled = false;
			}
			yield return new WaitForSeconds (0.1f);
			player.GetComponentInChildren<SkillShot> ().enabled = true;
			player.GetComponentInChildren<ShipBaseMovement> ().enabled = true;

			Animator[] anims = player.GetComponentsInChildren<Animator> ();
			foreach (Animator anim in anims) {
				anim.enabled = true;
			}
			foreach (HullHealth health in player.GetComponentsInChildren<HullHealth>()) {
				health.enabled = true;
			}


			player.GetComponent<PowerUpCoordinator> ().enabled = true;
			player.GetComponent<Rigidbody2D> ().useAutoMass = true;
		}
		battlePanel.SetActive (true);
		battleBoundary.SetActive (true);
		gameObject.GetComponent<PowerUpSpawner> ().enabled = true;
		Camera.main.GetComponent<ZoomCamera>().enabled = true;
		battlePhase = true;

	}

    IEnumerator StartCollection(){
		Time.timeScale = 1.0f;
		InAudio.Music.Play (collectionMusic);
        yield return new WaitForSeconds (0.1f);
		collectionPanel.SetActive (true);
		gameTime = collectionStartTime;
		foreach (GameObject player in playerList) {
			player.transform.GetChild (0).gameObject.SetActive (true);
		}

		collectionPhase = true;
		if (lclTutorialMode) {
			StartTutorial ();
		}
		
    }

	IEnumerator EndCollection(){
		foreach (GameObject player in playerList) {
			GameObject IGOR = player.transform.GetChild (0).gameObject;
			PolygonCollider2D[] polys = IGOR.GetComponentsInChildren<PolygonCollider2D> ();
			foreach (PolygonCollider2D poly in polys) {
				poly.isTrigger = true;
			}
			IGOR.GetComponentInChildren<ThrusterMove> ().enabled = false;
			IGOR.GetComponentInChildren<GravGunPull> ().ShootGravGun ();
			ParticleSystem[] systems = IGOR.GetComponentsInChildren<ParticleSystem> ();
			foreach (ParticleSystem system in systems) {
				system.enableEmission = false;
			}
			player.GetComponentInChildren<ShipBaseMovement> ().enabled = false;
		}
		blackHole.GetComponent<BlackHoleSpawner> ().enabled = false;
		blackHole.GetComponent<BlackHoleSpawner> ().StopAllCoroutines ();
		foreach (GameObject mother in motherList) {
			mother.GetComponent<MotherShipPull> ().enabled = true;
			mother.GetComponent<BoxCollider2D> ().enabled = true;
		}
		Time.timeScale = 5.0f;
		yield return new WaitForSeconds (25);

		foreach (BlackHoleObjectState crate in FindObjectsOfType(typeof(BlackHoleObjectState)) as BlackHoleObjectState[]){
			crate.GetComponent<SpriteRenderer> ().sortingLayerName = "Background";
		}
		singularity.GetComponent<SingularityPull> ().deathDistance = 0.1f;
		blackHole.GetComponent<CircleCollider2D> ().radius = 125;
		blackHole.GetComponent<BlackHolePole> ().pullAmountMin = 1.0f;
		blackHole.GetComponent<BlackHolePole> ().pullAmountMax = 1.25f;
		blackHole.GetComponent<BlackHolePole> ().rotateAmountMin = 0.1f;
		blackHole.GetComponent<BlackHolePole> ().rotateAmountMax = 0.5f;

		//blackHole.GetComponent<BlackHolePole> ().enabled = false;
		//blackHole.GetComponent<CircleCollider2D> ().enabled = false;
        collectionPhase = false;
		StartCoroutine (CollectionToBuild ());
	}

	IEnumerator CollectionToBuild(){
		//buildMusic.Volume = 0.0f;
		InAudio.Music.Play (buildMusic);
		InAudio.Music.SwapCrossfadeVolume(collectionMusic, buildMusic, 7.5f, InAudioLeanTween.LeanTweenType.easeInOutQuad);

		Time.timeScale = 1.0f;
		yield return new WaitForSeconds (0.1f);
		collectionPanel.SetActive (false);
		foreach (GameObject player in playerList) {
			player.transform.GetChild(0).GetComponent<LockLocalPos> ().myLocPos = Vector3.down * 3.5f;
			player.transform.GetChild (0).gameObject.GetComponentInChildren<Animator> ().SetTrigger ("Still");
			player.transform.GetChild (1).gameObject.SetActive (true);
			player.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.None;
			PolygonCollider2D[] polys = player.GetComponentsInChildren<PolygonCollider2D> ();
			foreach (PolygonCollider2D poly in polys) {
				poly.isTrigger = true;
			}
			ParticleSystem[] systems = player.transform.GetChild(0).GetComponentsInChildren<ParticleSystem> ();
			foreach (ParticleSystem system in systems) {
				system.enableEmission = true;
			}
            //TODO: Split this based on player number
            if (player.tag == "Player01")
            {
                Analytics.CustomEvent("Player 1 Inventory Size at Collection End", new Dictionary<string, object>
                {
                    { "player", player.tag },
                    { "inventorySize", player.GetComponent<Inventory>().invParts.Count }
                });
            }
            else
            {
                Analytics.CustomEvent("Player 2 Inventory Size at Collection End", new Dictionary<string, object>
                {
                    { "player", player.tag },
                    { "inventorySize", player.GetComponent<Inventory>().invParts.Count }
                });
            }
            //temporary variables for analytics purposes
            int steelCrate = 0;
            int goldCrate = 0;
            int diamondCrate = 0;

            foreach (GameObject crate in player.GetComponent<Inventory>().invParts)
            {
                switch (crate.tag)
                {
                    case "LootCrate01":
                        steelCrate++;
                        break;
                    case "LootCrate02":
                        goldCrate++;
                        break;
                    case "LootCrate03":
                        diamondCrate++;
                        break;
                    default:
                        break;
                }
            }
            if (player.tag == "Player01")
            {
                Analytics.CustomEvent("Player 1 Types of Crates", new Dictionary<string, object>
                {
                    { "player", player.tag },
                    { "steelCrates", steelCrate },
                    { "goldCrates", goldCrate },
                    { "diamondCrates", diamondCrate }
                });
            }
            else
            {
                Analytics.CustomEvent("Player 2 Types of Crates", new Dictionary<string, object>
                {
                    { "player", player.tag },
                    { "steelCrates", steelCrate },
                    { "goldCrates", goldCrate },
                    { "diamondCrates", diamondCrate }
                });
            }
        }

		foreach (GameObject mother in motherList) {
			mother.GetComponent<MotherShipPull> ().enabled = false;
		}

		foreach (GameObject builder in buildList) {
			builder.SetActive (true);
		}
		BattleFieldsParallax fieldScript = battleField.GetComponent<BattleFieldsParallax> ();
		int fieldNum = UnityEngine.Random.Range (0, 3);
		if (fieldNum == 0) {
			fieldScript.fieldType = BattleFieldsParallax.FieldTypes.Ice;
		} else if (fieldNum == 1) {
			fieldScript.fieldType = BattleFieldsParallax.FieldTypes.Asteroid;
		} else if (fieldNum == 2) {
			fieldScript.fieldType = BattleFieldsParallax.FieldTypes.Junk;
		}
		fieldScript.enabled = true;

		zoomCamIn = true;
	}

	IEnumerator StartBuild(){
		InAudio.Music.Stop (collectionMusic);
		foreach (BlackHoleObjectState crate in FindObjectsOfType(typeof(BlackHoleObjectState)) as BlackHoleObjectState[]){
			//crate.gameObject.SetActive (false);
		}

		foreach (GameObject mother in motherList) {
			foreach (SpriteRenderer sprite in mother.GetComponentsInChildren<SpriteRenderer>()) {
				sprite.enabled = false;
			}
		}

		foreach (GameObject player in playerList) {
			CircleCollider2D[] connections = player.GetComponentsInChildren<CircleCollider2D> ();
			foreach (CircleCollider2D conn in connections) {
				conn.enabled = true;
			}

			GridConnection[] grids = player.GetComponentsInChildren<GridConnection> ();
			foreach (GridConnection grid in grids) {
				grid.GetComponent<CircleCollider2D>().enabled = true;
			}
		}
		foreach (GameObject canvas in buildCanvasList) {
			if (!DEVMODE_Build) {
				canvas.transform.GetChild (0).gameObject.SetActive (true);
			} else {
				canvas.transform.GetChild (1).gameObject.SetActive (true);
			}
		}
		foreach (GameObject builder in buildList) {
			if (DEVMODE_Build) {
				builder.transform.GetChild (0).gameObject.SetActive (true);
			}
			builder.GetComponentInChildren<ShipBuilder> ().enabled = true;
		}
		blackHole.transform.GetChild (1).gameObject.SetActive (false);
		buildPanel.SetActive (true);
		//buildPanel.GetComponentInChildren<TimerText> ().enabled = true;
		if (!DEVMODE_Build) {
			gameTime = buildStartTime;
		} else {
			lifeSwitchButton.GetComponent<Animator> ().SetTrigger ("Both");
			foreach (Button button in lifeSwitchButton.GetComponentsInChildren<Button>()) {
				button.enabled = true;
			}
		}
		foreach (GameObject builder in buildList) {
			builder.GetComponentInChildren<BuildAnimHandler> ().enabled = true;
		}
		buildPhase = true;
        //If tutorial mode is enabled, bring up the Tutorial UI.
        if (lclTutorialMode)
            StartTutorial();
		yield return new WaitForSeconds (0.5f);
		if (!pause && !tutorial) {
			EventSystem.current.gameObject.GetComponent<CursorObjectEvents> ().enabled = true;
		}
    }
		

	IEnumerator EndBuild(){
		buildPhase = false;
		foreach (GameObject builder in buildList) {
            InAudio.Play(gameObject, randomLaughter, null);
		}
		foreach (GameObject player in playerList) {
			foreach (CheckConnection checker in player.GetComponentsInChildren<CheckConnection>()) {
				if (!checker.connected) {
					checker.GetComponent<CircleCollider2D> ().enabled = false;
				}
			}
		}
		EventSystem.current.GetComponent<CursorObjectEvents> ().enabled = false;
		fadeBuild = true;
		yield return new WaitForSeconds (buildEndTime);
		buildPanel.SetActive (false);
		foreach (GameObject builder in buildList) {
			builder.SetActive (false);
		}
		StartCoroutine (BuildToBattle ());
	}

	IEnumerator BuildToBattle(){
		battleMusic.Volume = 0.0f;
		InAudio.Music.Play (battleMusic);
		InAudio.Music.SwapCrossfadeVolume(buildMusic, battleMusic, 100.0f, InAudioLeanTween.LeanTweenType.easeInOutQuad);
		buildPhase = false;
        buildToBattlePhase = true;
        yield return new WaitForSeconds (0.0f);
		blackHole.GetComponent<BlackHolePole> ().pullAmountMax = 10.0f;
        buildToBattlePanel.SetActive(true);
        buildToBattleTimer.GetComponent<TimerText>().enabled = true;
        gameTime = buildToBattleStartTime;
        buildToBattleTimer.GetComponent<TimerText>().enabled = true;
        foreach (GameObject player in playerList) {
			player.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.None;
			player.GetComponentInChildren<Animator> ().SetTrigger ("Still");
		}
		foreach (GameObject mother in motherList) {
			mother.GetComponent<MotherShipPull> ().enabled = true;
			mother.GetComponent<MotherShipPull> ().pullStrength = 14.0f;
			Vector3 moveDir = mother.transform.position - Vector3.zero;
			mother.transform.position = moveDir.normalized * 115;
		}
		battlePanel.SetActive (true);
		zoomCamOut = true;
		battleBoundary.SetActive (true);
	}

	IEnumerator StartBattle(){
		InAudio.Music.Stop (buildMusic);
        buildToBattlePanel.SetActive(false);
        buildToBattlePhase = false;
        Camera.main.GetComponent<ZoomCamera>().enabled = true;
        yield return new WaitForSeconds (0.1f);
		battlePhase = true;
		EventSystem.current.gameObject.GetComponent<CursorObjectEvents> ().enabled = false;
		foreach (GameObject player in playerList) {
			player.GetComponentInChildren<ShipBaseMovement> ().enabled = true;
			player.GetComponent<PowerUpCoordinator> ().enabled = true;
			player.GetComponentInChildren<SkillShot> ().enabled = true;

			PolygonCollider2D[] polys = player.GetComponentsInChildren<PolygonCollider2D> ();
			foreach (PolygonCollider2D poly in polys) {
				poly.isTrigger = false;
			}

			ShootShoot[] shoots = player.GetComponentsInChildren<ShootShoot> ();
			foreach (ShootShoot shooter in shoots) {
				shooter.enabled = true;
			}

			ThrusterMove[] moves = player.GetComponentsInChildren<ThrusterMove> ();
			foreach (ThrusterMove thruster in moves) {
				thruster.enabled = true;
			}

			player.GetComponent<Rigidbody2D> ().useAutoMass = true;

			Animator[] anims = player.GetComponentsInChildren<Animator> ();
			foreach (Animator anim in anims) {
				anim.enabled = true;
			}

			HullHealth[] healths = player.GetComponentsInChildren<HullHealth> ();
			foreach (HullHealth health in healths) {
				health.enabled = true;
			}
		}

		foreach (GameObject mother in motherList) {
			mother.GetComponent<MotherShipPull> ().enabled = false;
		}
		GameObject.FindWithTag ("GameMaster").GetComponent<PowerUpSpawner> ().enabled = true;

        //If tutorial mode is enabled, bring up the Tutorial UI.
        if (lclTutorialMode)
            StartTutorial();
    }

	IEnumerator EndBattle(GameObject deadPlayer){
		if (deadPlayer.tag == "Player01") {
			alivePlayer = playerList [1];
		} else {
			alivePlayer = playerList [0];
		}
		InAudio.Play(gameObject, gameOverSfx, null);
		foreach (GameObject player in playerList) {
			player.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;
			player.GetComponentInChildren<SpriteRenderer> ().color = Color.white;
		}
		alivePlayer.GetComponentInChildren<SkillShot> ().enabled = false;
		deadPlayer.GetComponentInChildren<SkillShot> ().enabled = false;
		alivePlayer.GetComponentInChildren<PowerUpCoordinator> ().enabled = false;
		deadPlayer.GetComponentInChildren<PowerUpCoordinator> ().enabled = false;
		yield return new WaitForSeconds (2);
		alivePlayer.GetComponentInChildren<Animator> ().SetTrigger ("Laugh");
		alivePlayer.GetComponentInChildren<PolygonCollider2D> ().enabled = false;
		if (alivePlayer.tag == "Player01") {
			//InAudio.Play (gameObject, victoryP1, null);
			InAudio.Play (gameObject, deathP2, null);
		} else {
			//InAudio.Play (gameObject, victoryP2, null);
			InAudio.Play (gameObject, deathP1, null);
		}
		//InAudio.Play(gameObject, randomLaughter, null);
		gameOverPanel.GetComponentInChildren<Text>().text = alivePlayer.tag + " WINS!!!";
		gameOverPanel.SetActive (true);
		EventSystem.current.SetSelectedGameObject (gameOverPanel.GetComponentInChildren<Button> ().gameObject);
        totalGameTime.Stop();// stop the timer
        TimeSpan ts = totalGameTime.Elapsed; //read the time
        double elapsedTime = ts.TotalSeconds; //Send total game time to analytics in seconds.
        Analytics.CustomEvent("Total Gameplay Time per Round in Seconds", new Dictionary<string, object>
        {
            { "Run Time", elapsedTime }
        });
		
	}
}
