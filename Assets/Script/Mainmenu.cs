using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using QuantumTek.EncryptedSave;
using Unity.Notifications.Android;

public class Mainmenu : MonoBehaviour
{

    string username;
    [SerializeField] private Text helloText;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text moneyText;
    public GameObject popUp;
    public GameObject logingGP;
    public GameObject settingCanvas;
    public GameObject shop;
    public GameObject premiumShop;
    public GameObject shopBackground;
    public GameObject myProfile;
    public GameObject skinPlayer;
    public GameObject boosterPanel;
    public GameObject dailyRewardPanel;
    public GameObject notEnoughMoney;
    public Admob admob;
    private int selected;
    public Sprite[] imagePlayer;
    public Image playerImage;
    private float playerMoney;
    private int finalscore;

    public GooglePlayScript playScript;

    public GameObject firstGP;

    public GameObject tutorial;

    public GameObject canvasMainMenu;

    public RectTransform playButton;

    private int selectedBackground;
    public Sprite[] imageBackground;
    public SpriteRenderer backgroundImage;

    public Image[] imageMainMenu; 
    public Text[] textMainMenu;

    private bool tutON;

    //New My Profile

    public GameObject startPanelMyProfile;
    public GameObject missionPanel;

    private int playerLevel;
    public GameObject playerLeverResetButton;

    public GameObject missionPopUp;


    //DailyReward/Challange
    public GameObject dailyReward;
    public GameObject dailyChallange;


    public IAPGoogle iap;


    private AudioSource audioSource;
    public AudioClip audioClip;


    private void Awake()
    {
        moneyText = GameObject.Find("moneyText").GetComponent<Text>();
    }



    private void Start()
    {
        SetVsync0_120FPS();

        iap = GameObject.Find("IAP").GetComponent<IAPGoogle>();

        canvasMainMenu.transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(canvasMainMenu, new Vector3(1, 1, 1), 0.6f).setEase(LeanTweenType.easeOutBack);

        
        var notificationIntentData = AndroidNotificationCenter.GetLastNotificationIntent();

        if (notificationIntentData != null)
        {
            var id = notificationIntentData.Id;
            AndroidNotificationCenter.CancelNotification(id);
        }

        audioSource = GetComponent<AudioSource>();


        admob = GameObject.Find("AdmobManager").GetComponent<Admob>();
        settingCanvas.gameObject.SetActive(false);
        if (ES_Save.Exists("finalscore"))
        {
            finalscore = ES_Save.Load<int>("finalscore");
            scoreText.text = finalscore.ToString();
            
        }
        else
        {

            finalscore = 0;
            ES_Save.Save(finalscore, "finalscore");
            scoreText.text = finalscore.ToString();
        }
        
        if (ES_Save.Exists("money"))
        {
            playerMoney = ES_Save.Load<float>("money");
        }
        else
        {
            playerMoney = 0;
            ES_Save.Save(playerMoney, "money");
        }

        if (ES_Save.Exists("selectedSkinNEW"))
        {
            selected = ES_Save.Load<int>("selectedSkinNEW");
        }
        else
        {
            selected = 0;
            ES_Save.Save(selected, "selectedSkinNEW");
        }
        if (ES_Save.Exists("selectedBackground"))
        {
            selectedBackground = ES_Save.Load<int>("selectedBackground");
        }
        else
        {
            selectedBackground = 0;
            ES_Save.Save(selected, "selectedBackground");
        }
        if (ES_Save.Exists("tutON"))
        {
            tutON = ES_Save.Load<bool>("tutON");
        }
        else
        {
            tutON = true;
        }
        RoundMoney();
        popUp.gameObject.SetActive(false);
        
        playerImage.sprite = imagePlayer[selected];
        dailyRewardPanel.SetActive(false);
        //admob.ShowAd();
        SelectedSkinBackground();
        LeanTween.size(playButton, playButton.sizeDelta * 1.1f, 0.5f).setDelay(3f).setEaseInOutCirc().setRepeat(6).setLoopPingPong();

        if (ES_Save.Exists("playerLevel"))
        {
            playerLevel = ES_Save.Load<int>("playerLevel");
            
        }
        else
        {
            playerLevel = 1;
            ES_Save.Save(playerLevel, "playerLevel");
            
        }

    }


