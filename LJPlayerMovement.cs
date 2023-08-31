using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LJPlayerMovement : MonoBehaviour
{
    public SteamVR_Action_Vector2 movementAction = null;
    public float gravity = 9.81f;
    public Transform playerHeadCamera;//assign this
    public float speed = 1.0f;
    private CharacterController characterController;
    Vector3 moveDirection;

    private void Awake()
    {
        playerHeadCamera = GameObject.Find("VRCamera").transform;
        characterController = GetComponent<CharacterController>();


    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = movementAction.GetAxis(SteamVR_Input_Sources.Any);

        Vector3 forward = playerHeadCamera.forward;
        Vector3 right = playerHeadCamera.right;

        if (characterController.isGrounded)
        {
            moveDirection = (forward * movement.y + right * movement.x).normalized;
            moveDirection.y = 0f;
        }

        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * speed * Time.deltaTime);
    }
}
