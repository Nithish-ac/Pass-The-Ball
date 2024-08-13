using UnityEngine;
using Fusion;
using Fusion.Addons.Physics;

public class ThirdPersonController : NetworkBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public Transform cameraTransform;

    public float kickForce = 1f;
    private GameObject ball;
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
            hasBall = true;
            ball = other.gameObject;
            // Remove all forces from the ball
            NetworkRigidbody3D ballRb = ball.GetComponent<NetworkRigidbody3D>();
            ballRb.Rigidbody.velocity = Vector3.zero;
            ballRb.Rigidbody.angularVelocity = Vector3.zero;
            ballRb.Rigidbody.isKinematic = true;
            // Attach the ball to the player
            ball.transform.SetParent(transform);
            ball.transform.localPosition = new Vector3(0, 0.2f, 1); // Position the ball in front of the player
        }
    }
    private void KickBall()
    {
        hasBall = false;
        ball.transform.SetParent(null);
        NetworkRigidbody3D ballRb = ball.GetComponent<NetworkRigidbody3D>();
        ballRb.Rigidbody.isKinematic=false;
        ballRb.Rigidbody.AddForce(transform.forward * kickForce, ForceMode.Impulse);
        animator.SetTrigger("Kick");
    }
}