    public void SetVsync1() { QualitySettings.vSyncCount = 1; Application.targetFrameRate = -1; }
    public void SetVsync2() { QualitySettings.vSyncCount = 2; Application.targetFrameRate = -1; }
    public void SetVsync0_Default() { QualitySettings.vSyncCount = 0; Application.targetFrameRate = -1; }
    public void SetVsync0_60FPS() { QualitySettings.vSyncCount = 0; Application.targetFrameRate = 60; }
    public void SetVsync0_120FPS() { QualitySettings.vSyncCount = 0; Application.targetFrameRate = 120; }

    private void Update()
    {
        
        
        playerImage.sprite = imagePlayer[selected];
       // RoundMoney();
    }


    public void SelectedSkin()
    {
        selected = ES_Save.Load<int>("selectedSkinNEW");
    }


    public UnityEngine.UI.Shadow[] shadowMainMenu;

    public void SelectedSkinBackground()
    {
        selectedBackground = ES_Save.Load<int>("selectedBackground");
        backgroundImage.sprite = imageBackground[selectedBackground];
        if(selectedBackground == 1)
        {
            for (int i = 0; i < imageMainMenu.Length; i++)
            {
                imageMainMenu[i].color = new Color32(6, 82, 221, 255);
            }
            for (int i = 0; i < textMainMenu.Length; i++)
            {
                textMainMenu[i].color = new Color32(6, 82, 221, 255);
            }
            for (int i = 0; i < shadowMainMenu.Length; i++)
            {
                shadowMainMenu[i].effectColor = new Color32(255, 255, 255, 128);
            }
        }
        if(selectedBackground == 0)
        {
            for (int i = 0; i < imageMainMenu.Length; i++)
            {
                imageMainMenu[i].color = new Color32(255, 255, 255, 255);
            }
            for (int i = 0; i < textMainMenu.Length; i++)
            {
                textMainMenu[i].color = new Color32(255, 255, 255, 255);
            }
            for (int i = 0; i < shadowMainMenu.Length; i++)
            {
                shadowMainMenu[i].effectColor = new Color32(0, 0, 0, 128);
            }
        }
    }

    public void RoundMoney()
    {
        if(moneyText == null)
        {
            moneyText = GameObject.Find("moneyText").GetComponent<Text>();
        }
        playerMoney = ES_Save.Load<float>("money");
        if (playerMoney >= 1000000)
        {
            moneyText.text = Math.Round((playerMoney / 1000000), 1).ToString() + "kk";
        }
        else if (playerMoney >= 10000 && playerMoney<= 999999)
        {
            moneyText.text = Math.Round((playerMoney / 1000), 1).ToString() + "k";
            moneyText.fontSize = 100;
        }
        else if(playerMoney <= 9999)
        {
            moneyText.text = playerMoney.ToString();
            //moneyText.fontSize = 140;
        }
            
    }

    public void LoadGame()
    {
        audioSource.PlayOneShot(audioClip);
        if (tutON)
        {
            tutorial.SetActive(true);
        }
        else
        {
            StartGame();
        }
        
    }

    public void StartGame()
    {
        audioSource.PlayOneShot(audioClip);
        LeanTween.scale(canvasMainMenu, new Vector3(0, 0, 0), 0.3f).setOnComplete(() => SceneManager.LoadScene(1));
        
    }

    public void StartTut()
    {
        audioSource.PlayOneShot(audioClip);
        LeanTween.scale(canvasMainMenu, new Vector3(0, 0, 0), 0.3f).setOnComplete(() => SceneManager.LoadScene(2));
        
    }


    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadHighscoreTable()
    {
       GooglePlayScript.ShowLeaderBoard();
        audioSource.PlayOneShot(audioClip);
    }

    public void ErrorLogOkButton()
    {
        audioSource.PlayOneShot(audioClip);
        popUp.gameObject.SetActive(true);
        
    }

