using UnityEngine;
using System.Collections;

public class ClickToMoveCamera : MonoBehaviour {

    public float x;
    public float y;

    public Camera cam;

    
    void OnMouseDown()
    {
        cam.transform.position = new Vector3(x, y, -10);
    }
}
