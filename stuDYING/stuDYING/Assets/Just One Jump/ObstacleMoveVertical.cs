using UnityEngine;
using System.Collections;

public class ObstacleMoveVertical : MonoBehaviour {
    public float min = 2f;
    public float max = 3f;

    public float speed = 2f;
    public float distance = 3f;

    // Use this for initialization
    void Start()
    {

        min = transform.position.y;
        max = transform.position.y + distance;

    }

    // Update is called once per frame
    void Update()
    {


        transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * speed, max - min) + min, transform.position.z);

    }
}
