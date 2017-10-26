using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MadLibsHG : MonoBehaviour {

    public Text[] textInputs = new Text[22];
    public string[] stringInputs = new string[22];

	// Use this for initialization
	void Start () {
		
	}

    static string UppercaseFirst(string s)
    {
        // Check for empty string.
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        // Return char and concat substring.
        return char.ToUpper(s[0]) + s.Substring(1);
    }

    // Update is called once per frame
    void Update () {
        int i = 0;
        while (i < textInputs.Length && i < stringInputs.Length)
        {
            if (textInputs[i] != null)
            {
                stringInputs[i] = textInputs[i].text.ToString();
            }
            else
            {
                stringInputs[i] = "Bologna";
            }

            if (stringInputs[i] == "" || stringInputs[i] == null)
            {
                stringInputs[i] = "Bologna";
            }
            i += 1;

            stringInputs[0] = UppercaseFirst(stringInputs[0]);
        }

        this.GetComponent<Text>().text = "Hoosier " + stringInputs[0] +
            " aims to bring together " + stringInputs[1] + 
            "-area " + stringInputs[2] +
            " of various skill-sets to produce " + stringInputs[3] +
            " games in a " + stringInputs[4] +
            ", mock-industry environment. Using workflows like " + stringInputs[5] +
            " and " + stringInputs[6] +
            ", along with " + stringInputs[7] +
            " best industry practice in all its " + stringInputs[8] +
            ", Hoosier " + stringInputs[0] +
            " gives its members a chance to gain " + stringInputs[9] +
            " from game design and grow their " + stringInputs[10] +
            " with " + stringInputs[3] + 
            " projects.\n\n" +
            "Each " + stringInputs[11] +
            ", members with a " + stringInputs[12] +
            " idea they'd like to try out can '" + stringInputs[13] +
            "' their idea to everyone at the beginning of the " + stringInputs[11] +
            ". Then, other members can join the team of whatever " + stringInputs[13] +
            "ed project they thought was " + stringInputs[14] +
            ". Teams will work throughout the " + stringInputs[11] +
            " to try and hit a milestone in the development of their game.\n\n" +
            "NO " + stringInputs[15].ToUpper() +
            " IS REQUIRED! Hoosier " + stringInputs[0] +
            " provides " + stringInputs[16] +
            " at the beginning of each " + stringInputs[11] +
            " to help everyone get a head start. Our " + stringInputs[17] +
            " members are also available throughout the " + stringInputs[11] +
            " to help " + stringInputs[18] +
            " any " + stringInputs[19] +
            ". You'll also be " + stringInputs[20] +
            " at how much you'll " + stringInputs[21] +
            " from your team!";
	}
}
