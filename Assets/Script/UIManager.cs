using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    //[SerializeField] private Text liveGame;
    [SerializeField] private Image lives;
    [SerializeField] private Image coinImg;
    [SerializeField]
    private Sprite[] _liveSprite;
    [SerializeField] private Text scoreGame;
    [SerializeField] private Text distanceGame;
    [SerializeField] private Text scoreDead;
    [SerializeField] private Text distanceDead;
    [SerializeField] private GameObject resetButton;
    [SerializeField] private GameObject mainmenuButton;
    [SerializeField] private GameObject highScore;
    [SerializeField] private GameObject deadUI;
    [SerializeField] private GameObject panelBar;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private Text moneyText;
    [SerializeField] private Text scoreBest;

    private Player player;
    [SerializeField] private GameObject pauseMenu;
    public bool pauseOn;

    public float animationTime = 1.5f;
    private float desiredNumber;
    private float initialNumber;
    private float currentNumber;
    private bool slideNumber;

    public GameObject doubleScoreFire;
    public GameObject doubleMoneyFire;

    public GameObject canvasGame;

    public AudioSource audioSource;
    public AudioClip audioClip;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        //scoreGame.text = "Coin: " + 0;
        //liveGame.text = "Lives" + 3;
        distanceGame.text = "x" + 0;
        mainmenuButton.gameObject.SetActive(false);
        resetButton.gameObject.SetActive(false);
        highScore.SetActive(false);
        deadUI.SetActive(false);
        scoreBest.text = "SCORE";
        slideNumber = false;
        currentNumber = 0;
        UpdateLives(3);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreGame.text = player.HowMoney().ToString();
       // liveGame.text = "Lives: " + player.Live();
        distanceGame.text = player.Distance().ToString();

        distanceGame.text = player.Distance().ToString();
            //distanceGame.color = new Color32(255, 0, 0, 255);

        if (slideNumber)
        {
            if(currentNumber != desiredNumber)
            {
                    currentNumber += (animationTime * Time.deltaTime) * (desiredNumber - initialNumber);
                    if (currentNumber >= desiredNumber)
                        currentNumber = desiredNumber;

                distanceDead.text = currentNumber.ToString("0");
            }
        }

        


    }
    public void UpdateLives(int currentLives)
    {
        lives.sprite = _liveSprite[currentLives];
        if(currentLives <= 3)
        {
            lives.rectTransform.sizeDelta = new Vector2(750, 250);
        }else
            if(currentLives >= 4)
        {
            lives.rectTransform.sizeDelta = new Vector2(1000, 250);
        }
    }

    public void DeadUI()
    {
        resetButton.gameObject.SetActive(true);
        mainmenuButton.gameObject.SetActive(true);
        scoreGame.gameObject.SetActive(false);
        lives.gameObject.SetActive(false);
        coinImg.gameObject.SetActive(false);
        distanceGame.gameObject.SetActive(false);
        highScore.gameObject.SetActive(true);
        deadUI.gameObject.SetActive(true);
        if (player.AllMoney() >= 1000000)
        {
            moneyText.text = Math.Round((player.AllMoney() / 1000000), 1).ToString() + "kk";
        }
        else if (player.AllMoney() >= 10000 && player.AllMoney() <= 999999)
        {
            moneyText.text = Math.Round((player.AllMoney() / 1000), 1).ToString() + "k";
            moneyText.fontSize = 100;
        }
        else if (player.AllMoney() <= 9999)
        {
            moneyText.text = player.AllMoney().ToString();
            //moneyText.fontSize = 140;
        }
        //moneyText.text = player.AllMoney().ToString();
        scoreDead.text = player.HowMoney().ToString();
        //distanceDead.text = player.Distance().ToString();
        desiredNumber = player.Distance();
        slideNumber = true;
        panelBar.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(false);
        doubleMoneyFire.SetActive(false);
        doubleScoreFire.SetActive(false);

    }

    public void NewBest()
    {
        scoreBest.text = "NEW BEST";
    }


    public void LoadHighscoreTable()
    {
        audioSource.PlayOneShot(audioClip);
        GooglePlayScript.ShowLeaderBoard();
    }
    public void LoadMainMenu()
    {
        audioSource.PlayOneShot(audioClip);

        LeanTween.scale(canvasGame, new Vector3(0, 0, 0), 0.3f).setOnComplete(() => SceneManager.LoadScene(0));
    }
    public void LoadGame()
    {
        audioSource.PlayOneShot(audioClip);

        LeanTween.scale(canvasGame, new Vector3(0, 0, 0), 0.3f).setOnComplete(() => SceneManager.LoadScene(1));
    }

}
