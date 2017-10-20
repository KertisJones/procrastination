using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAudioTime : MonoBehaviour {

    public float time = 0;

	// Use this for initialization
	void Start () {
        this.GetComponent<AudioSource>().time = time;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
