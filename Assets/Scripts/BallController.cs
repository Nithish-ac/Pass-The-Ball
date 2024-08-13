using Fusion.Addons.Physics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using static UnityEngine.GraphicsBuffer;

public class BallController : NetworkBehaviour
{
    private NetworkRigidbody3D ballRb;
    private bool isHeld = false;
    private Transform holderTransform;

    public override void Spawned()
    {
        ballRb = GetComponent<NetworkRigidbody3D>();
    }

    public override void FixedUpdateNetwork()
    {
        if (isHeld && holderTransform != null)
        {
            // Keep the ball in front of the holder
            Vector3 targetPosition = holderTransform.position + holderTransform.forward * 1f; // Adjust the offset as needed
            targetPosition.y = -0.3f; // Maintain the height
            transform.position = targetPosition;
            transform.rotation = holderTransform.rotation;
        }
        else
        {
            // Ensure ball stays at target height if not held
            Vector3 position = transform.position;
            position.y = -0.3f;
            transform.position = position;
        }
    }

    public void SetHeld(bool held, Transform holder = null)
    {
        isHeld = held;
        holderTransform = holder;

        if (isHeld)
        {
            // When the ball is held, disable its physics
            ballRb.Rigidbody.isKinematic = true;
            ballRb.Rigidbody.velocity = Vector3.zero;
            ballRb.Rigidbody.angularVelocity = Vector3.zero;
        }
        else
        {
            // When the ball is released, enable its physics
            ballRb.Rigidbody.isKinematic = false;
        }
    }

    public void ApplyKickForce(Vector3 force)
    {
        if (!isHeld)
        {
            ballRb.Rigidbody.AddForce(force, ForceMode.Impulse);
        }
    }
}
