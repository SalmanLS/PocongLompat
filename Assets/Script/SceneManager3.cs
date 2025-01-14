using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneManager3 : MonoBehaviour
{
    public AudioClip button;
    private AudioSource playerAudio;
    public TextMeshProUGUI namaPlayer;
    public TextMeshProUGUI scorePlayer;
    public TextMeshProUGUI wavePlayer;

    public TextMeshProUGUI bestName;
    public TextMeshProUGUI bestScore;
    public TextMeshProUGUI bestWave;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        playerAudio = GetComponent<AudioSource>();
        namaPlayer.text = MainManager.INSTANCE.playerName;
        scorePlayer.text = "" + MainManager.INSTANCE.playerScore;
        wavePlayer.text = "" + MainManager.INSTANCE.playerWave;

        if (MainManager.INSTANCE.playerScore > MainManager.INSTANCE.bestScore)
        {
            MainManager.INSTANCE.bestScore = MainManager.INSTANCE.playerScore;
            MainManager.INSTANCE.bestWave = MainManager.INSTANCE.playerWave;
            MainManager.INSTANCE.bestName = MainManager.INSTANCE.playerName;
            MainManager.INSTANCE.SaveBestScore();
        }
        bestName.text = MainManager.INSTANCE.bestName;
        bestScore.text = "Score : " + MainManager.INSTANCE.bestScore;
        bestWave.text = "Wave : " + MainManager.INSTANCE.bestWave;

    }

    void Update()
    {
        
    }

    public void PlayAgain()
    {
        playerAudio.PlayOneShot(button);
        SceneManager.LoadScene(1);
    }
    public void ButtonMenu()
    {
        playerAudio.PlayOneShot(button);
        StartCoroutine(TimeSwap());
    }

    public void ButtonQuit()
    {
        playerAudio.PlayOneShot(button);
        Application.Quit();
    }
    IEnumerator TimeSwap()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(0);
    }
}
