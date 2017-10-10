using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class OpenSceneClick : MonoBehaviour {

    public string sceneToLoad;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        //yield return new WaitForSeconds(delayTime - .75f);
        //float fadeTime = GameObject.Find("_GM").GetComponent<Fading>().BeginFade(1);
        //yield return new WaitForSeconds(.75f);

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
