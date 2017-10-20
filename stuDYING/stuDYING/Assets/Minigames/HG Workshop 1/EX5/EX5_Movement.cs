using UnityEngine;

// This component requires a Rigidbody2D to work, so make sure there is one attached
[RequireComponent(typeof(Rigidbody2D))]
public class EX5_Movement : MonoBehaviour
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
            /* Implement a function to change the velocity of the Rigidbody2D stored in the variable body
             * Note:
             *   Rigidbodies will automatically move, etc. if given a velocity
             *   The object's speed (velocity magnitude) shouldn't go above maxSpeed
             *   The function should use Time.fixedDeltaTime for a time delta since it messes with physics objects
             *   
             * Useful Things:
             *   Access (to get or set) a Rigidbody2D's velocity -> <Some RigidBody2D>.velocity
             *     (which is a Vector2)
             *   Force a vector v to have a length of at most x  -> Vector2.ClampMagnitude( v, x )
             *     while maintaining direction
             */
        }
    }
}
