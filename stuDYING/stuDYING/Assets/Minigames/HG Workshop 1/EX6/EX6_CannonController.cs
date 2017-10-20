using UnityEngine;

public class EX6_CannonController : MonoBehaviour
{
    // A reference to the cannon to control
    public EX6_Cannon cannon;
    // The number max number of shots that can be fired in a single burst
    public int maxShots = 4;
    // The amount of time after firing before the number of available shots is reset
    public float reloadCooldown = 1;

    // The number of shots left
    private int currentShots = 0;
    // The current time left until the number of available shots is reset
    private float cooldownTime = 0;

    private void Update()
    {
        // Get the world position of the mouse (from property WorldPosition in class MouseCursos)
        Vector2 mousePosition = MouseCursor.WorldPosition;

        // Aim the referenced cannon at the mouse
        cannon.AimAt(mousePosition);

        // If the Fire1 button (L Click or L Control) is pressed on this frame fire if there are shots left
        if (Input.GetButtonDown("Fire1")) TryFire();

        // Reset the time left if necessary
        ResetCooldown();
    }

    // Decrement the amount of time left until the number of available shots is reset by the time since the last frame
    private void ResetCooldown()
    {
        // Only lower the value if it is greater than 0
        if (cooldownTime > 0)
        {
            cooldownTime -= Time.deltaTime;
        }
        else if (currentShots < maxShots)
        {
            // Reset the number of available shots if it isn't hasn't already been
            currentShots = maxShots;
        }
    }

    // Try firing the cannon (does nothing if there are no shots left)
    private void TryFire()
    {
        // If there are shots left
        if (currentShots > 0)
        {
            // Call the Fire function of the referenced cannon
            cannon.Fire();
            // Decrement the current number of available shots
            currentShots--;
            // Reset the time until the number of available shots is reset
            cooldownTime = reloadCooldown;
        }
    }
}
