using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f; // Maximum health
    private float currentHealth; // Current health

    public Slider healthBar; // Reference to the health bar UI slider

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.value = currentHealth;
        Debug.Log("Health Bar: " + healthBar.value);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1f);
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
