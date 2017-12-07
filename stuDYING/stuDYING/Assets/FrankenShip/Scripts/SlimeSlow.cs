using UnityEngine;
using System.Collections;

public class SlimeSlow : MonoBehaviour
{

    public float timeScale = 1.0f;
    float timeElapsed = 0f;
    bool slowed = false;
    private float curSpeed;


    // Use this for initialization
    void Start()
    {
        timeElapsed = 0f;
        curSpeed = gameObject.GetComponent<ShipBaseMovement>().thrustPower;
    }

    // Update is called once per frame
    void Update()
    {
        if (slowed)
        {
            curSpeed /= 2;
        }
        if (timeElapsed >= 3)
        {
            if (slowed)
            {
                slowed = false;
            }
        }
        else {
            //should count up time
            timeElapsed += Time.deltaTime;
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            Debug.Log("hit");
            timeElapsed = 0f;
            slowed = true;
        }

    }
}