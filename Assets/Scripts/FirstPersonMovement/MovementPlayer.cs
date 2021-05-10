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
  float maxJumpHeight = 2f;
  float timeToMaxHeight = 0.5f;

  void Start()
  {
    controller = GetComponent<CharacterController>();

    gravity = -8f;
  }

  void Update()
  {
    if (DialogManager.Instance.isTalking) return;
    forwardInput = Input.GetAxisRaw("Vertical");
    strafeInput = Input.GetAxisRaw("Horizontal");

    ReceiveDirection();

    CheckCollision();

    Vector3 finalVelocity = forward + strafe + vertical;
    controller.Move(finalVelocity * Time.deltaTime);

    if (controller.isGrounded)
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

  void CheckCollision()
  {
    if (vertical.y > 0 && (controller.collisionFlags & CollisionFlags.Above) != 0)
    {
      vertical = Vector3.zero;
    }
  }
}