    public void PopUpOkButton()
    {
        audioSource.PlayOneShot(audioClip);
        Debug.Log("Pressed OkButton");
        LeanTween.scale(popUp, new Vector3(0, 0, 0), 0.3f).setOnComplete(TurnoffGP);
        firstGP.SetActive(false);
        
    }

    private void TurnoffGP()
    {
        audioSource.PlayOneShot(audioClip);
        logingGP.gameObject.SetActive(false);
        
    }

    public void PopUpReturnButton()
    {
        audioSource.PlayOneShot(audioClip);

        Debug.Log("Pressed ReturnB");
        SceneManager.LoadScene(0);
    }

    public void SettingButton()
    {
        audioSource.PlayOneShot(audioClip);
        settingCanvas.gameObject.SetActive(true);
        settingCanvas.transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(settingCanvas, new Vector3(1, 1, 1), 0.6f).setEase(LeanTweenType.easeOutBack);

    }
    public void ExitSettingButton()
    {
        audioSource.PlayOneShot(audioClip);
        LeanTween.scale(settingCanvas, new Vector3(0, 0, 0), 0.3f).setOnComplete(CloseSetting);

    }

    private void CloseSetting()
    {
        settingCanvas.gameObject.SetActive(false);
    }

    public void LoadShop()
    {
        audioSource.PlayOneShot(audioClip);
        iap.RefreshMoneyLocalized();
        shop.gameObject.SetActive(true);
        shop.transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(shop, new Vector3(1, 1, 1), 0.6f).setEase(LeanTweenType.easeOutBack);
        premiumShop.gameObject.SetActive(true);
 

    }

    public void ExitShopButton()
    {
        audioSource.PlayOneShot(audioClip);
        LeanTween.scale(shop, new Vector3(0, 0, 0), 0.3f).setOnComplete(() => shop.SetActive(false));


    }



    public void MyProfile()
    {
        audioSource.PlayOneShot(audioClip);
        myProfile.gameObject.SetActive(true);
        myProfile.transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(myProfile, new Vector3(1, 1, 1), 0.6f).setEase(LeanTweenType.easeOutBack);
        skinPlayer.gameObject.SetActive(false);
        boosterPanel.gameObject.SetActive(false);
        missionPanel.gameObject.SetActive(false);
        shopBackground.SetActive(false);
        startPanelMyProfile.gameObject.SetActive(true);
        playerLeverResetButton.SetActive(false);
        startPanelMyProfile.gameObject.transform.localScale = new Vector3(1, 1, 1);

    }
    public void ExitMyProfileButton()
    {
        audioSource.PlayOneShot(audioClip);
        LeanTween.scale(myProfile, new Vector3(0, 0, 0), 0.3f).setOnComplete(CloseMyProfile);

    }
    private void CloseMyProfile()
    {
        myProfile.SetActive(false);
    }

    public void Boosters()
    {
        audioSource.PlayOneShot(audioClip);

        playerLeverResetButton.SetActive(false);
        if (skinPlayer.activeSelf)
        {
            LeanTween.scale(skinPlayer, new Vector3(0, 0, 0), 0.3f).setOnComplete(() => skinPlayer.gameObject.SetActive(false));
        }
        if (missionPanel.activeSelf)
        {
            LeanTween.scale(missionPanel, new Vector3(0, 0, 0), 0.3f).setOnComplete(() => missionPanel.gameObject.SetActive(false));
        }
        if (startPanelMyProfile.activeSelf)
        {
            LeanTween.scale(startPanelMyProfile, new Vector3(0, 0, 0), 0.3f).setOnComplete(() => startPanelMyProfile.gameObject.SetActive(false));
        }
        if (shopBackground.activeSelf)
        {
            LeanTween.scale(shopBackground, new Vector3(0, 0, 0), 0.3f).setOnComplete(() => shopBackground.gameObject.SetActive(false));
        }

        boosterPanel.gameObject.SetActive(true);
        boosterPanel.transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(boosterPanel, new Vector3(1, 1, 1), 0.6f).setEase(LeanTweenType.easeOutBack).setDelay(0.3f);

    }

