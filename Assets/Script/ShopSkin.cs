using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QuantumTek.EncryptedSave;

public class ShopSkin : MonoBehaviour
{

    public Mainmenu MAIN_MENU;

    public Transform skinPanel;
    public Text skinBuySetText;
    public GameObject notmoneyText;
    private int[] skinCost = new int[] { 1, 3000, 3000, 3000, 5000, 3000, 3000, 5000, 3000, 3000, 10000, 10000, 15000, 10000 };
    private int selectedSkinIndex;
    [SerializeField]bool[] skinList = new bool[32];
    private String[] skinName = {"Toast!", "Alien!", "CartonBox!", "Dark Cat!", "Ghost!", "IceCUBE!", "Moon!", "Panda!", "Sun!", "Zombie!", "Cupcake!", "Snail!", "Unicorn!"};
    public Image skinPreview;
    public Sprite[] skinPrevieSprite;
    public Text skinNameText;
    private float money;
    public int selected;
    public Admob admob;
    private int[] scoreDoubleCost = new int[] { 2000, 10000, 100000 };
    private int[] jumpBooster = new int[] { 2000, 10000, 100000 };
    private int[] ubreakableBoosterCost = new int[] { 3000, 10000, 100000};
    private int[] allBoosterFasterCost = new int[] { 3000, 10000, 100000 };
    private int[] coinMagnetFasterCost = new int[] { 3000, 10000, 100000 };
    public float timeScoreBooster;
    public float jumpBoosterTime;
    public float ubreakableBoosterTime;
    public float coinMagnetBoosterTime;
    public float allBoosterFasterTimeDown;
    public float allBoosterFasterTimeUp;
    public bool[] doubleScoreBooster = new bool[4];
    public bool[] jumpBoosterBool = new bool[4];
    public bool[] ubreakableBoosterBool = new bool[4];
    public bool[] coinMagnetBool = new bool[4];
    public bool[] allBoosterFasterBool = new bool[4];
    public GameObject buyButton;
    public GameObject buyButtonJump;
    public GameObject buyButtonSkinsPlayer;
    public GameObject ubreakableBoosterButton;
    public GameObject coinMagnetButton;
    public GameObject allBoosterFasterButton;
    public Text buyButtonText;
    public Text buyButtonJumpText;
    public Text ubreakableBoosterButtonText;
    public Text coinMagnetBoosterButtonText;
    public Text allBoosterFasterButtonText;
    public GameObject[] lvlScoreDouble;
    public GameObject[] lvlJumpBoost;
    public GameObject[] lvlUbreakableBooster;
    public GameObject[] lvlcoinMagnetBooster;
    public GameObject[] lvlAllBoosterFaster;


    public GooglePlayScript gps;

