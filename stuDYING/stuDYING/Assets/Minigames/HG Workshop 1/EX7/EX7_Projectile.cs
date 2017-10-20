using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX7_Projectile : MonoBehaviour
{
    // The speed of the projectile
    public float speed = 30;
    // The radius of the projectile (used to check for collisions)
    public float radius = 0.2f;
    // A mask of what layers can and can't be hit by the Projectile instance
    public LayerMask canHit;

    // The amount of damage the projectile will do uppon hitting something
    public int damage = 1;

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
            Collider2D hit = GetCollision();
            if (hit != null)
            {
                done = true;
                // Call the function Hit with the Collider2D that was hit
                Hit(hit);
            }
        }
    }

    // Fire will set the direction of the projectile so it can move
    public void Fire(Vector2 direction)
    {
        currentDirection = direction.normalized;
    }
    // Move will move the projectile along a given direction
    private void Move(Vector3 direction)
    {
        // Move the projectile along the given direction by the distance it would travel in one frame
        transform.position += direction * speed * Time.deltaTime;
    }
    // GetCollision will check if any Collider2D (with a layer in the mask stored in the variable canHit) is
    //   overlapping a circle around the projectile and return it (or null, i.e. nothing)
    private Collider2D GetCollision()
    {
        return Physics2D.OverlapCircle(transform.position, radius, canHit);
    }
    // Handle what happens when the projectile hits an object
    private void Hit(Collider2D other)
    {
        /* Implement a function to damage the object the projectile hits
         * Note:
         *   The function takes a Collider2D as input, so information somehow has to get to a Health component
         *   A hit object may or may not have a Health component
         *   
         * Useful Things:
         *   As in EX6_Cannon, SendMessage can be used to attempt a function call on all components on a GameObject or all siblings of a component
         *   Look up SendMessage on the Unity Documentation to see how to ensure there are no issues if the object hit has no Health component
         *   SendMessage isn't the only way to accomplish this task, try to find alternate solutions that work just as well!
         *   
         * Bonus:
         *   Upon hitting an object the projectile will just stop and sit there, it would look much better if the sprite disappeared
         *   There are many ways to accomplish this, some will involve getting the relevant sprite and hiding it, since that's the only thing left once the projectile stops
         */
    }
}
