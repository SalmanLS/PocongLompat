using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneManager1 : MonoBehaviour
{
    public AudioClip button;
    private AudioSource playerAudio;
    public Canvas canvasMenu;
    public Canvas canvasTutorial;

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
        playerAudio.PlayOneShot(button);
        StartCoroutine(TimeSwap());
    }
    public void GoToTutorial()
    {
        playerAudio.PlayOneShot(button);
        canvasMenu.gameObject.SetActive(false);
        canvasTutorial.gameObject.SetActive(true);
    }
    public void BackToMenu()
    {
        playerAudio.PlayOneShot(button);
        canvasTutorial.gameObject.SetActive(false);
        canvasMenu.gameObject.SetActive(true);
    }
    public void exitScene2()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator TimeSwap()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(1);
    }

}
