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
    private float boundary = 4.0f; 
    public TextMeshProUGUI waveText; 

    private bool isSpawningPowerUps = false; 
    private bool isSpawningWave = false; 

    void Start()
    {
        StartCoroutine(SpawnWaveAndPocong(enemyNumber)); 
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
    }

    private void SpawnPocong(int numberOfEnemies)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Instantiate(pocongPrefab, GenerateSpawnLocation(), pocongPrefab.transform.rotation);
        }
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

    public Vector3 GenerateSpawnLocation()
    {
        Vector3 spawnLocation = new Vector3(
            Random.Range(-boundary, boundary),
            2.0f,
            Random.Range(-boundary, boundary)
        );
        return spawnLocation;
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
        waveText.gameObject.SetActive(true); 
        yield return new WaitForSeconds(3f); 
        waveText.gameObject.SetActive(false); 
        SpawnPocong(number); 
        isSpawningWave = false; 
    }
}
