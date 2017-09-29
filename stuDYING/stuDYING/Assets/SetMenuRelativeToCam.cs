using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMenuRelativeToCam : MonoBehaviour {

    public GameObject openTarget;
    public GameObject closeTarget;

   // public bool parentIsCamera = false;
    public Camera cam;


    public float originalCamSize = 5.4f;
    private float relativeSize = 0;
    private float lastSize = 0;
    public bool camChange = false;

    private float originalOpenX = 0;
    private float originalOpenY = 0;
    private float originalCloseX = 0;
    private float originalCloseY = 0;

    // Use this for initialization
    void Start () {
        originalOpenX = openTarget.transform.position.x;
        originalOpenY = openTarget.transform.position.y;
        originalCloseX = closeTarget.transform.position.x;
        originalCloseY = closeTarget.transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        relativeSize = cam.orthographicSize / originalCamSize;

        if (relativeSize != lastSize)
        {
            camChange = true;
            lastSize = relativeSize;
        }
        else
        {
            camChange = false;            
        }

        if (camChange)
        {
            closeTarget.GetComponent<PauseMenuMove>().triggerMovement = true;
        }
        openTarget.transform.position = new Vector3(originalOpenX * relativeSize + cam.transform.position.x, originalOpenY * relativeSize + cam.transform.position.y, 0);
        closeTarget.transform.position = new Vector3(originalCloseX * relativeSize + cam.transform.position.x, originalCloseY * relativeSize + cam.transform.position.y, 0);

        this.transform.localScale = new Vector3(relativeSize, relativeSize, relativeSize);



	}
}
