using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class TutorialPageFlip : MonoBehaviour {

	Text myText;
	public List <string> pageTexts;
	/// <summary>
	/// The page number the turoial is currently on,, if it has multiple;
	/// </summary>
	public int tutPageNum;

	// Use this for initialization
	void Start () {
		pageTexts = new List<string> ();
		myText = gameObject.GetComponent<Text> ();
		if (transform.tag == "CollectionTutorial" && ControlManager.P1Xbox && ControlManager.P2Xbox)
        {
			CollectionTextController ();
		}
        if (transform.tag == "CollectionTutorial" && ControlManager.P1Key && ControlManager.P2Key)
        {
            CollectionTextKeyboard();
        }
        if (transform.tag == "CollectionTutorial" && ControlManager.P1Xbox && ControlManager.P2Key)
        {
            CollectionTextMix();
        }

        if (transform.tag == "BuildTutorial" && ControlManager.P1Xbox && ControlManager.P2Xbox)
        {
            BuildTextController();
        }
        if (transform.tag == "BuildTutorial" && ControlManager.P1Key && ControlManager.P2Key)
        {
            BuildTextKeyboard();
        }
        if (transform.tag == "BuildTutorial" && ControlManager.P1Xbox && ControlManager.P2Key)
        {
            BuildTextMix();
        }

        if (transform.tag == "BattleTutorial" && ControlManager.P1Xbox && ControlManager.P2Xbox)
        {
            BattleTextController();
        }
        if (transform.tag == "BattleTutorial" && ControlManager.P1Key && ControlManager.P2Key)
        {
            BattleTextKeyboard();
        }
        if (transform.tag == "BattleTutorial" && ControlManager.P1Xbox && ControlManager.P2Key)
        {
            BattleTextMix();
        }
    }
	
	// Update is called once per frame
	void Update () {
		myText.text = pageTexts [tutPageNum];
	}

    /// <summary>
    /// Collection phase tutorial text, for two controllers plugged in
    /// </summary>
	public void CollectionTextController ()
    {
		pageTexts.Add ("In the Collection Phase, players control an Inter-Galactic-Orbital-Retriever (IGOR) unit " +
		"equipped with magnetic grav-hands...");
		pageTexts.Add ("the IGOR's objective is to collect as many loot crates as possible " +
		"and return them to its stasis fields before they are sucked into the black hole (and gone forever!).");
		pageTexts.Add ("Alternatively, the IGOR can sabotage the opponent by harrassing the other IGOR, " +
			"or by knocking out the crates in the other player's stasis fields...");
		pageTexts.Add ("It is important to remember that the crates collected unlock parts for use in the next phase, " +
			"so grab as many as you can-- and don't let your opponent get too many!");
		pageTexts.Add ("Remember to keep an eye on the timer, and good luck!");
        pageTexts.Add("(Use Left Stick to thrust, Right Stick to rotate, Left Trigger to grab, hold Right Trigger to charge, and release Right Trigger to throw)");
	}

    /// <summary>
    /// Collection phase tutorial text, for no controllers plugged in
    /// </summary>
    public void CollectionTextKeyboard()
    {
        pageTexts.Add("In the Collection Phase, players control an Inter-Galactic-Orbital-Retriever (IGOR) unit " +
        "equipped with magnetic grav-hands...");
        pageTexts.Add("the IGOR's objective is to collect as many loot crates as possible " +
        "and return them to its stasis fields before they are sucked into the black hole (and gone forever!).");
        pageTexts.Add("Alternatively, the IGOR can sabotage the opponent by harrassing the other IGOR, " +
            "or by knocking out the crates in the other player's stasis fields...");
        pageTexts.Add("It is important to remember that the crates collected unlock parts for use in the next phase, " +
            "so grab as many as you can-- and don't let your opponent get too many!");
        pageTexts.Add("Remember to keep an eye on the timer, and good luck!");
		pageTexts.Add("(Use Left Stick to thrust, Right Stick to rotate, Left Trigger to grab, hold Right Trigger to charge, and release Right Trigger to throw)");
    }

    /// <summary>
    /// Collection phase tutorial text, for 1 controller / 1 keyboard
    /// </summary>
    public void CollectionTextMix()
    {
        pageTexts.Add("In the Collection Phase, players control an Inter-Galactic-Orbital-Retriever (IGOR) unit " +
        "equipped with magnetic grav-hands...");
        pageTexts.Add("the IGOR's objective is to collect as many loot crates as possible " +
        "and return them to its stasis fields before they are sucked into the black hole (and gone forever!).");
        pageTexts.Add("Alternatively, the IGOR can sabotage the opponent by harrassing the other IGOR, " +
            "or by knocking out the crates in the other player's stasis fields...");
        pageTexts.Add("It is important to remember that the crates collected unlock parts for use in the next phase, " +
            "so grab as many as you can-- and don't let your opponent get too many!");
        pageTexts.Add("Remember to keep an eye on the timer, and good luck!");
		pageTexts.Add("(Use Left Stick to thrust, Right Stick to rotate, Left Trigger to grab, hold Right Trigger to charge, and release Right Trigger to throw)``");
    }

    /// <summary>
    /// Build phase tutorial text, 2 controllers plugged in
    /// </summary>
    public void BuildTextController (){
		
		pageTexts.Add ("Here, the crates acquired during the Collection Phase are unlocked " +
		"and the parts within are attached to ships--");
		pageTexts.Add ("but the important thing to note is that YOU ARE BUILDING YOUR OPPONENT'S SHIP!!!");
		pageTexts.Add ("Build the worst ship you possibly can.." +
			"but be prepared, because your opponent will be building your ship, too!");
		pageTexts.Add ("The ships constructed in this phase will be carried over into the Battle Phase for a final showdown.");
        pageTexts.Add("(Use LS to move the cursor, press A to unlock crates and grab/place pieces, " + 
            "bumpers to rotate, and A to lock in)");
        pageTexts.Add("(IMPORTANT NOTE: You will be unable to advance until all components have been placed.)");

	}

    /// <summary>
    /// Build phase tutorial text, no controllers plugged in
    /// </summary>
    public void BuildTextKeyboard()
    {

        pageTexts.Add("Here, the crates acquired during the Collection Phase are unlocked " +
        "and the parts within are attached to ships--");
        pageTexts.Add("but the important thing to note is that YOU ARE BUILDING YOUR OPPONENT'S SHIP!!!");
        pageTexts.Add("Build the worst ship you possibly can.." +
            "but be prepared, because your opponent will be building your ship, too!");
        pageTexts.Add("The ships constructed in this phase will be carried over into the Battle Phase for a final showdown.");
        pageTexts.Add("(Use LS to move the cursor, press A to unlock crates and grab/place pieces, " +
            "bumpers to rotate, and A to lock in)");
        pageTexts.Add("(IMPORTANT NOTE: You will be unable to advance until all components have been placed.)");

    }

    /// <summary>
    /// Build phase tutorial text, 1 controller / 1 keyboard
    /// </summary>
    public void BuildTextMix()
    {

        pageTexts.Add("Here, the crates acquired during the Collection Phase are unlocked " +
        "and the parts within are attached to ships--");
        pageTexts.Add("but the important thing to note is that YOU ARE BUILDING YOUR OPPONENT'S SHIP!!!");
        pageTexts.Add("Build the worst ship you possibly can.." +
            "but be prepared, because your opponent will be building your ship, too!");
        pageTexts.Add("The ships constructed in this phase will be carried over into the Battle Phase for a final showdown.");
        pageTexts.Add("(Use LS to move the cursor, press A to unlock crates and grab/place pieces, " +
            "bumpers to rotate, and A to lock in)");
        pageTexts.Add("(IMPORTANT NOTE: You will be unable to advance until all components have been placed.)");

    }

    /// <summary>
    /// Battle phase tutorial text, 2 controllers plugged in
    /// </summary>
    public void BattleTextController ()
    {

		pageTexts.Add ("You finally get to pilot the Frankenship youre oppponent has constructed for you. ");
		pageTexts.Add ("In this phase you have one goal: destroy the opponent's Core");
		pageTexts.Add ("You built the opponent's ship so you know its weaknesses --");
		pageTexts.Add ("The trick is figuring out your own, and adapting your playstyle accordingly.");
		pageTexts.Add ("Grab power-ups often and use your Core's power effectively and you just might survive!");
        pageTexts.Add ("(LS/RS to move, hold LT to aim Core power, use RS to aim Core power, press RT to shoot Core power, " +
            "press X to activate collected power-up)");
        pageTexts.Add ("IMPORTANT NOTE: With the exception of your Core's special ability, your weapons fire automatically!");
	}

    /// <summary>
    /// Battle phase tutorial text, no controllers plugged in
    /// </summary>
    public void BattleTextKeyboard()
    {

        pageTexts.Add("You finally get to pilot the Frankenship youre oppponent has constructed for you. ");
        pageTexts.Add("In this phase you have one goal: destroy the opponent's Core");
        pageTexts.Add("You built the opponent's ship so you know its weaknesses --");
        pageTexts.Add("The trick is figuring out your own, and adapting your playstyle accordingly.");
        pageTexts.Add("Grab power-ups often and use your Core's power effectively and you just might survive!");
        pageTexts.Add("(LS/RS to move, hold LT to aim Core power, use RS to aim Core power, press RT to shoot Core power, " +
            "press X to activate collected power-up)");
        pageTexts.Add("IMPORTANT NOTE: With the exception of your Core's special ability, your weapons fire automatically!");
    }

    /// <summary>
    /// Battle phase tutorial text, 1 controller / 1 Keyboard
    /// </summary>
    public void BattleTextMix()
    {

        pageTexts.Add("You finally get to pilot the Frankenship youre oppponent has constructed for you. ");
        pageTexts.Add("In this phase you have one goal: destroy the opponent's Core");
        pageTexts.Add("You built the opponent's ship so you know its weaknesses --");
        pageTexts.Add("The trick is figuring out your own, and adapting your playstyle accordingly.");
        pageTexts.Add("Grab power-ups often and use your Core's power effectively and you just might survive!");
        pageTexts.Add("(LS/RS to move, hold LT to aim Core power, use RS to aim Core power, press RT to shoot Core power, " +
            "press X to activate collected power-up)");
        pageTexts.Add("IMPORTANT NOTE: With the exception of your Core's special ability, your weapons fire automatically!");
    }
}
