using UnityEngine;

// This component requires a Rigidbody2D to work, so make sure there is one attached
[RequireComponent(typeof(Rigidbody2D))]
public class EX6_Movement : MonoBehaviour
{
    // The objects max speed, useful so the velocity doesn't continuously grow without bound
    public float maxSpeed = 11;

    // The acceleration to apply <(m/s)/s>
    //   ( if you start at v = 0m/s and accelerate at 5m/s^2 for 2 seconds you will end at v = 10m/s )
    public float acceleration = 8;

    // The Rigidbody2D to use when setting velocity
    //   ( a Rigidbody is simply a generic physics body that can move around and be acted on by other Ridigbodies )
    private Rigidbody2D body;

    // Awake is called as soon as the object is loaded, before anything else happens
    private void Awake()
    {
        // Get the attached Rigidbody2D component and store that in the variable body
        body = GetComponent<Rigidbody2D>();
    }

    // SetVelocity will be used to accelerate the object's Rigidbody in the direction of a given input
    public void SetVelocity(Vector2 input)
    {
        // Only change the velocity if an input is given (don't do anything if input = [0,0])
        if(input != Vector2.zero)
        {
            // Add the change in velocity for this frame to the body's velocity and clamp the magnitude to maxSpeed
            body.velocity = Vector2.ClampMagnitude(body.velocity + input * Time.fixedDeltaTime * acceleration, maxSpeed);
        }
    }
}
