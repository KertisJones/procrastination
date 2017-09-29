using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class DeathCountManager : MonoBehaviour
{
	public static double deaths;        // The player's deaths.
	
	
	Text text;                      // Reference to the Text component.
	
	
	void Awake ()
	{
		// Set up the reference.
		text = GetComponent <Text> ();
		
		// Reset the deaths.
		deaths = 0;
	}
	
	
	void Update ()
	{
		// Set the displayed text to be the word "deaths" followed by the deaths value.
		deaths = Math.Ceiling (deaths);
		text.text = "Deaths: " + deaths;
	}
}