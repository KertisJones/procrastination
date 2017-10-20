using UnityEngine;

public class EX4_MoveRight : MonoBehaviour
{
    // The speed to apply <m/s>
    //   ( if you start at x = 0m and move at 4ms for 2 seconds you will end at x = 8m )
    public float speed = 4;

    // Update will be called every frame
    private void Update()
    {
        Move(Vector3.right);
    }

    // Move will move the object in a given particular direction over 1 frame
    private void Move(Vector3 direction)
    {
        /* Implement a function to move the object along the given direction, using the value stored in the variable speed
         * Note:
         *   This function will be called every frame, so the object has to move only as far as it would in a single frame
         * 
         * Useful Things:
         *   Get the location of an object          -> transform.position
         *   Scale a vector v by a certain amount s -> v * s
         *   Get the time since the last frame      -> Time.deltaTime
         */
    }
}