    public void SkinsPanel()
    {
        audioSource.PlayOneShot(audioClip);

        playerLeverResetButton.SetActive(false);
        if (boosterPanel.activeSelf)
        {
            LeanTween.scale(boosterPanel, new Vector3(0, 0, 0), 0.3f).setOnComplete(() => boosterPanel.gameObject.SetActive(false));
        }
        if (missionPanel.activeSelf)
        {
            LeanTween.scale(missionPanel, new Vector3(0, 0, 0), 0.3f).setOnComplete(() => missionPanel.gameObject.SetActive(false));
        }
        if (startPanelMyProfile.activeSelf)
        {
            LeanTween.scale(startPanelMyProfile, new Vector3(0, 0, 0), 0.3f).setOnComplete(() => startPanelMyProfile.gameObject.SetActive(false));
        }
        if (shopBackground.activeSelf)
        {
            LeanTween.scale(shopBackground, new Vector3(0, 0, 0), 0.3f).setOnComplete(() => shopBackground.gameObject.SetActive(false));
        }

        skinPlayer.gameObject.SetActive(true);
        skinPlayer.transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(skinPlayer, new Vector3(1, 1, 1), 0.6f).setEase(LeanTweenType.easeOutBack).setDelay(0.3f);
    }

    public void MissionPanel()
    {
        audioSource.PlayOneShot(audioClip);

        if (boosterPanel.activeSelf)
        {
            LeanTween.scale(boosterPanel, new Vector3(0, 0, 0), 0.3f).setOnComplete(() => boosterPanel.gameObject.SetActive(false));
        }
        if (skinPlayer.activeSelf)
        {
            LeanTween.scale(skinPlayer, new Vector3(0, 0, 0), 0.3f).setOnComplete(() => skinPlayer.gameObject.SetActive(false));
        }
        if (startPanelMyProfile.activeSelf)
        {
            LeanTween.scale(startPanelMyProfile, new Vector3(0, 0, 0), 0.3f).setOnComplete(() => startPanelMyProfile.gameObject.SetActive(false));
        }
        if (shopBackground.activeSelf)
        {
            LeanTween.scale(shopBackground, new Vector3(0, 0, 0), 0.3f).setOnComplete(() => shopBackground.gameObject.SetActive(false));
        }
       playerLeverResetButton.SetActive(true);
        missionPanel.gameObject.SetActive(true);
        missionPanel.transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(missionPanel, new Vector3(1, 1, 1), 0.6f).setEase(LeanTweenType.easeOutBack).setDelay(0.3f);
    }

    public void StartPanel()
    {
        audioSource.PlayOneShot(audioClip);

        playerLeverResetButton.SetActive(false);
        if (boosterPanel.activeSelf)
        {
            LeanTween.scale(boosterPanel, new Vector3(0, 0, 0), 0.3f).setOnComplete(() => boosterPanel.gameObject.SetActive(false));
        }
        if (skinPlayer.activeSelf)
        {
            LeanTween.scale(skinPlayer, new Vector3(0, 0, 0), 0.3f).setOnComplete(() => skinPlayer.gameObject.SetActive(false));
        }
        if (missionPanel.activeSelf)
        {
            LeanTween.scale(missionPanel, new Vector3(0, 0, 0), 0.3f).setOnComplete(() => missionPanel.gameObject.SetActive(false));
        }
        if (shopBackground.activeSelf)
        {
            LeanTween.scale(shopBackground, new Vector3(0, 0, 0), 0.3f).setOnComplete(() => shopBackground.gameObject.SetActive(false));
        }

        startPanelMyProfile.gameObject.SetActive(true);
        startPanelMyProfile.transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(startPanelMyProfile, new Vector3(1, 1, 1), 0.6f).setEase(LeanTweenType.easeOutBack).setDelay(0.3f);
    }

