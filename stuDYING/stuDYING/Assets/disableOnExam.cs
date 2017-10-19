using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class disableOnExam : MonoBehaviour {

    public string sceneThatDisables = "Exam";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (sceneThatDisables == SceneManager.GetActiveScene().name)
        {
            gameObject.SetActive(false);
        }
    }
}
