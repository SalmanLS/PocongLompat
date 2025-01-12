using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PocongMovement : MonoBehaviour
{
    private GameObject player; // Reference to the player
    public float jumpForce = 10.0f; // Vertical force for the jump
    public float horizontalForce = 5.0f; // Force toward the player
    public float jumpInterval = 2.0f; // Time between jumps
    private Rigidbody enemyRb;
    private bool isOnGround = true; // To prevent jumping mid-air

    void Start()
    {
        player = GameObject.Find("Player");
        enemyRb = GetComponent<Rigidbody>();
        enemyRb.constraints = RigidbodyConstraints.FreezeRotation;
        StartCoroutine(JumpTowardPlayer());
    }

    void Update()
    {
        if (isOnGround)
        {
            // Calculate the direction to the player
            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
            directionToPlayer.y = 0; // Ignore vertical component

            // Rotate the enemy's body to face the player, adjust for the model orientation
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

            // Flip the enemy by 180 degrees on the Y-axis to correct the backward orientation
            transform.rotation = Quaternion.Euler(-90, targetRotation.eulerAngles.y + 180, 0);
        }
    }

    IEnumerator JumpTowardPlayer()
    {
        while (true)
        {
            if (isOnGround)
            {
                // Calculate the direction toward the player
                Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

                // Apply jump force (vertical + horizontal components)
                Vector3 jumpVector = new Vector3(
                    directionToPlayer.x * horizontalForce,
                    jumpForce,
                    directionToPlayer.z * horizontalForce
                );
                enemyRb.AddForce(jumpVector, ForceMode.Impulse);

                // Prevent mid-air jumps
                isOnGround = false;
            }
            yield return new WaitForSeconds(jumpInterval); // Wait before the next jump
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the enemy touches the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
}
