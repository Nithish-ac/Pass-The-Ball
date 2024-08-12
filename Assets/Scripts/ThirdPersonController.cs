
using UnityEngine;
using Fusion;

public class ThirdPersonController : NetworkBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public Transform cameraTransform;

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
        }
    }

    //private void HandleMovement()
    //{
    //    // Vertical input for forward/backward movement
    //    float verticalInput = Input.GetAxis("Vertical");
    //    Vector3 moveDirection = transform.forward * verticalInput;
    //    Debug.Log("verticalInput"+ verticalInput);
    //    characterController.Move(moveDirection * moveSpeed * Runner.DeltaTime);

    //    // Update Animator Speed parameter for the blend tree
    //    float speed = moveDirection.magnitude * moveSpeed;
    //    animator.SetFloat("Speed", speed);
    //}

    //private void HandleRotation()
    //{
    //    // Horizontal input for rotation
    //    float horizontalInput = Input.GetAxis("Horizontal");
    //    Debug.Log("Horizontal Input" + horizontalInput);
    //    Vector3 rotation = new Vector3(0, horizontalInput * rotationSpeed * Runner.DeltaTime, 0);

    //    transform.Rotate(rotation);
    //}

    //private void HandleKick()
    //{
    //    // Trigger Kick animation with a key press (e.g., Space key)
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        animator.SetTrigger("Kick");
    //    }
    //}
}
