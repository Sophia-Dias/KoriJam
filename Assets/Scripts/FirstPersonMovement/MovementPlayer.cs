using UnityEngine;

public class MovementPlayer : MonoBehaviour 
{
    CharacterController controller;

    Vector3 forward;
    Vector3 strafe;
    Vector3 vertical;
	
	float forwardInput;
	float strafeInput;

    float forwardSpeed = 6f;
    float strafeSpeed = 6f;

    float gravity;
    float jumpSpeed;
    float maxJumpHeight = 2f;
    float timeToMaxHeight = 0.5f;

    void Start() 
	{
        controller = GetComponent<CharacterController>();

        gravity = (-2 * maxJumpHeight) / (timeToMaxHeight * timeToMaxHeight);
        jumpSpeed = (2 * maxJumpHeight) / timeToMaxHeight;

    }

    void Update() 
	{
        forwardInput = Input.GetAxisRaw("Vertical");
        strafeInput = Input.GetAxisRaw("Horizontal");

		ReceiveDirection();
		Jump();

		CheckCollision();
		
        Vector3 finalVelocity = forward + strafe + vertical;
        controller.Move(finalVelocity * Time.deltaTime);

		if(controller.isGrounded)
		{
            vertical = Vector3.down;
        }
    }
	
	void ReceiveDirection()
	{
		// force = input * speed * direction
        forward = forwardInput * forwardSpeed * transform.forward;
        strafe = strafeInput * strafeSpeed * transform.right;
        vertical += gravity * Time.deltaTime * Vector3.up;
	}
	
	void Jump()
	{
		Debug.Log("Pulou");
		
        if(Input.GetKeyDown(KeyCode.Space) && controller.isGrounded) 
		{
            vertical = jumpSpeed * Vector3.up;
        }
	}
	
	void CheckCollision()
	{
		if (vertical.y > 0 && (controller.collisionFlags & CollisionFlags.Above) != 0)
		{
            vertical = Vector3.zero;
        }
	}
}