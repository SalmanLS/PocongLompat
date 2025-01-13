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
        healthBar.value = currentHealth; 
        Debug.Log("Health: " + currentHealth);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
