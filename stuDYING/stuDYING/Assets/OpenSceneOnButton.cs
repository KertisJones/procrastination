using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenSceneOnButton : MonoBehaviour {

    public string[] sceneNames;
    public string[] keysToOpenScene;

    //public float delayTime = 3;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        int i = 0;
		while (i < keysToOpenScene.Length)
        {
            if (Input.GetKeyDown(keysToOpenScene[i]))
            {
                //yield return new WaitForSeconds(delayTime - .75f);
                //float fadeTime = GameObject.Find("_GM").GetComponent<Fading>().BeginFade(1);
                //yield return new WaitForSeconds(.75f);
                SceneManager.LoadScene(sceneNames[i]);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 1f;
            }
            i += 1;
        }
	}
}
