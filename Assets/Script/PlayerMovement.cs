using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Camera mainCamera;
    public float moveSpeed = 5f;
    private bool hasPowerSpeed = false;
    public bool hasPowerAttack = false;
    public bool hasPowerShield = false;

    private void Start()
    {
        mainCamera = Camera.main;

        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false; 
    }

    private void Update()
    {
        RotatePlayerTowardsCamera();
        MovePlayer();
    }

    private void RotatePlayerTowardsCamera()
    {
        if (mainCamera != null)
        {
            Vector3 cameraForward = mainCamera.transform.forward;
            cameraForward.y = 0f; 

            if (cameraForward != Vector3.zero)
            {
                Quaternion newRotation = Quaternion.LookRotation(cameraForward);
                transform.rotation = newRotation;
            }
        }
    }

    private void MovePlayer()
    {
        if(hasPowerSpeed)
        {
            moveSpeed = 10f;
        }
        else
        {
            moveSpeed = 5f;
        }
        float horizontalInput = Input.GetAxis("Horizontal"); 
        float verticalInput = Input.GetAxis("Vertical");     
        Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if(hasPowerShield)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                Debug.Log("Game Over");
            }
        }
        if (collision.gameObject.CompareTag("PowerShield"))
        {
            Destroy(collision.gameObject);
            hasPowerShield = true;
            StartCoroutine(ShieldCoroutine());
        }
        if (collision.gameObject.CompareTag("PowerSpeed"))
        {
            Destroy(collision.gameObject);
            hasPowerSpeed = true;   
            StartCoroutine(PowerSpeedCoroutine());
        }

        if (collision.gameObject.CompareTag("PowerAttack"))
        {
            Destroy(collision.gameObject);
            hasPowerAttack = true;
            StartCoroutine(PowerAttackCoroutine());
        }
    }

    IEnumerator PowerSpeedCoroutine()
    {
        moveSpeed *= 2;
        yield return new WaitForSeconds(5);
        hasPowerSpeed = false;
    }

    IEnumerator ShieldCoroutine()
    {
        yield return new WaitForSeconds(5);
        hasPowerShield = false;
    }


    IEnumerator PowerAttackCoroutine()
    {
        yield return new WaitForSeconds(5);
        hasPowerAttack = false;
    }
}