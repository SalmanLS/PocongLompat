using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneManager1 : MonoBehaviour
{
    public AudioClip gameClip;
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
    public void GoScene2()
    {
        playerAudio.PlayOneShot(gameClip, 10.0f);
        SceneManager.LoadScene(1);
    }
    public void exitScene2()
    {
        SceneManager.LoadScene(0);
    }

}
