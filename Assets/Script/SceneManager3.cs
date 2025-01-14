using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneManager3 : MonoBehaviour
{
    public AudioClip button;
    private AudioSource playerAudio;
    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
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
