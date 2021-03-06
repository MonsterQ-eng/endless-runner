using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] private Text wiater;
    public GameObject waiterText;

    public GameObject canvasGame;
    public MissionHandler msHandler;

    private AudioSource audioSource;
    public AudioClip audioClip;

    void Start()
    {
        gameObject.SetActive(false);
        msHandler = GameObject.Find("MissionHandler").GetComponent<MissionHandler>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            waiterText.SetActive(true);
            waiterText.GetComponent<WiaterTextSet>().StartCouri();
            gameObject.SetActive(false);
        }
    }

    public void Resume()
    {
        audioSource.PlayOneShot(audioClip);

        waiterText.SetActive(true);
        waiterText.GetComponent<WiaterTextSet>().StartCouri();
        gameObject.SetActive(false);
    }



    public void PauseGame()
    {
        audioSource.PlayOneShot(audioClip);

        msHandler.MissionDoIt();
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }



    public void LoadMainMenu()
    {

        Time.timeScale = 1f;
        audioSource.PlayOneShot(audioClip);

        LeanTween.scale(canvasGame, new Vector3(0, 0, 0), 0.3f).setOnComplete(() => SceneManager.LoadScene(0));
    }

    public void Reload()
    {
        Time.timeScale = 1f;
        audioSource.PlayOneShot(audioClip);

        LeanTween.scale(canvasGame, new Vector3(0, 0, 0), 0.3f).setOnComplete(() => SceneManager.LoadScene(1));
    }


}
