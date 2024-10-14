using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ClothFlowController : MonoBehaviour
{
    public float smoothSpeed = 0.5f; // How smooth the motion should be
    public float dragStrength = 0.2f;  // Strength of the flowing motion when dragged
    public float maxDragDistance = 0.5f;  // Maximum distance for flow effect

    private XRGrabInteractable grabInteractable;
    private Vector3 lastPosition;
    private Rigidbody rb;

    private void Awake()
    {
        // Grab the XRGrabInteractable and Rigidbody components
        grabInteractable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();

        // Ensure Rigidbody is NOT kinematic and has gravity enabled
        rb.isKinematic = false;
        rb.useGravity = true;

        // Save the initial position of the dress
        lastPosition = transform.position;
    }

    private void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // When grabbed, we want the object to be fully affected by physics
        rb.isKinematic = false;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        // When released, the Rigidbody stays non-kinematic so physics continues to affect the object
        rb.isKinematic = false;
    }

    private void FixedUpdate()
    {
        if (!grabInteractable.isSelected)
            return;

        // Calculate the delta position between frames to simulate the drag flow
        Vector3 currentPosition = transform.position;
        Vector3 dragDelta = currentPosition - lastPosition;

        // Clamp the drag distance to avoid extreme stretching
        if (dragDelta.magnitude > maxDragDistance)
        {
            dragDelta = dragDelta.normalized * maxDragDistance;
        }

        // Apply a smooth flowing motion effect by adjusting the velocity
        Vector3 flowDirection = dragDelta * dragStrength / Time.fixedDeltaTime;
        rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, flowDirection, smoothSpeed * Time.fixedDeltaTime);

        // Update the last position for the next frame's calculations
        lastPosition = currentPosition;
    }
}
