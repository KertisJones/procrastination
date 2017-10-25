using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	//Public variables for movespeed, and jump height
	public float maxSpeed;
	public float acceleration;
	public float jumpVel;
	public float distanceToEdge;
	public float distanceToBottom;
	public float groundCheckDis;
	public float defaultScale;
	public LayerMask groundLayers;
	public bool isHiding = false;

	//Internal variables to check things such as if player can jump
	bool canJump = true;
	bool onGround;
	Rigidbody2D rb2d;

	void Start() {
		rb2d = this.GetComponent<Rigidbody2D> ();
		this.defaultScale = this.gameObject.transform.localScale.x;
	}

	// Update is called once per frame
	void Update () {

		//Gets input
		if (Input.GetAxisRaw ("Horizontal") != 0) {
			//Temp variable to store velocity Vector3
			Vector3 newVel = new Vector3 (rb2d.velocity.x, rb2d.velocity.y);

			//Adds acceleration to player based on input
			newVel.x = rb2d.velocity.x + (acceleration * Time.deltaTime * Input.GetAxisRaw ("Horizontal"));

			//Locks player velocity if it gets too high or low
			if (newVel.x > maxSpeed) {
				newVel.x = maxSpeed;
			} else if (newVel.x < (maxSpeed * -1)) {
				newVel.x = maxSpeed * -1;
			}

			//Sets velocity to new velocity
			rb2d.velocity = newVel;

		//If there is no input, deccelerates player
		} else {
			//Temp Vector3
			Vector3 newVel = new Vector3 (rb2d.velocity.x, rb2d.velocity.y);
			//Checks direction of velocity and adds or subtracts velocity
			if (rb2d.velocity.x > .5) {
				newVel.x = rb2d.velocity.x - (acceleration * Time.deltaTime);
			} else if (rb2d.velocity.x < -.5) {
				newVel.x = rb2d.velocity.x + (acceleration * Time.deltaTime);
			} else {
				newVel.x = 0;
			}
			//Sets the new velocity
			rb2d.velocity = newVel;
		}

		//Sets onGround to result of GroundCheck
		onGround = GroundCheck();

		//Checks the ground to possibly reset jump variable
		if (onGround) {
			canJump = true;
		}

		//If the jump key is pressed, checks if the player can jump and runs jump function
		if (Input.GetAxis ("Jump") != 0) {
			if (canJump) {
				Jump ();
			}
		}

		//Makes player face current velocity direction
		FaceVelocity();
	}

	//Adds vertical velocity to the player and makes them unable to jump again until landing
	void Jump() {
		Vector3 newVel = new Vector3 (rb2d.velocity.x, jumpVel);
		rb2d.velocity = newVel;
		canJump = false;
	}

	//Runs three raycasts down to check for a floor under the player
	//If there is a floor, it makes the player able to jump
	bool GroundCheck() {
		//Creates Vector2s for origin
		Vector2 leftOrigin = new Vector2(this.transform.position.x - distanceToEdge, this.transform.position.y - distanceToBottom);
		Vector2 midOrigin = new Vector2(this.transform.position.x, this.transform.position.y);
		Vector2 rightOrigin = new Vector2(this.transform.position.x + distanceToEdge, this.transform.position.y - distanceToBottom);
		//Raycasts towards the ground
		RaycastHit2D left = Physics2D.Raycast (leftOrigin, Vector2.down, groundCheckDis, groundLayers);
		RaycastHit2D center = Physics2D.Raycast (midOrigin, Vector2.down, groundCheckDis, groundLayers);
		RaycastHit2D right = Physics2D.Raycast (rightOrigin, Vector2.down, groundCheckDis, groundLayers);

		//Returns true if any ray hit the ground, else false
		return (left.collider != null || center.collider != null || right.collider != null);
	}

	//Takes the players x velocity and makes them face right if it is positive, or left if it is negative
	//Assumes sprite faces right by default
	void FaceVelocity() {
		//Temporary scale variable
		Vector3 newScale = this.gameObject.transform.localScale;

		if (this.rb2d.velocity.x > 0) {
			newScale.x = defaultScale;
		} else if (this.rb2d.velocity.x < 0) {
			newScale.x = defaultScale * -1;
		}
		//Sets scale to new scale
		this.transform.localScale = newScale;
	}
}
