using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour {

    //Public variable for scene transport
	public float x;
    public float y;
    public float z;

    public float camx;
    public float camy;
    public float camz = -10;

    void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
            other.transform.position = new Vector3(x, y, z);
            GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(camx, camy, camz);
		}
	}
}
