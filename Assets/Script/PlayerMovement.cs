using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Camera mainCamera;
    public float moveSpeed = 5f; // Speed of player movement

    private void Start()
    {
        // Find the main camera in the scene
        mainCamera = Camera.main;

        // Hide and lock the cursor
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
        Cursor.visible = false; // Make the cursor invisible
    }

    private void Update()
    {
        // Rotate the player towards the camera every frame
        RotatePlayerTowardsCamera();

        // Handle player movement
        MovePlayer();
    }

    private void RotatePlayerTowardsCamera()
    {
        if (mainCamera != null)
        {
            Vector3 cameraForward = mainCamera.transform.forward;
            cameraForward.y = 0f; // Ignore the y-axis rotation

            if (cameraForward != Vector3.zero)
            {
                Quaternion newRotation = Quaternion.LookRotation(cameraForward);
                transform.rotation = newRotation;
            }
        }
    }

    private void MovePlayer()
    {
        // Get input from the player
        float horizontalInput = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow
        float verticalInput = Input.GetAxis("Vertical");     // W/S or Up/Down Arrow

        // Calculate movement direction relative to the player's current rotation
        Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

        // Apply movement
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}