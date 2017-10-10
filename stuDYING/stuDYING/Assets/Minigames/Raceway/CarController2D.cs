using UnityEngine;
using System.Collections;

public class CarController2D : MonoBehaviour {
    private Rigidbody2D rb;

    public float maxSpeed = 5;
    public float maxSpeedReverse = 1;
    public float acceleration = 2;

    private float speedCurrent = 0;

    public float steering = 3;
    public float steeringDrift = 3; //steering when holding down shift

    public float accelWaitTime = 1; //in seconds
    private float timeLeft;
    //public int totalDays = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speedCurrent = acceleration;
    }

    void FixedUpdate()
    {
        float h = -Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //-------------------------------------------------- Acceleration --------------------------------------------------

        Vector2 speed;

        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            //totalDays += 1;
            timeLeft = accelWaitTime;



            if (v > 0)
            {
                if (speedCurrent < maxSpeed)
                {
                    speedCurrent += acceleration;
                }
                else
                {
                    speedCurrent = maxSpeed;
                }
            }
            else if (v < 0)
            {
                if (speedCurrent < maxSpeedReverse)
                {
                    speedCurrent += (acceleration * (maxSpeedReverse / maxSpeed));
                }
                else
                {
                    speedCurrent = maxSpeedReverse;
                }
            }
            else
            {
                speedCurrent = acceleration;
                //if (speedCurrent > 0)
                //{
                //    speedCurrent -= acceleration;
                //}
                //else
                //{
                //    speedCurrent = 0;
                //}
            }
        }

        speed = transform.up * (v * speedCurrent);
        rb.AddForce(speed);

        //-------------------------------------------------- Turning --------------------------------------------------

        float direction = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.up));
        if (direction >= 0.0f)
        {
            if (Input.GetButton("Handbrake"))
            {
                rb.rotation += h * steeringDrift * (rb.velocity.magnitude / 5.0f);
            }
            else
            {
                rb.rotation += h * steering * (rb.velocity.magnitude / 5.0f);
            }
            //rb.AddTorque((h * steering) * (rb.velocity.magnitude / 10.0f));
        }
        else
        {
            if (Input.GetButton("Handbrake"))
            {
                rb.rotation -= h * steeringDrift * (rb.velocity.magnitude / 5.0f);
            }
            else
            {
                rb.rotation -= h * steering * (rb.velocity.magnitude / 5.0f);
            }
            //rb.AddTorque((-h * steering) * (rb.velocity.magnitude / 10.0f));
        }

        Vector2 forward = new Vector2(0.0f, 0.5f);
        float steeringRightAngle;
        if (rb.angularVelocity > 0)
        {
            steeringRightAngle = -90;
        }
        else
        {
            steeringRightAngle = 90;
        }

        Vector2 rightAngleFromForward = Quaternion.AngleAxis(steeringRightAngle, Vector3.forward) * forward;
        Debug.DrawLine((Vector3)rb.position, (Vector3)rb.GetRelativePoint(rightAngleFromForward), Color.green);

        float driftForce = Vector2.Dot(rb.velocity, rb.GetRelativeVector(rightAngleFromForward.normalized));

        Vector2 relativeForce = (rightAngleFromForward.normalized * -1.0f) * (driftForce * 10.0f);


        Debug.DrawLine((Vector3)rb.position, (Vector3)rb.GetRelativePoint(relativeForce), Color.red);

        rb.AddForce(rb.GetRelativeVector(relativeForce));

     
    }
 }
