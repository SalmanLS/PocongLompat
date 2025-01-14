using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PocongMovement : MonoBehaviour
{
    private GameObject player;
    public float jumpForce = 3.0f;
    public float horizontalForce = 5.0f;
    public float jumpInterval = 2.0f;
    private Rigidbody enemyRb;
    public bool isOnGround = true;
    private float offGroundTime = 0f;
    public float respawnTime = 2f;
    public AudioClip pocongStep;
    public AudioClip pocongSound;
    private AudioSource playerAudio;
    public bool isPocongSoundPlayed = false;
    public float minSoundInterval = 1f;
    public float maxSoundInterval = 20f;
    private EnemyHealth enemyHealth;

    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        playerAudio = GetComponent<AudioSource>();
        player = GameObject.Find("Player");
        enemyRb = GetComponent<Rigidbody>();
        enemyRb.constraints = RigidbodyConstraints.FreezeRotation;
        StartCoroutine(JumpTowardPlayer());
        StartCoroutine(RandomPocongSound());
    }

    void Update()
    {
        if (!isOnGround)
        {
            offGroundTime += Time.deltaTime;

            if (offGroundTime >= respawnTime)
            {
                Respawn();
            }
        }
        else
        {
            offGroundTime = 0f;
        }
        if (isOnGround && !GetComponent<EnemyHealth>().IsDead())
        {
            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
            directionToPlayer.y = 0; 
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Euler(-90, targetRotation.eulerAngles.y + 180, 0);
        }
        else if (GetComponent<EnemyHealth>().IsDead())
        {
            transform.position = transform.position;
        }
    }

    IEnumerator JumpTowardPlayer()
    {
        while (true)
        {
            if (isOnGround)
            {
                Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

                Vector3 jumpVector = new Vector3(
                    directionToPlayer.x * horizontalForce,
                    jumpForce,
                    directionToPlayer.z * horizontalForce
                );
                enemyRb.AddForce(jumpVector, ForceMode.Impulse);

                isOnGround = false;
            }
            yield return new WaitForSeconds(jumpInterval);
        }
    }

    IEnumerator RandomPocongSound()
    {
        while (true)
        {
            float randomInterval = Random.Range(minSoundInterval, maxSoundInterval);
            yield return new WaitForSeconds(randomInterval);

            if (!isPocongSoundPlayed)
            {
                isPocongSoundPlayed = true;
                playerAudio.PlayOneShot(pocongSound);
                yield return new WaitForSeconds(pocongSound.length);
                isPocongSoundPlayed = false;
            }
        }
    }

    private void Respawn()
    {
        transform.position =  new Vector3(player.transform.position.x + 1.5f, 2.0f, player.transform.position.z + 1.5f);
        offGroundTime = 0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            playerAudio.PlayOneShot(pocongStep);
            isOnGround = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = false;
        }
    }
}
