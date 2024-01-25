using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OmnicatLabs.Audio;
public class HeadbobController : MonoBehaviour
{
    public bool enable = true;

    [Tooltip("Controls the 'size' of the bob")]
    public float amplitude = 0.015f;
    [Tooltip("Controls the speed of the bob")]
    public float frequency = 10.0f;
    public float sprintModifier = 1.7f;
    [Tooltip("The speed at which the camera focus snaps back to center screen when not moving")]
    public float snapSpeed = 10f;
    public Transform cam;
    public Transform cameraHolder;

    private float toggleSpeed = 3f;
    private Vector3 startPos;
    private OmnicatLabs.CharacterControllers.CharacterController controller;

    private void Start()
    {
        controller = OmnicatLabs.CharacterControllers.CharacterController.Instance;
        startPos = cam.localPosition;
    }

    private void FixedUpdate()
    {
        if (!enable) return;
        if (!controller.isGrounded || controller.isCrouching || controller.slideKeyDown) return;

        CheckMotion();
        ResetPosition();
        cam.LookAt(FocusTarget());
    }

    private Vector3 FootstepMotion()
    {
        Vector3 pos = Vector3.zero;
        if (controller.sprinting)
        {
            pos.y += Mathf.Sin(Time.time * frequency * sprintModifier) * amplitude * sprintModifier;
            pos.x += Mathf.Cos(Time.time * frequency * sprintModifier / 2) * amplitude * sprintModifier * 2;
        }
        else
        {
            pos.y += Mathf.Sin(Time.time * frequency) * amplitude;
            pos.x += Mathf.Cos(Time.time * frequency / 2) * amplitude * 2;
        }
        
        return pos;
    }

    private void CheckMotion()
    {
        float speed = new Vector3(controller.rb.velocity.x, 0f, controller.rb.velocity.z).magnitude;
        ResetPosition();
        if (speed < toggleSpeed) return;
        if (!controller.isGrounded) return;

        PlayMotion(FootstepMotion());
    }

    private void ResetPosition()
    {
        if (cam.localPosition == startPos) return;

        cam.localPosition = Vector3.Lerp(cam.localPosition, startPos, snapSpeed * Time.deltaTime);
    }

    private Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + cameraHolder.localPosition.y, transform.position.z);
        pos += cameraHolder.forward * 15f;
        return pos;
    }

    private void PlayMotion(Vector3 motion)
    {
        cam.localPosition += motion;
    }
}
