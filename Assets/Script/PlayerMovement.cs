using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Camera mainCamera;
    public float baseMoveSpeed = 5f; // Default move speed
    private float currentMoveSpeed;
    public bool hasPowerSpeed = false;
    public bool hasPowerAttack = false;
    public bool hasPowerShield = false;

    private void Start()
    {
        mainCamera = Camera.main;
        currentMoveSpeed = baseMoveSpeed;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        RotatePlayerTowardsCamera();
        MovePlayer();
        if (Input.GetKeyDown(KeyCode.Space) && hasPowerAttack)
        {
            DamageClosestEnemy();
        }
    }

    private void RotatePlayerTowardsCamera()
    {
        if (mainCamera != null)
        {
            Vector3 cameraForward = mainCamera.transform.forward;
            cameraForward.y = 0f;

            if (cameraForward != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(cameraForward);
            }
        }
    }

    private void MovePlayer()
    {
        currentMoveSpeed = hasPowerSpeed ? baseMoveSpeed * 2 : baseMoveSpeed;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        transform.position += moveDirection * currentMoveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                if (hasPowerShield)
                {
                    Destroy(collision.gameObject); 
                }
                else
                {
                    Debug.Log("Game Over");
                }
                break;

            case "PowerShield":
                ActivatePowerUp(collision, PowerType.Shield);
                break;

            case "PowerSpeed":
                ActivatePowerUp(collision, PowerType.Speed);
                break;

            case "PowerAttack":
                ActivatePowerUp(collision, PowerType.Attack);
                break;
        }
    }

    private void DamageClosestEnemy()
    {
        EnemyHealth[] enemies = FindObjectsOfType<EnemyHealth>();
        if (enemies.Length == 0) return;

        EnemyHealth closestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (var enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            closestEnemy.TakeDamage(10f);
            Debug.Log("Damaged closest enemy: " + closestEnemy.name);
        }
    }

    private void ActivatePowerUp(Collision collision, PowerType powerType)
    {
        Destroy(collision.gameObject);

        switch (powerType)
        {
            case PowerType.Shield:
                hasPowerShield = true;
                StartCoroutine(DeactivatePowerAfterDuration(PowerType.Shield, 5f));
                break;

            case PowerType.Speed:
                hasPowerSpeed = true;
                StartCoroutine(DeactivatePowerAfterDuration(PowerType.Speed, 5f));
                break;

            case PowerType.Attack:
                hasPowerAttack = true;
                StartCoroutine(DeactivatePowerAfterDuration(PowerType.Attack, 5f));
                break;
        }
    }

    private IEnumerator DeactivatePowerAfterDuration(PowerType powerType, float duration)
    {
        yield return new WaitForSeconds(duration);

        switch (powerType)
        {
            case PowerType.Shield:
                hasPowerShield = false;
                break;

            case PowerType.Speed:
                hasPowerSpeed = false;
                break;

            case PowerType.Attack:
                hasPowerAttack = false;
                break;
        }
    }
}

public enum PowerType
{
    Shield,
    Speed,
    Attack
}
