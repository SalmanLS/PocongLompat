using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneManager1 : MonoBehaviour
{
    public AudioClip playbutton;
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
        playerAudio.PlayOneShot(playbutton);
        StartCoroutine(TimeSwap());
    }
    public void exitScene2()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator TimeSwap()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }

}
