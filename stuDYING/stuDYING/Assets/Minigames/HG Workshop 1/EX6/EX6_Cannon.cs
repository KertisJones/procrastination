using UnityEngine;

public class EX6_Cannon : MonoBehaviour
{
    // The object to spawn as a projectile
    public GameObject projectile;
    // The location to spawn new projectiles
    public Transform shotSpawn;
    // How fast the cannon should rotate when aiming
    public float rotationSpeed = 6;

    // Fire will create a new projectile
    public void Fire()
    {
        // Instantiate a new copy of the object stored in the variable projectile
        GameObject newShot = Instantiate(projectile);
        // Set the object's position to that of shotSpawn
        newShot.transform.position = shotSpawn.position;
        // Attempt to call the function Fire on some component of the new object
        //   transform.right will give the local right vector, so if an object is rotated 90 degrees you will get [0,1] instead of [1,0] (Vector2.right)
        newShot.SendMessage("Fire", (Vector2)transform.right);
    }

    // AimAt will move the object to point the object at the given position each frame it is called
    public void AimAt(Vector2 position)
    {
        // Slerp -> Spherical Linear Interpolation, used for interpolating between rotation values (Quaternions)
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, Vector2.Angle(Vector2.right, position - (Vector2)transform.position) * (position.y >= transform.position.y ? 1 : -1)), Time.deltaTime * rotationSpeed);
    }
}
