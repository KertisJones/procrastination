using UnityEngine;

public class EX6_Projectile : MonoBehaviour
{
    // The speed of the projectile
    public float speed = 30;
    // The radius of the projectile (used to check for collisions)
    public float radius = 0.2f;
    // A mask of what layers can and can't be hit by the Projectile instance
    public LayerMask canHit;

    // The current direction the projectile is traveling in
    private Vector2 currentDirection;
    // Whether or not the projectile has hit something (finished moving)
    private bool done = false;

    // Update will be called every frame
    private void Update()
    {
        // Check if the projectile should be traveling
        if (!done)
        {
            // Move for one frame
            Move(currentDirection);
            
            // Finish moving if a collision is detected
            if (GetCollision() != null)
            {
                done = true;
            }
        }
    }

    // Fire will set the direction of the projectile so it can move
    public void Fire(Vector2 direction)
    {
        currentDirection = direction.normalized;
    }
    // Move will move the projectile along a given direction
    public void Move(Vector3 direction)
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
    // GetCollision will check if any Collider2D (with a layer in the mask stored in the variable canHit) is
    //   overlapping a circle around the projectile and return it (or null, i.e. nothing)
    public Collider2D GetCollision()
    {
        return Physics2D.OverlapCircle(transform.position, radius, canHit);
    }
}
