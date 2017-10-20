using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

    public string sceneToLoad;
    public float delayTime = 3;

    // Use this for initialization
    void Start () {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("escape"))
        {
            if (sceneToLoad == "QUIT")
            {
                Application.Quit();
            }
            else
            {
                //yield return new WaitForSeconds(delayTime - .75f);
                //float fadeTime = GameObject.Find("_GM").GetComponent<Fading>().BeginFade(1);
                //yield return new WaitForSeconds(.75f);
                SceneManager.LoadScene(sceneToLoad);
            }
            
        }
    }
}
