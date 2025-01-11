using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f; 
    private float currentHealth; 
    public Slider healthBar; 

    private PlayerMovement playerMovement;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.value = currentHealth;
        Debug.Log("Health Bar: " + healthBar.value);
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (playerMovement.hasPowerAttack)
            {
                TakeDamage(10f);
            }
            else
            {
                TakeDamage(0f);
            }
            Debug.Log("Health Bar Updated: " + healthBar.value);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0f)
        {
            currentHealth = 0f;
            Die();
        }

        UpdateHealthBar();
    }
    private void UpdateHealthBar()
    {
        healthBar.value = currentHealth; // Update the health bar value
        Debug.Log("Health: " + currentHealth);
    }

    private void Die()
    {
        // Destroy the enemy
        Destroy(gameObject);
    }
}
