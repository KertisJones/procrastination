using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTimer : MonoBehaviour {

	//Public variable for laser object and timers
	public GameObject laserObject;
	public float onTime;
	public float offTime;
	public float delay;
	public bool isOn;

	//Private variable for current time
	float curTime;
	
	// Update is called once per frame
	void Update () {
		//Waits for the delay
		if (delay <= 0) {
			//Changes state if timer is out
			if (curTime <= 0) {
				//Actions to take when turning off laser
				if (isOn) {
					isOn = false;
					curTime = offTime;
					laserObject.SetActive (false);
				} else {
					isOn = true;
					curTime = onTime;
					laserObject.SetActive (true);
				}
			} else {
				curTime -= Time.deltaTime;
			}
		} else {
			delay -= Time.deltaTime;
		}
	}
}
