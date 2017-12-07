using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuNavigator : MonoBehaviour {

    public static bool tutorialMode;
	public GameObject checkmarkImage;
	public InMusicGroup mainMenuMusic;

    void Awake()
    {
		//DontDestroyOnLoad(gameObject);

    }

	// Use this for initialization
	void Start () {
		mainMenuMusic.Volume = 1.0f;
		InAudio.Music.Play (mainMenuMusic);
	}

    public void StartGame()
    {
		InAudio.Music.Stop (mainMenuMusic);
        SceneManager.LoadScene("FrankenScene");
		Destroy (gameObject);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

	public void ToggleTutorial(){
		if (tutorialMode) {
			tutorialMode = false;
			checkmarkImage.SetActive (false);
		} else {
			tutorialMode = true;
			checkmarkImage.SetActive (true);
		}
	}
}
