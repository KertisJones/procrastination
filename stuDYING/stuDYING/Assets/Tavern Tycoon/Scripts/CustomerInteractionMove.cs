using UnityEngine;
using System.Collections;

public class CustomerInteractionMove : MonoBehaviour {

    public float x;
    public float y;
    public Transform target;
    public float transition = 6f;
    public bool triggerMovement = false;
    //private bool keepMoving = false;
	// Use this for initialization
	void Start ()
    {

    }

    void FixedUpdate()
    {
        if (triggerMovement)
        {
            Vector3 newPos = transform.position - new Vector3(x, y, 0);
            target.position = Vector3.Lerp(target.position, newPos, transition * Time.deltaTime);

            //Debug.Log(Vector3.Distance(target.position, this.transform.position));
        }
        if (Vector3.Distance(target.position, this.transform.position) <= 0.1)
        {
            triggerMovement = false;
        }
        
    }

    // Update is called once per frame
    void Update () {
	
	}
}