    // Start is called before the first frame update
    void Start()
    {

        MAIN_MENU = GameObject.Find("Canvas").GetComponent<Mainmenu>();

        admob = GameObject.Find("AdmobManager").GetComponent<Admob>();
        gameObject.SetActive(false);

        if (ES_Save.Exists("allBoosterFasterTimeUp"))
        {
            Debug.Log("Finded allBoosterFasterTimeUp");
            allBoosterFasterTimeUp = ES_Save.Load<float>("allBoosterFasterTimeUp");
        }
        else
        {
            Debug.Log("Create a allBoosterFasterTimeUp");
            allBoosterFasterTimeUp = 80f;
            ES_Save.Save(allBoosterFasterTimeUp, "allBoosterFasterTimeUp");
        }

        if (ES_Save.Exists("allBoosterFasterTimeDown"))
        {
            Debug.Log("Finded allBoosterFasterTime");
            allBoosterFasterTimeDown = ES_Save.Load<float>("allBoosterFasterTimeDown");
        }
        else
        {
            Debug.Log("Create a allBoosterFasterTimeDown");
            allBoosterFasterTimeDown = 70f;
            ES_Save.Save(allBoosterFasterTimeDown, "allBoosterFasterTimeDown");
        }
        if (ES_Save.Exists("allBoosterFasterBool"))
        {
            Debug.Log("Finded allBoosterFasterBool");
            allBoosterFasterBool = ES_Save.Load<bool[]>("allBoosterFasterBool");
        }
        else
        {
            Debug.Log("Create a allBoosterFasterBool");
            allBoosterFasterBool[0] = true;
            allBoosterFasterBool[1] = false;
            allBoosterFasterBool[2] = false;
            allBoosterFasterBool[3] = false;
            ES_Save.Save<bool[]>(allBoosterFasterBool, "allBoosterFasterBool");
        }

        if (ES_Save.Exists("timeCoinMagnet"))
        {
            Debug.Log("Finded timeCoinMagnet");
            coinMagnetBoosterTime = ES_Save.Load<float>("timeCoinMagnet");
        }
        else
        {
            Debug.Log("Create a timeCoinMagnet");
            coinMagnetBoosterTime = 10f;
            ES_Save.Save(coinMagnetBoosterTime, "timeCoinMagnet");
        }
        if (ES_Save.Exists("coinMagnetBool"))
        {
            Debug.Log("Finded ubreakableBool");
            coinMagnetBool = ES_Save.Load<bool[]>("coinMagnetBool");
        }
        else
        {
            Debug.Log("Create a coinMagnetBool");
            coinMagnetBool[0] = true;
            coinMagnetBool[1] = false;
            coinMagnetBool[2] = false;
            coinMagnetBool[3] = false;
            ES_Save.Save<bool[]>(coinMagnetBool, "coinMagnetBool");
        }

        if (ES_Save.Exists("timeUbreakableBooster"))
        {
            Debug.Log("Finded timeUbreakableBooster");
            ubreakableBoosterTime = ES_Save.Load<float>("timeUbreakableBooster");
        }
        else
        {
            Debug.Log("Create a timeUbreakableBooster");
            ubreakableBoosterTime = 10f;
            ES_Save.Save(ubreakableBoosterTime, "timeUbreakableBooster");
        }
        if (ES_Save.Exists("ubreakableBool"))
        {
            Debug.Log("Finded ubreakableBool");
            ubreakableBoosterBool = ES_Save.Load<bool[]>("ubreakableBool");
        }
        else
        {
            Debug.Log("Create a ubreakableBool");
            ubreakableBoosterBool[0] = true;
            ubreakableBoosterBool[1] = false;
            ubreakableBoosterBool[2] = false;
            ubreakableBoosterBool[3] = false;
            ES_Save.Save<bool[]>(ubreakableBoosterBool, "ubreakableBool");
        }
        if (ES_Save.Exists("timeScoreBooster"))
        {
            Debug.Log("Finded timeScoreBooster");
            timeScoreBooster = ES_Save.Load<float>("timeScoreBooster");
        }
        else
        {
            Debug.Log("Create a timeScoreBooster");
            timeScoreBooster = 10f;
            ES_Save.Save(timeScoreBooster, "timeScoreBooster");
        }
        if (ES_Save.Exists("jumpBoosterTime2"))
        {
            Debug.Log("Finded jumpBoosterTime");
            jumpBoosterTime = ES_Save.Load<float>("jumpBoosterTime2");
        }
        else
        {
            Debug.Log("Create a jumpBoosterTime");
            jumpBoosterTime = 10f;
            ES_Save.Save(jumpBoosterTime, "jumpBoosterTime2");
        }

        if (ES_Save.Exists("doubleScoreBooster"))
        {
            Debug.Log("Finded doubleScoreBooster");
            doubleScoreBooster = ES_Save.Load<bool[]>("doubleScoreBooster");
        }
        else
        {
            Debug.Log("Create a doubleScoreBooster");
            doubleScoreBooster[0] = true;
            doubleScoreBooster[1] = false;
            doubleScoreBooster[2] = false;
            doubleScoreBooster[3] = false;
            ES_Save.Save<bool[]>(doubleScoreBooster, "doubleScoreBooster");
        }
        if (ES_Save.Exists("jumpBoosterBool"))
        {
            Debug.Log("Finded jumpBoosterBool");
            jumpBoosterBool = ES_Save.Load<bool[]>("jumpBoosterBool");
        }
        else
        {
            Debug.Log("Create a jumpBoosterBool");
            jumpBoosterBool[0] = true;
            jumpBoosterBool[1] = false;
            jumpBoosterBool[2] = false;
            jumpBoosterBool[3] = false;
            ES_Save.Save<bool[]>(jumpBoosterBool, "jumpBoosterBool");
        }
        if (doubleScoreBooster[0] && doubleScoreBooster[1] && doubleScoreBooster[2] && doubleScoreBooster[3])
        {
            buyButtonText.text = "Max Upgrade!".ToString();
            buyButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
        if (jumpBoosterBool[0] && jumpBoosterBool[1] && jumpBoosterBool[2] && jumpBoosterBool[3])
        {
            buyButtonJumpText.text = "Max Upgrade!".ToString();
            buyButtonJump.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
        if (ubreakableBoosterBool[0] && ubreakableBoosterBool[1] && ubreakableBoosterBool[2] && ubreakableBoosterBool[3])
        {
            ubreakableBoosterButtonText.text = "Max Upgrade!".ToString();
            ubreakableBoosterButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
        if (coinMagnetBool[0] && coinMagnetBool[1] && coinMagnetBool[2] && coinMagnetBool[3])
        {
            coinMagnetBoosterButtonText.text = "Max Upgrade!".ToString();
            coinMagnetButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }

        if (ES_Save.Exists("selectedSkinNEW"))
        {
            Debug.Log("Finded selected skin!");
            selected = ES_Save.Load<int>("selectedSkinNEW");
        }
        else
        {
            selected = 0;
            ES_Save.Save(selected, "selectedSkinNEW");
        }

        InitShop();
        if (ES_Save.Exists("skinlistNEW")){
            Debug.Log("Finded skinlist!");
            skinList = ES_Save.Load<bool[]>("skinlistNEW");
        }
        else
        {
            skinList[0] = true;
            skinList[1] = false;
            skinList[2] = false;
            skinList[3] = false;
            skinList[4] = false;
            skinList[5] = false;
            skinList[6] = false;
            skinList[7] = false;
            skinList[8] = false;
            skinList[9] = false;
            skinList[10] = false;
            skinList[11] = false;
            skinList[12] = false;
            skinList[13] = false;
            skinList[14] = false;
            skinList[15] = false;
            skinList[16] = false;
            skinList[17] = false;
            skinList[18] = false;
            skinList[19] = false;
            skinList[20] = false;
            skinList[21] = false;
            skinList[22] = false;
            skinList[23] = false;
            skinList[24] = false;
            skinList[25] = false;
            skinList[26] = false;
            skinList[27] = false;
            skinList[28] = false;
            skinList[29] = false;
            skinList[30] = false;
            skinList[31] = false;
            ES_Save.Save<bool[]>(skinList, "skinlistNEW");
        }
        skinPreview.sprite = skinPrevieSprite[selected];
        skinNameText.text = skinName[selected];
        money = ES_Save.Load<float>("money");


        CheckForUpgrade();
        CheckForUpgradeJump();
        CheckForUpgradeUbreakable();
        CheckForUpgradeCoinMagnet();
        CheckForUpgradeAllBooster();

        UpdateCostBooster();
        UpdateJumpCost();
        UpdateUbreakableCost();
        UpdateCoinMagnetCost();
        UpdateAllBoosterCost();

        JumpBoostTimeSave();
        ScoreDoubleTimeSave();
        UbreakableBoostTimeSave();
        CoinMagnetTimeSave();
        AllBoosterTimeSave();

        skinBuySetText.text = "Current";
        buyButtonSkinsPlayer.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
    }

    // Update is called once per frame
    void Update()
    {
        
        //money = ES_Save.Load<float>("money");

        if (Input.GetKey(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }

    }

    public void SaveSkin(){
        skinList = ES_Save.Load<bool[]>("skinlistNEW");
    }


    private void CloudSave()
    {
        if (gps == null)
        {
            gps = GameObject.Find("GooglePlayServices").GetComponent<GooglePlayScript>();
            gps.SaveToCloud();
        }
        else
        {
            gps.SaveToCloud();
        }
    }


    // BUY SKIN PLAYER
    private void InitShop()
    {
        if (skinPanel == null)
            Debug.Log("Nie dodałeś panelu do inspectora");

        //For every children transform under our skin panel
        int i = 0;
        foreach (Transform t in skinPanel)
        {

            int currentIndex = i;

            UnityEngine.UI.Button button = t.GetComponent<UnityEngine.UI.Button>();
            button.onClick.AddListener(() => OnSkinSelect(currentIndex));

            i++;
        }

        i = 0;

    }

    private void SetSkin(int index)
    {
        skinBuySetText.text = "Current";
        selected = index;
        buyButtonSkinsPlayer.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
        ES_Save.Save(selected, "selectedSkinNEW");
        MAIN_MENU.SelectedSkin();
    }

    private void OnSkinSelect(int currentIndex)
    {
        Debug.Log("Selecting skin button: " + currentIndex);

        selectedSkinIndex = currentIndex;

        if (currentIndex == selected)
        {
            skinBuySetText.text = "Current";
            buyButtonSkinsPlayer.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
        }
        else 
        if (skinList[currentIndex])
        {
            skinBuySetText.text = "Select";
            buyButtonSkinsPlayer.GetComponent<Image>().color = new Color32(30, 144, 255, 255);
        }
        else
        {
            skinBuySetText.text = "Buy: " + skinCost[currentIndex].ToString();
            buyButtonSkinsPlayer.GetComponent<Image>().color = new Color32(255, 107, 129, 255);
        }
        skinNameText.text = skinName[currentIndex];
        skinPreview.sprite = skinPrevieSprite[currentIndex];
        
    }

    public void OnBuySkin()
    {
        Debug.Log("Buy");
        money = ES_Save.Load<float>("money");
        if (skinList[selectedSkinIndex])
        {
            //set the skin
            SetSkin(selectedSkinIndex);
        }
        else
        {
            //buy skin
            if(money >= skinCost[selectedSkinIndex])
            {
                skinList[selectedSkinIndex] = true;
                SavedSkin();
                money -= skinCost[selectedSkinIndex];
                ES_Save.Save<float>(money, "money");
                SetSkin(selectedSkinIndex);
                MAIN_MENU.RoundMoney();
                SaveSkin();
                CloudSave();
            }
            else
            {
                Debug.Log("Not enought money");
                StartCoroutine(NotMoney());
            }
        }

    }


    IEnumerator NotMoney()
    {
        GameObject.Find("IAP").GetComponent<IAPGoogle>().RefreshMoneyNEMLocalized();
        notmoneyText.SetActive(true);
        notmoneyText.transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(notmoneyText, new Vector3(1, 1, 1), 0.6f).setEase(LeanTweenType.easeOutBack);
        yield return new WaitForSeconds(3);
    }

    public void SavedSkin()
    {
        ES_Save.Save<bool[]>(skinList, "skinlistNEW");
    }
    // END BUY SKIN PLAYER




    //BUY BOOSTERS
    public void BuyUpgrade()
    {
        money = ES_Save.Load<float>("money");
        if (doubleScoreBooster[0] == true)
        {
            if(money > scoreDoubleCost[0] && doubleScoreBooster[1] == false)
            {
                money -= scoreDoubleCost[0];
                ES_Save.Save<float>(money, "money");
                doubleScoreBooster[1] = true;
                ES_Save.Save(doubleScoreBooster, "doubleScoreBooster");
                MAIN_MENU.RoundMoney();
                ScoreDoubleTimeSave();
                LoadData();
                CheckForUpgrade();
                UpdateCostBooster();
                CloudSave();
            }
            else
            {
                if(money > scoreDoubleCost[1] && doubleScoreBooster[2] == false)
                {
                    money -= scoreDoubleCost[1];
                    ES_Save.Save<float>(money, "money");
                    MAIN_MENU.RoundMoney();
                    doubleScoreBooster[2] = true;
                    ES_Save.Save(doubleScoreBooster, "doubleScoreBooster");
                    ScoreDoubleTimeSave();
                    LoadData();
                    CheckForUpgrade();
                    UpdateCostBooster();
                    CloudSave();
                }
                else
                {
                    if (money > scoreDoubleCost[2] && doubleScoreBooster[3] == false)
                    {
                        money -= scoreDoubleCost[2];
                        ES_Save.Save<float>(money, "money");
                        MAIN_MENU.RoundMoney();
                        doubleScoreBooster[3] = true;
                        ES_Save.Save(doubleScoreBooster, "doubleScoreBooster");
                        buyButtonText.text = "Max Upgrade!";
                        buyButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
                        ScoreDoubleTimeSave();
                        LoadData();
                        CheckForUpgrade();
                        CloudSave();
                    }
                    else
                    {
                        if(doubleScoreBooster[0] && doubleScoreBooster[1] && doubleScoreBooster[2] && doubleScoreBooster[3])
                        {
                            buyButtonText.text = "Max Upgrade!";
                            buyButton.GetComponent<UnityEngine.UI.Button>().interactable = false;  
                        }
                        else
                        {
                            StartCoroutine(NotMoney());
                        }
                    }
                }
            }
        }
    }

    private void UpdateCostBooster()
    {
        if(doubleScoreBooster[0])
        {
            buyButtonText.text = "Buy: " + scoreDoubleCost[0].ToString(); 
        }
        
        {
            if (doubleScoreBooster[1] == true)
            {
                buyButtonText.text = "Buy: " + scoreDoubleCost[1].ToString();
            }
            
            {
                if (doubleScoreBooster[2] == true)
                {
                    buyButtonText.text = "Buy: " + scoreDoubleCost[2].ToString();
                }
                if(doubleScoreBooster[0] && doubleScoreBooster[1] && doubleScoreBooster[2] && doubleScoreBooster[3])
                {
                    buyButtonText.text = "Max Upgrade!";
                    buyButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
                }
            }
        }

        
    }

    public void ScoreDoubleTimeSave()
    {
        if (doubleScoreBooster[0] == true)
        {
            timeScoreBooster = 10f;
            ES_Save.Save(timeScoreBooster, "timeScoreBooster");
            LoadData();
        }
            if (doubleScoreBooster[1] == true)
        {
            timeScoreBooster = 25f;
            ES_Save.Save(timeScoreBooster, "timeScoreBooster");
            LoadData();
        }
        
        {
            if (doubleScoreBooster[2] == true)
            {
                timeScoreBooster = 40f;
                ES_Save.Save(timeScoreBooster, "timeScoreBooster");
                LoadData();
            }
            
            {
                if (doubleScoreBooster[3] == true)
                {
                    timeScoreBooster = 60f;
                    ES_Save.Save(timeScoreBooster, "timeScoreBooster");
                    LoadData();
                }
            }
        }
    }           // DOUBLE SCORE BOOSTER

    private void LoadData()
    {
        doubleScoreBooster = ES_Save.Load<bool[]>("doubleScoreBooster");
        timeScoreBooster = ES_Save.Load<float>("timeScoreBooster");

        jumpBoosterBool = ES_Save.Load<bool[]>("jumpBoosterBool");
        jumpBoosterTime = ES_Save.Load<float>("jumpBoosterTime2");

        ubreakableBoosterBool = ES_Save.Load<bool[]>("ubreakableBool");
        ubreakableBoosterTime = ES_Save.Load<float>("timeUbreakableBooster");

        allBoosterFasterBool = ES_Save.Load<bool[]>("allBoosterFasterBool");
        allBoosterFasterTimeDown = ES_Save.Load<float>("allBoosterFasterTimeDown");
        allBoosterFasterTimeUp = ES_Save.Load<float>("allBoosterFasterTimeUp");
    }

    public void CheckForUpgrade()
    {
        if(doubleScoreBooster[0] == true)
        {
            lvlScoreDouble[1].gameObject.GetComponent<Image>().color = new Color32(255, 71, 87, 255);
            lvlScoreDouble[2].gameObject.GetComponent<Image>().color = new Color32(255, 71, 87, 255);
            lvlScoreDouble[3].gameObject.GetComponent<Image>().color = new Color32(255, 71, 87, 255);
            lvlScoreDouble[0].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
        }
        {
            if (doubleScoreBooster[1] == true)
            {
                lvlScoreDouble[2].gameObject.GetComponent<Image>().color = new Color32(255, 71, 87, 255);
                lvlScoreDouble[1].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
                lvlScoreDouble[0].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
            }
            {
                if (doubleScoreBooster[2] == true)
                {
                    lvlScoreDouble[3].gameObject.GetComponent<Image>().color = new Color32(255, 71, 87, 255);
                    lvlScoreDouble[2].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
                    lvlScoreDouble[1].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
                    lvlScoreDouble[0].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
                }
                {
                    if (doubleScoreBooster[3] == true)
                    {
                        lvlScoreDouble[3].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
                        lvlScoreDouble[2].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
                        lvlScoreDouble[1].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
                        lvlScoreDouble[0].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
                    }
                }
            }
        }
    }






    public void BuyUpgradeJump()
    {
        money = ES_Save.Load<float>("money");
        if (jumpBoosterBool[0] == true)
        {
            if (money > jumpBooster[0] && jumpBoosterBool[1] == false)
            {
                money -= jumpBooster[0];
                ES_Save.Save<float>(money, "money");
                MAIN_MENU.RoundMoney();
                jumpBoosterBool[1] = true;
                ES_Save.Save(jumpBoosterBool, "jumpBoosterBool");
                JumpBoostTimeSave();
                LoadData();
                CheckForUpgradeJump();
                UpdateJumpCost();
                CloudSave();
            }
            else
            {
                if (money > jumpBooster[1] && jumpBoosterBool[2] == false)
                {
                    money -= jumpBooster[1];
                    ES_Save.Save<float>(money, "money");
                    MAIN_MENU.RoundMoney();
                    jumpBoosterBool[2] = true;
                    ES_Save.Save(jumpBoosterBool, "jumpBoosterBool");
                    JumpBoostTimeSave();
                    LoadData();
                    CheckForUpgradeJump();
                    UpdateJumpCost();
                    CloudSave();
                }
                else
                {
                    if (money > jumpBooster[2] && jumpBoosterBool[3] == false)
                    {
                        money -= jumpBooster[2];
                        ES_Save.Save<float>(money, "money");
                        MAIN_MENU.RoundMoney();
                        jumpBoosterBool[3] = true;
                        ES_Save.Save(jumpBoosterBool, "jumpBoosterBool");
                        buyButtonJumpText.text = "Max Upgrade!";
                        buyButtonJump.GetComponent<UnityEngine.UI.Button>().interactable = false;
                        JumpBoostTimeSave();
                        LoadData();
                        CheckForUpgradeJump();
                        CloudSave();
                    }
                    else
                    {
                        if (jumpBoosterBool[0] && jumpBoosterBool[1] && jumpBoosterBool[2] && jumpBoosterBool[3])
                        {
                            buyButtonJumpText.text = "Max Upgrade!";
                            buyButtonJump.GetComponent<UnityEngine.UI.Button>().interactable = false;
                        }
                        else
                        {
                            StartCoroutine(NotMoney());
                        }
                    }
                }
            }
        }
    }

    public void UpdateJumpCost()
    {

        if (jumpBoosterBool[0])
        {
            buyButtonJumpText.text = "Buy: " + jumpBooster[0].ToString();
        }
        
        {
            if (jumpBoosterBool[1] == true)
            {
                buyButtonJumpText.text = "Buy: " + jumpBooster[1].ToString();
            }
            
            {
                if (jumpBoosterBool[2] == true)
                {
                    buyButtonJumpText.text = "Buy: " + jumpBooster[2].ToString();
                }

                if (jumpBoosterBool[0] && jumpBoosterBool[1] && jumpBoosterBool[2] && jumpBoosterBool[3])
                {
                    buyButtonJumpText.text = "Max Upgrade!".ToString();
                    buyButtonJump.GetComponent<UnityEngine.UI.Button>().interactable = false;
                }
            }
        }
    }               // JUMP BOOST

    public void JumpBoostTimeSave()
    {
        if (jumpBoosterBool[0] == true)
        {
            jumpBoosterTime = 10f;
            ES_Save.Save(jumpBoosterTime, "jumpBoosterTime2");
            LoadData();
        }

        if (jumpBoosterBool[1] == true)
        {
            jumpBoosterTime = 25f;
            ES_Save.Save(jumpBoosterTime, "jumpBoosterTime2");
            LoadData();
        }
        
        {
            if (jumpBoosterBool[2] == true)
            {
                jumpBoosterTime = 40f;
                ES_Save.Save(jumpBoosterTime, "jumpBoosterTime2");
                LoadData();
            }
            
            {
                if (jumpBoosterBool[3] == true)
                {
                    jumpBoosterTime = 60f;
                    ES_Save.Save(jumpBoosterTime, "jumpBoosterTime2");
                    LoadData();
                }
            }
        }
    }

    public void CheckForUpgradeJump()
    {
        if (jumpBoosterBool[0] == true)
        {
            lvlJumpBoost[1].gameObject.GetComponent<Image>().color = new Color32(255, 71, 87, 255);
            lvlJumpBoost[2].gameObject.GetComponent<Image>().color = new Color32(255, 71, 87, 255);
            lvlJumpBoost[3].gameObject.GetComponent<Image>().color = new Color32(255, 71, 87, 255);
            lvlJumpBoost[0].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
        }
        {
            if (jumpBoosterBool[1] == true)
            {
                lvlJumpBoost[2].gameObject.GetComponent<Image>().color = new Color32(255, 71, 87, 255);
                lvlJumpBoost[1].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
                lvlJumpBoost[0].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
            }
            {
                if (jumpBoosterBool[2] == true)
                {
                    lvlJumpBoost[3].gameObject.GetComponent<Image>().color = new Color32(255, 71, 87, 255);
                    lvlJumpBoost[2].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
                    lvlJumpBoost[1].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
                    lvlJumpBoost[0].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
                }
                {
                    if (jumpBoosterBool[3] == true)
                    {
                        lvlJumpBoost[3].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
                        lvlJumpBoost[2].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
                        lvlJumpBoost[1].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
                        lvlJumpBoost[0].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
                    }
                }
            }
        }
    }





    public void BuyUnbreakableBooster()
    {
        money = ES_Save.Load<float>("money");
        if (ubreakableBoosterBool[0] == true)
        {
            if (money > ubreakableBoosterCost[0] && ubreakableBoosterBool[1] == false)
            {
                money -= ubreakableBoosterCost[0];
                ES_Save.Save<float>(money, "money");
                MAIN_MENU.RoundMoney();
                ubreakableBoosterBool[1] = true;
                ES_Save.Save(ubreakableBoosterBool, "ubreakableBool");
                UbreakableBoostTimeSave();
                LoadData();
                CheckForUpgradeUbreakable();
                UpdateUbreakableCost();
                CloudSave();
            }
            else
            {
                if (money > ubreakableBoosterCost[1] && ubreakableBoosterBool[2] == false)
                {
                    money -= ubreakableBoosterCost[1];
                    ES_Save.Save<float>(money, "money");
                    MAIN_MENU.RoundMoney();
                    ubreakableBoosterBool[2] = true;
                    ES_Save.Save(ubreakableBoosterBool, "ubreakableBool");
                    UbreakableBoostTimeSave();
                    LoadData();
                    CheckForUpgradeUbreakable();
                    UpdateUbreakableCost();
                    CloudSave();
                }
                else
                {
                    if (money > ubreakableBoosterCost[2] && ubreakableBoosterBool[3] == false)
                    {
                        money -= ubreakableBoosterCost[2];
                        ES_Save.Save<float>(money, "money");
                        MAIN_MENU.RoundMoney();
                        ubreakableBoosterBool[3] = true;
                        ES_Save.Save(ubreakableBoosterBool, "ubreakableBool");
                        ubreakableBoosterButtonText.text = "Max Upgrade!";
                        ubreakableBoosterButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
                        UbreakableBoostTimeSave();
                        LoadData();
                        CheckForUpgradeUbreakable();
                        CloudSave();
                    }
                    else
                    {
                        if (ubreakableBoosterBool[0] && ubreakableBoosterBool[1] && ubreakableBoosterBool[2] && ubreakableBoosterBool[3])
                        {
                            ubreakableBoosterButtonText.text = "Max Upgrade!";
                            ubreakableBoosterButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
                        }
                        else
                        {
                            StartCoroutine(NotMoney());
                        }
                    }
                }
            }
        }
    }

    public void UpdateUbreakableCost()
    {
        if (ubreakableBoosterBool[0])
        {
            ubreakableBoosterButtonText.text = "Buy: " + ubreakableBoosterCost[0].ToString();
        }

        {
            if (ubreakableBoosterBool[1] == true)
            {
                ubreakableBoosterButtonText.text = "Buy: " + ubreakableBoosterCost[1].ToString();
            }

            {
                if (ubreakableBoosterBool[2] == true)
                {
                    ubreakableBoosterButtonText.text = "Buy: " + ubreakableBoosterCost[2].ToString();
                }

                if (ubreakableBoosterBool[0] && ubreakableBoosterBool[1] && ubreakableBoosterBool[2] && ubreakableBoosterBool[3])
                {
                    ubreakableBoosterButtonText.text = "Max Upgrade!".ToString();
                    ubreakableBoosterButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
                }
            }
        }
    }           // Ubreakable Boost

    public void UbreakableBoostTimeSave()
    {
        if (ubreakableBoosterBool[0] == true)
        {
            ubreakableBoosterTime = 10f;
            ES_Save.Save(ubreakableBoosterTime, "timeUbreakableBooster");
            LoadData();
        }

        if (ubreakableBoosterBool[1] == true)
        {
            ubreakableBoosterTime = 25f;
            ES_Save.Save(ubreakableBoosterTime, "timeUbreakableBooster");
            LoadData();
        }

        {
            if (ubreakableBoosterBool[2] == true)
            {
                ubreakableBoosterTime = 50f;
                ES_Save.Save(ubreakableBoosterTime, "timeUbreakableBooster");
                LoadData();
            }

            {
                if (ubreakableBoosterBool[3] == true)
                {
                    ubreakableBoosterTime = 90f;
                    ES_Save.Save(ubreakableBoosterTime, "timeUbreakableBooster");
                    LoadData();
                }
            }
        }
    }

    public void CheckForUpgradeUbreakable()
    {
        if (ubreakableBoosterBool[0] == true)
        {
            lvlUbreakableBooster[1].gameObject.GetComponent<Image>().color = new Color32(255, 71, 87, 255);
            lvlUbreakableBooster[2].gameObject.GetComponent<Image>().color = new Color32(255, 71, 87, 255);
            lvlUbreakableBooster[3].gameObject.GetComponent<Image>().color = new Color32(255, 71, 87, 255);
            lvlUbreakableBooster[0].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
        }
        {
            if (ubreakableBoosterBool[1] == true)
            {
                lvlUbreakableBooster[2].gameObject.GetComponent<Image>().color = new Color32(255, 71, 87, 255);
                lvlUbreakableBooster[1].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
                lvlUbreakableBooster[0].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
            }
            {
                if (ubreakableBoosterBool[2] == true)
                {
                    lvlUbreakableBooster[3].gameObject.GetComponent<Image>().color = new Color32(255, 71, 87, 255);
                    lvlUbreakableBooster[2].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
                    lvlUbreakableBooster[1].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
                    lvlUbreakableBooster[0].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
                }
                {
                    if (ubreakableBoosterBool[3] == true)
                    {
                        lvlUbreakableBooster[3].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
                        lvlUbreakableBooster[2].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
                        lvlUbreakableBooster[1].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
                        lvlUbreakableBooster[0].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
                    }
                }
            }
        }
    }



    public void BuyCoinMagnet()
    {
        money = ES_Save.Load<float>("money");
        if (coinMagnetBool[0] == true)
        {
            if (money > coinMagnetFasterCost[0] && coinMagnetBool[1] == false)
            {
                money -= coinMagnetFasterCost[0];
                ES_Save.Save<float>(money, "money");
                MAIN_MENU.RoundMoney();
                coinMagnetBool[1] = true;
                ES_Save.Save(coinMagnetBool, "coinMagnetBool");
                CoinMagnetTimeSave();
                LoadData();
                CheckForUpgradeCoinMagnet();
                UpdateCoinMagnetCost();
                CloudSave();
            }
            else
            {
                if (money > coinMagnetFasterCost[1] && coinMagnetBool[2] == false)
                {
                    money -= coinMagnetFasterCost[1];
                    ES_Save.Save<float>(money, "money");
                    MAIN_MENU.RoundMoney();
                    coinMagnetBool[2] = true;
                    ES_Save.Save(coinMagnetBool, "coinMagnetBool");
                    CoinMagnetTimeSave();
                    LoadData();
                    CheckForUpgradeCoinMagnet();
                    UpdateCoinMagnetCost();
                    CloudSave();
                }
                else
                {
                    if (money > coinMagnetFasterCost[2] && coinMagnetBool[3] == false)
                    {
                        money -= coinMagnetFasterCost[2];
                        ES_Save.Save<float>(money, "money");
                        MAIN_MENU.RoundMoney();
                        coinMagnetBool[3] = true;
                        ES_Save.Save(coinMagnetBool, "coinMagnetBool");
                        coinMagnetBoosterButtonText.text = "Max Upgrade!";
                        coinMagnetButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
                        CoinMagnetTimeSave();
                        LoadData();
                        CheckForUpgradeCoinMagnet();
                        CloudSave();
                    }
                    else
                    {
                        if (coinMagnetBool[0] && coinMagnetBool[1] && coinMagnetBool[2] && coinMagnetBool[3])
                        {
                            coinMagnetBoosterButtonText.text = "Max Upgrade!";
                            coinMagnetButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
                        }
                        else
                        {
                            StartCoroutine(NotMoney());
                        }
                    }
                }
            }
        }
    }

    public void UpdateCoinMagnetCost()
    {
        if (coinMagnetBool[0])
        {
            coinMagnetBoosterButtonText.text = "Buy: " + coinMagnetFasterCost[0].ToString();
        }

        {
            if (coinMagnetBool[1] == true)
            {
                coinMagnetBoosterButtonText.text = "Buy: " + coinMagnetFasterCost[1].ToString();
            }

            {
                if (coinMagnetBool[2] == true)
                {
                    coinMagnetBoosterButtonText.text = "Buy: " + coinMagnetFasterCost[2].ToString();
                }

                if (coinMagnetBool[0] && coinMagnetBool[1] && coinMagnetBool[2] && coinMagnetBool[3])
                {
                    coinMagnetBoosterButtonText.text = "Max Upgrade!".ToString();
                    coinMagnetButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
                }
            }
        }
    }   // Coin Magnet Booster

    public void CoinMagnetTimeSave()
    {
        if (coinMagnetBool[0] == true)
        {
            coinMagnetBoosterTime = 10f;
            ES_Save.Save(coinMagnetBoosterTime, "timeCoinMagnet");
            LoadData();
        }

        if (coinMagnetBool[1] == true)
        {
            coinMagnetBoosterTime = 25f;
            ES_Save.Save(coinMagnetBoosterTime, "timeCoinMagnet");
            LoadData();
        }

        {
            if (coinMagnetBool[2] == true)
            {
                coinMagnetBoosterTime = 50f;
                ES_Save.Save(coinMagnetBoosterTime, "timeCoinMagnet");
                LoadData();
            }

            {
                if (coinMagnetBool[3] == true)
                {
                    coinMagnetBoosterTime = 90f;
                    ES_Save.Save(coinMagnetBoosterTime, "timeCoinMagnet");
                    LoadData();
                }
            }
        }
    }

    public void CheckForUpgradeCoinMagnet()
    {
        if (coinMagnetBool[0] == true)
        {
            lvlcoinMagnetBooster[1].gameObject.GetComponent<Image>().color = new Color32(255, 71, 87, 255);
            lvlcoinMagnetBooster[2].gameObject.GetComponent<Image>().color = new Color32(255, 71, 87, 255);
            lvlcoinMagnetBooster[3].gameObject.GetComponent<Image>().color = new Color32(255, 71, 87, 255);
            lvlcoinMagnetBooster[0].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
        }
        {
            if (coinMagnetBool[1] == true)
            {
                lvlcoinMagnetBooster[2].gameObject.GetComponent<Image>().color = new Color32(255, 71, 87, 255);
                lvlcoinMagnetBooster[1].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
                lvlcoinMagnetBooster[0].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
            }
            {
                if (coinMagnetBool[2] == true)
                {
                    lvlcoinMagnetBooster[3].gameObject.GetComponent<Image>().color = new Color32(255, 71, 87, 255);
                    lvlcoinMagnetBooster[2].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
                    lvlcoinMagnetBooster[1].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
                    lvlcoinMagnetBooster[0].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
                }
                {
                    if (coinMagnetBool[3] == true)
                    {
                        lvlcoinMagnetBooster[3].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
                        lvlcoinMagnetBooster[2].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
                        lvlcoinMagnetBooster[1].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
                        lvlcoinMagnetBooster[0].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
                    }
                }
            }
        }
    }




    public void BuyAllBoosterFaster()
    {
        money = ES_Save.Load<float>("money");
        if (allBoosterFasterBool[0] == true)
        {
            if (money > allBoosterFasterCost[0] && allBoosterFasterBool[1] == false)
            {
                money -= allBoosterFasterCost[0];
                ES_Save.Save<float>(money, "money");
                MAIN_MENU.RoundMoney();
                allBoosterFasterBool[1] = true;
                ES_Save.Save(allBoosterFasterBool, "allBoosterFasterBool");
                AllBoosterTimeSave();
                LoadData();
                CheckForUpgradeAllBooster();
                UpdateAllBoosterCost();
                CloudSave();
            }
            else
            {
                if (money > allBoosterFasterCost[1] && allBoosterFasterBool[2] == false)
                {
                    money -= allBoosterFasterCost[1];
                    ES_Save.Save<float>(money, "money");
                    MAIN_MENU.RoundMoney();
                    allBoosterFasterBool[2] = true;
                    ES_Save.Save(allBoosterFasterBool, "allBoosterFasterBool");
                    AllBoosterTimeSave();
                    LoadData();
                    CheckForUpgradeAllBooster();
                    UpdateAllBoosterCost();
                    CloudSave();
                }
                else
                {
                    if (money > allBoosterFasterCost[2] && allBoosterFasterBool[3] == false)
                    {
                        money -= allBoosterFasterCost[2];
                        ES_Save.Save<float>(money, "money");
                        MAIN_MENU.RoundMoney();
                        allBoosterFasterBool[3] = true;
                        ES_Save.Save(allBoosterFasterBool, "allBoosterFasterBool");
                        allBoosterFasterButtonText.text = "Max Upgrade!";
                        allBoosterFasterButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
                        AllBoosterTimeSave();
                        LoadData();
                        CheckForUpgradeAllBooster();
                        CloudSave();
                    }
                    else
                    {
                        if (allBoosterFasterBool[0] && allBoosterFasterBool[1] && allBoosterFasterBool[2] && allBoosterFasterBool[3])
                        {
                            allBoosterFasterButtonText.text = "Max Upgrade!";
                            allBoosterFasterButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
                        }
                        else
                        {
                            StartCoroutine(NotMoney());
                        }
                    }
                }
            }
        }
    }

    public void UpdateAllBoosterCost()
    {
        if (allBoosterFasterBool[0])
        {
            allBoosterFasterButtonText.text = "Buy: " + allBoosterFasterCost[0].ToString();
        }

        {
            if (allBoosterFasterBool[1] == true)
            {
                allBoosterFasterButtonText.text = "Buy: " + allBoosterFasterCost[1].ToString();
            }

            {
                if (allBoosterFasterBool[2] == true)
                {
                    allBoosterFasterButtonText.text = "Buy: " + allBoosterFasterCost[2].ToString();
                }

                if (allBoosterFasterBool[0] && allBoosterFasterBool[1] && allBoosterFasterBool[2] && allBoosterFasterBool[3])
                {
                    allBoosterFasterButtonText.text = "Max Upgrade!".ToString();
                    allBoosterFasterButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
                }
            }
        }
    }

    public void AllBoosterTimeSave()
    {
        if (allBoosterFasterBool[1] == true)
        {
            allBoosterFasterTimeDown = 60f;
            ES_Save.Save(allBoosterFasterTimeDown, "allBoosterFasterTimeDown");
            allBoosterFasterTimeUp = 70f;
            ES_Save.Save(allBoosterFasterTimeUp, "allBoosterFasterTimeUp");
        }

        {
            if (allBoosterFasterBool[2] == true)
            {
                allBoosterFasterTimeDown = 40f;
                ES_Save.Save(allBoosterFasterTimeDown, "allBoosterFasterTimeDown");
                allBoosterFasterTimeUp = 55f;
                ES_Save.Save(allBoosterFasterTimeUp, "allBoosterFasterTimeUp");
            }

            {
                if (allBoosterFasterBool[3] == true)
                {
                    allBoosterFasterTimeDown = 20f;
                    ES_Save.Save(allBoosterFasterTimeDown, "allBoosterFasterTimeDown");
                    allBoosterFasterTimeUp = 35f;
                    ES_Save.Save(allBoosterFasterTimeUp, "allBoosterFasterTimeUp");
                }
            }
        }
    }

    public void CheckForUpgradeAllBooster()
    {
        if (allBoosterFasterBool[0] == true)
        {
            lvlAllBoosterFaster[1].gameObject.GetComponent<Image>().color = new Color32(255, 71, 87, 255);
            lvlAllBoosterFaster[2].gameObject.GetComponent<Image>().color = new Color32(255, 71, 87, 255);
            lvlAllBoosterFaster[3].gameObject.GetComponent<Image>().color = new Color32(255, 71, 87, 255);
            lvlAllBoosterFaster[0].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
        }
        {
            if (allBoosterFasterBool[1] == true)
            {
                lvlAllBoosterFaster[2].gameObject.GetComponent<Image>().color = new Color32(255, 71, 87, 255);
                lvlAllBoosterFaster[1].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
                lvlAllBoosterFaster[0].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
            }
            {
                if (allBoosterFasterBool[2] == true)
                {
                    lvlAllBoosterFaster[3].gameObject.GetComponent<Image>().color = new Color32(255, 71, 87, 255);
                    lvlAllBoosterFaster[2].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
                    lvlAllBoosterFaster[1].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
                    lvlAllBoosterFaster[0].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
                }
                {
                    if (allBoosterFasterBool[3] == true)
                    {
                        lvlAllBoosterFaster[3].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
                        lvlAllBoosterFaster[2].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
                        lvlAllBoosterFaster[1].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
                        lvlAllBoosterFaster[0].gameObject.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
                    }
                }
            }
        }
    }

    //END BUY BOOSTERS

    public void Return()
    {
        gameObject.SetActive(false);
    }

}
