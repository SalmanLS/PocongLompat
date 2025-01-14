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
    public TMP_InputField namaInput;
    public TextMeshProUGUI alertText;

    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GoToPlay()
    {
        playerAudio.PlayOneShot(button);
        if (namaInput.text == "")
        {
            alertText.gameObject.SetActive(true);
            return;
        }
        else
        {
            MainManager.INSTANCE.playerName = namaInput.text;
            StartCoroutine(TimeSwap());
        }

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
    public void exitGame()
    {
        Application.Quit();
    }

    IEnumerator TimeSwap()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(1);
    }

}
