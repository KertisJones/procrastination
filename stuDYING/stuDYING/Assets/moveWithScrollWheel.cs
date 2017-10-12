using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveWithScrollWheel : MonoBehaviour {

    public Transform target;
    public float speed;

    public float yMax = 100;
    public float yMin = -100;

    void Update()
    {

        if (Input.GetAxis("Mouse ScrollWheel") < 0 && this.transform.position.y > yMin)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            //transform.LookAt(target);
            transform.Translate(0, scroll * speed, 0);


        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0 && this.transform.position.y < yMax)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            //transform.LookAt(target);
            transform.Translate(0, scroll * speed, 0);
        }

    }
}
