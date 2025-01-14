using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SceneManager2 : MonoBehaviour
{
    public GameObject pocongPrefab;
    public GameObject[] powerUpPrefab;
    private int enemyNumber = 1;
    private int enemyAlive;
    private int powerUpCount;
    private float boundary = 20.0f;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI activeText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreWaveText;
    private bool isSpawningPowerUps = false;
    private bool isSpawningWave = false;
    private bool isShowingPower = false;

    private PlayerMovement PlayerMovement;

    private int score;
    private int wave;

    void Start()
    {
        score = 0;
        MainManager.INSTANCE.playerScore = score;
        PlayerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        StartCoroutine(SpawnWaveAndPocong(enemyNumber));
        AddWave(enemyNumber);
        SpawnPowerUps();
    }

    void Update()
    {

        enemyAlive = FindObjectsOfType<PocongMovement>().Length;
        if (enemyAlive == 0 && !isSpawningWave)
        {
            enemyNumber++;
            StartCoroutine(SpawnWaveAndPocong(enemyNumber));
        }
        powerUpCount = FindObjectsOfType<ObjectBounce>().Length;
        if (powerUpCount == 0 && !isSpawningPowerUps)
        {
            StartCoroutine(SpawnPowers());
        }

        if (PlayerMovement.hasPowerAttack && !isShowingPower)
        {
            StartCoroutine(ShowActivePower("Attack"));
        }
        else if (PlayerMovement.hasPowerSpeed && !isShowingPower)
        {
            StartCoroutine(ShowActivePower("Speed"));
        }
        else if (PlayerMovement.hasPowerShield && !isShowingPower)
        {
            StartCoroutine(ShowActivePower("Shield"));
        }
    }
    private void SpawnPocong(int numberOfEnemies)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            pocongPrefab.GetComponent<PocongMovement>().jumpInterval = Random.Range(1.0f, 3.0f);
            pocongPrefab.GetComponent<PocongMovement>().horizontalForce = Random.Range(5.0f, 7.0f);
            Instantiate(pocongPrefab, GenerateSpawnLocation(), pocongPrefab.transform.rotation);
        }
    }

    public Vector3 GenerateSpawnLocation()
    {
        Vector3 spawnLocation = new Vector3(
            Random.Range(-boundary, boundary),
            2.0f,
            Random.Range(-boundary, boundary)
        );
        return spawnLocation;
    }
    private void SpawnPowerUps()
    {
        int powerUpIndex = Random.Range(0, powerUpPrefab.Length);
        Instantiate(
            powerUpPrefab[powerUpIndex],
            GenerateSpawnLocation(),
            powerUpPrefab[powerUpIndex].transform.rotation
        );
    }
    private void ShowWaveText(int waveNumber)
    {
        waveText.text = "Wave " + waveNumber;
    }

    IEnumerator SpawnPowers()
    {
        isSpawningPowerUps = true;
        yield return new WaitForSeconds(5f);
        SpawnPowerUps();
        isSpawningPowerUps = false;
    }

    IEnumerator SpawnWaveAndPocong(int number)
    {
        isSpawningWave = true;
        ShowWaveText(number);
        AddWave(number);
        waveText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        waveText.gameObject.SetActive(false);
        SpawnPocong(number);
        isSpawningWave = false;
    }

    IEnumerator ShowActivePower(string power)
    {
        if (isShowingPower) yield break; 

        isShowingPower = true;
        ShowActiveText(power); 
        activeText.gameObject.SetActive(true);

        yield return new WaitForSeconds(5f);

        activeText.gameObject.SetActive(false);
        isShowingPower = false; 
    }

    private void ShowActiveText(string power)
    {
        activeText.text = power + " Power Active"; 
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score : " + score;
        MainManager.INSTANCE.playerScore = score;
    }
    private void AddWave(int waveScore)
    {
        scoreWaveText.text = "Wave : " + waveScore;
        MainManager.INSTANCE.playerWave = waveScore;
    }
}