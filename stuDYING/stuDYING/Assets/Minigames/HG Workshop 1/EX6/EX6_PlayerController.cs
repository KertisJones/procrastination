using UnityEngine;

// The PlayerController class is an extension of the Movement class, thus it can do everything Movement can and more!
public class EX6_PlayerController : EX6_Movement
{
    // FixedUpdate() will be called during every physics step (potentially more than once per frame)
    private void FixedUpdate()
    {
        // Set call the SetVelocity function (inherited from Movement) with the current input direction
        SetVelocity(GetInputDirection());
    }

    // GetInputDirection will return the a unit vector in the direction of the current input
    //   ex. { W }     -> [0,1]
    //       { S + D } -> [1,-1]
    private Vector2 GetInputDirection()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }
}
