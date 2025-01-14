using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public Slider healthBar;
    public AudioClip pocongDeath;
    private AudioSource playerAudio;
    private bool isDead = false; // Prevent duplicate death logic
    private Rigidbody enemyRb;
    public int objectScore;

    private SceneManager2 sceneManager;

    private void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        enemyRb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        healthBar.value = currentHealth;

        sceneManager=GameObject.Find("GameManager2").GetComponent<SceneManager2>();
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return; // Skip logic if already dead

        currentHealth -= damage;

        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            Die();
        }

        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        healthBar.value = currentHealth;
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;

        // Play death sound
        if (playerAudio && pocongDeath)
        {
            playerAudio.PlayOneShot(pocongDeath);
        }

        // Freeze the enemy in place
        if (enemyRb)
        {
            enemyRb.isKinematic = true; // Stop physics interaction
        }
        sceneManager.AddScore(objectScore);

        // Optionally disable movement or rotation scripts here
        StartCoroutine(KillPocong());
    }

    IEnumerator KillPocong()
    {
        yield return new WaitForSeconds(pocongDeath.length); // Wait for sound to finish
        Destroy(gameObject);
        
    }

    public bool IsDead()
    {
        return isDead;
    }
}
