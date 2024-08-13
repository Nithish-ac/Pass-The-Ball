using UnityEngine;
using Fusion;
using Fusion.Addons.Physics;

public class ThirdPersonController : NetworkBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public Transform cameraTransform;

    public float kickForce = 10f;
    private BallController ballController;
    private bool hasBall = false;

    private NetworkCharacterController characterController;
    private Animator animator;

    public override void Spawned()
    {
        characterController = GetComponent<NetworkCharacterController>();
        animator = GetComponent<Animator>();

        if (Object.HasInputAuthority)
        {
            cameraTransform.gameObject.SetActive(true);
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData inputData))
        {
            // Handle player movement based on input
            Vector3 move = transform.forward * inputData.moveDirection.z;
            characterController.Move(move * moveSpeed * Runner.DeltaTime);


            // Update the blend tree parameter
            animator.SetFloat("Speed", inputData.moveDirection.magnitude);

            // Rotate the player using horizontal input
            if (inputData.moveDirection.x != 0)
            {
                transform.Rotate(Vector3.up, inputData.moveDirection.x * rotationSpeed * Runner.DeltaTime);
            }

            // Handle Kick Animation
            if (inputData.isKicking)
            {
                KickBall();
            }
            else
            {
                animator.ResetTrigger("Kick");

            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            ballController = other.GetComponent<BallController>();
            if (ballController != null)
            {
                hasBall = true;
                ballController.SetHeld(true, transform); // Set the ball as held by this player
            }
        }
    }

    private void KickBall()
    {
        if (hasBall && ballController != null)
        {
            hasBall = false;
            ballController.SetHeld(false); // Release the ball
            ballController.ApplyKickForce(transform.forward * kickForce); // Apply the kick force
            animator.SetTrigger("Kick");
            ballController = null; // Clear the reference
        }
    }
}