    public void BackgroundPanel()
    {
        audioSource.PlayOneShot(audioClip);

        if (boosterPanel.activeSelf)
        {
            LeanTween.scale(boosterPanel, new Vector3(0, 0, 0), 0.3f).setOnComplete(() => boosterPanel.gameObject.SetActive(false));
        }
        if (skinPlayer.activeSelf)
        {
            LeanTween.scale(skinPlayer, new Vector3(0, 0, 0), 0.3f).setOnComplete(() => skinPlayer.gameObject.SetActive(false));
        }
        if (missionPanel.activeSelf)
        {
            LeanTween.scale(missionPanel, new Vector3(0, 0, 0), 0.3f).setOnComplete(() => missionPanel.gameObject.SetActive(false));
        }
        if (startPanelMyProfile.activeSelf)
        {
            LeanTween.scale(startPanelMyProfile, new Vector3(0, 0, 0), 0.3f).setOnComplete(() => startPanelMyProfile.gameObject.SetActive(false));
        }

        shopBackground.gameObject.SetActive(true);
        shopBackground.transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(shopBackground, new Vector3(1, 1, 1), 0.6f).setEase(LeanTweenType.easeOutBack).setDelay(0.3f);
    }



    public void Deletebooster()
    {
        ES_Save.DeleteData("timeScoreBooster");
        ES_Save.DeleteData("jumpBoosterTime2");
        ES_Save.DeleteData("doubleScoreBooster");
        ES_Save.DeleteData("jumpBoosterBool");
        ES_Save.DeleteData("skinlist");
    }


    public void DailyReward()
    {
        audioSource.PlayOneShot(audioClip);

        dailyRewardPanel.SetActive(true);
        dailyRewardPanel.transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(dailyRewardPanel, new Vector3(1, 1, 1), 0.6f).setEase(LeanTweenType.easeOutBack);
    }

    public void ExitDailyReward()
    {
        audioSource.PlayOneShot(audioClip);

        LeanTween.scale(dailyRewardPanel, new Vector3(0, 0, 0), 0.3f).setOnComplete(CloseDailyReward);
    }

    private void CloseDailyReward()
    {
        dailyRewardPanel.SetActive(false);
    }


    public void ExitNotEnoughtMoney()
    {
        audioSource.PlayOneShot(audioClip);

        LeanTween.scale(notEnoughMoney, new Vector3(0, 0, 0), 0.3f).setOnComplete(CloseNotEnought);
    }

    private void CloseNotEnought()
    {
        notEnoughMoney.SetActive(false);
    }


    public void MissionPopUp()
    {
        audioSource.PlayOneShot(audioClip);

        missionPopUp.SetActive(true);
        
    }

    public void CloseMissionPopUp()
    {
        audioSource.PlayOneShot(audioClip);

        LeanTween.scale(missionPopUp, new Vector3(0, 0, 0), 0.3f).setOnComplete(()=>missionPopUp.SetActive(false));
    }


    public void DailyRewardButton()
    {
        audioSource.PlayOneShot(audioClip);

        if (dailyReward.activeSelf)
        {
            dailyReward.SetActive(true);
        }
        else
        {
            LeanTween.scale(dailyChallange, new Vector3(0, 0, 0), 0.3f).setOnComplete(() => dailyChallange.gameObject.SetActive(false));
        }
        dailyReward.gameObject.SetActive(true);
        dailyReward.transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(dailyReward, new Vector3(1, 1, 1), 0.6f).setEase(LeanTweenType.easeOutBack).setDelay(0.3f);
    }

    public void DailyChallangeButton()
    {
        audioSource.PlayOneShot(audioClip);

        if (dailyChallange.activeSelf)
        {
            dailyChallange.SetActive(true);
        }
        else
        {
            LeanTween.scale(dailyReward, new Vector3(0, 0, 0), 0.3f).setOnComplete(() => dailyReward.gameObject.SetActive(false));
        }
        dailyChallange.gameObject.SetActive(true);
        dailyChallange.transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(dailyChallange, new Vector3(1, 1, 1), 0.6f).setEase(LeanTweenType.easeOutBack).setDelay(0.3f);
    }



}
