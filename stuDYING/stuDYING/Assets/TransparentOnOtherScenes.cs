using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransparentOnOtherScenes : MonoBehaviour {

    public string opaqueScene = "Windows";
    public float transparancy = 0.75f; //set alpha value, 0 to 255
    public enum objectType { spriteRenderer, text, image };
    public objectType type;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Color tmp;
        if (type == objectType.spriteRenderer)
        {
            tmp = gameObject.GetComponent<SpriteRenderer>().color;
        }
        else if (type == objectType.image)
        {
            tmp = gameObject.GetComponent<UnityEngine.UI.Image>().color;
        }
        else if (type == objectType.text)
        {
            tmp = gameObject.GetComponent<UnityEngine.UI.Text>().color;
        }
        else
        {
            tmp = Color.red; //error
        }
        //-----------------------
        if (SceneManager.GetActiveScene().name == opaqueScene)
        {
            tmp.a = 255f;
        }
        else
        {
            tmp.a = transparancy;
        }
        //-----------------------
        if (type == objectType.spriteRenderer)
        {
            gameObject.GetComponent<SpriteRenderer>().color = tmp;
        }
        else if (type == objectType.image)
        {
            gameObject.GetComponent<UnityEngine.UI.Image>().color = tmp;
        }
        else if (type == objectType.text)
        {
            gameObject.GetComponent<UnityEngine.UI.Text>().color = tmp;
        }
        else
        {
            tmp = Color.red; //error
        }
    }
}
