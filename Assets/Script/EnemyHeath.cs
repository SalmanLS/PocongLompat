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
    private bool isDead = false; 
    private Rigidbody enemyRb;
    public int objectScore = 0;

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
        if (isDead) return; 

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
        if (playerAudio && pocongDeath)
        {
            playerAudio.PlayOneShot(pocongDeath);
        }
        if (enemyRb)
        {
            enemyRb.isKinematic = true; 
        }
        sceneManager.AddScore(objectScore);

        StartCoroutine(KillPocong());
    }

    IEnumerator KillPocong()
    {
        yield return new WaitForSeconds(pocongDeath.length);
        Destroy(gameObject);
        
    }

    public bool IsDead()
    {
        return isDead;
    }
}
