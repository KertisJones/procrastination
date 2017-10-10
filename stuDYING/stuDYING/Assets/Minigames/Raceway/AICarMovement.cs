using UnityEngine;
using System.Collections;

public class AICarMovement : MonoBehaviour {

	public float maxSpeed = 0.3f;
	//public float braking = 0.3f;
	public float steering = 4.0f;

    public float targetRanRange = .5f;
    public float maxSpeedRanRange = .05f;
    private float maxSpeedMinLength = 10; //in seconds
    private float maxSpeedMaxLength = 15; //in seconds


    public float acceleration = .05f;
    private float accelerationCurrent = 0;
    private float accelerationTimeLeft = 1;
    public float accelerationWaitTime = 1; //in seconds

    public float collisionPenalty = .10f; // decimal; multiply with current acceleration

    private float maxSpeedCurrent = 0.3f;
    private float maxSpeedTimeLeft = 10;

    Vector3 target;

    // Use this for initialization
    void Start()
    {
        maxSpeedCurrent = maxSpeed + Random.Range(-maxSpeedRanRange, maxSpeedRanRange);
        maxSpeedTimeLeft = Random.Range(maxSpeedMinLength, maxSpeedMaxLength);
        accelerationCurrent = acceleration;
        accelerationTimeLeft = accelerationWaitTime;
    }

    public void OnNextTrigger(TrackLapTrigger next) {

		// choose a target to drive towards
		target = Vector3.Lerp(next.transform.position - next.transform.right + new Vector3 (Random.Range(-targetRanRange, targetRanRange), Random.Range(-targetRanRange, targetRanRange), 0), 
		                      next.transform.position + next.transform.right + new Vector3 (Random.Range(-targetRanRange, targetRanRange), Random.Range(-targetRanRange, targetRanRange), 0), 
		                      Random.value);

    }

	void SteerTowardsTarget ()
	{
		Vector2 towardNextTrigger = target - transform.position;
		float targetRot = Vector2.Angle (Vector2.right, towardNextTrigger);
		if (towardNextTrigger.y < 0.0f) {
			targetRot = -targetRot;
		}
		float rot = Mathf.MoveTowardsAngle (transform.localEulerAngles.z, targetRot, steering);
		transform.eulerAngles = new Vector3 (0.0f, 0.0f, rot);
	}

    private void Update()
    {
        maxSpeedTimeLeft -= Time.deltaTime;
        if (maxSpeedTimeLeft < 0)
        {
            maxSpeedTimeLeft = Random.Range(maxSpeedMinLength, maxSpeedMaxLength);
            maxSpeedCurrent = maxSpeed + Random.Range(-maxSpeedRanRange, maxSpeedRanRange);
        }

        accelerationTimeLeft -= Time.deltaTime;
        if (accelerationTimeLeft < 0)
        {
            accelerationTimeLeft = accelerationWaitTime;
            if (accelerationCurrent < maxSpeedCurrent)
            {
                accelerationCurrent += acceleration;
                //Debug.Log(accelerationCurrent);
            }
            else
            {
                accelerationCurrent = maxSpeedCurrent;
                //Debug.Log(accelerationCurrent);
            }
        }

    }

    // update for physics
	void FixedUpdate() {

		SteerTowardsTarget();

		// always accelerate
		float velocity = GetComponent<Rigidbody2D>().velocity.magnitude;
		velocity += accelerationCurrent;
        //Debug.Log(accelerationCurrent);
        // apply car movement
        GetComponent<Rigidbody2D>().velocity = transform.right * velocity;
        GetComponent<Rigidbody2D>().angularVelocity = 0.0f;
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "CarAI" && other.gameObject.tag != "Player")
        {
            accelerationCurrent = accelerationCurrent * collisionPenalty;
        }
        else if (other.gameObject.tag == "Player") {} //do nothing 
        else
        {
            maxSpeedCurrent = maxSpeed + Random.Range(-maxSpeedRanRange, maxSpeedRanRange);
        }
    }

}
