
using UnityEngine;
using Fusion;

public class ThirdPersonController : NetworkBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 720f;
    public Transform cameraTransform;

    private CharacterController characterController;
    private Animator animator;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        if (Object.HasInputAuthority)
        {
            cameraTransform.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (!Object.HasInputAuthority) return;

        HandleMovement();
        HandleRotation();
        HandleKick();
    }

    private void HandleMovement()
    {
        // Vertical input for forward/backward movement
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = transform.forward * verticalInput;

        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        // Update Animator Speed parameter for the blend tree
        float speed = moveDirection.magnitude * moveSpeed;
        animator.SetFloat("Speed", speed);
    }

    private void HandleRotation()
    {
        // Horizontal input for rotation
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 rotation = new Vector3(0, horizontalInput * rotationSpeed * Time.deltaTime, 0);

        transform.Rotate(rotation);
    }

    private void HandleKick()
    {
        // Trigger Kick animation with a key press (e.g., Space key)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Kick");
        }
    }
}
