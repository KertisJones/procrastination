﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffRenderer : MonoBehaviour {

	public Renderer rend;

	void Start()
	{
		rend = GetComponent<Renderer>();
		rend.enabled = false;
	}
	// Update is called once per frame
	void Update () {
		
	}
}
