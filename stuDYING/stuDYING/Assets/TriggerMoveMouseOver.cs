using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMoveMouseOver : MonoBehaviour {

    public PauseMenuMove menuOpen;
    public PauseMenuMove menuClose;

    // Use this for initialization
    void Start () {
		
	}

    void Update()
    {
        /*if (menuOpen.triggerMovement == true)
        {
            menuClose.triggerMovement = false;
        }
        else if (menuClose.triggerMovement == true)
        {
            menuOpen.triggerMovement = false;
        }*/
    }

    private void OnMouseEnter()
    {
        menuOpen.triggerMovement = true;
        menuClose.triggerMovement = false;
        //menuClose.triggerMovement = false;
    }

    private void OnMouseOver()
    {
        //menuOpen.triggerMovement = true;
        //menuClose.triggerMovement = false;
    }

    private void OnMouseExit()
    {
        menuClose.triggerMovement = true;
        menuOpen.triggerMovement = false;
        //menuClose.triggerMovement = true;
    } 
}
