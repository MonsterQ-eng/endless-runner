using QuantumTek.EncryptedSave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundShop : MonoBehaviour
{

    public Transform backgroundPanel;
    public Text backgroundBuySetText;
    private int[] backgroundCost = new int[] { 1, 1, 1, 1 };
    private int backgroundSkinIndex;
    [SerializeField] bool[] backgroundList = new bool[32];
    public Image backgroundPreview;
    public Sprite[] backgroundPrevieSprite;
    private float money;
    public int selectedBackground;
    public GameObject buyButtonSkinsBackground;
    public Mainmenu mainMenu;

    void Start()
    {
        
        InitShop();
        if (ES_Save.Exists("skinsBackground"))
        {
            Debug.Log("Finded backgroundlist!");
            backgroundList = ES_Save.Load<bool[]>("skinsBackground");
        }
        else
        {
            backgroundList[0] = true;
            backgroundList[1] = true;
            backgroundList[2] = false;
            backgroundList[3] = false;
            backgroundList[4] = false;
            backgroundList[5] = false;
            backgroundList[6] = false;
            backgroundList[7] = false;
            backgroundList[8] = false;
            backgroundList[9] = false;
            backgroundList[10] = false;
            backgroundList[11] = false;
            backgroundList[12] = false;
            backgroundList[13] = false;
            backgroundList[14] = false;
            backgroundList[15] = false;
            backgroundList[16] = false;
            backgroundList[17] = false;
            backgroundList[18] = false;
            backgroundList[19] = false;
            backgroundList[20] = false;
            backgroundList[21] = false;
            backgroundList[22] = false;
            backgroundList[23] = false;
            backgroundList[24] = false;
            backgroundList[25] = false;
            backgroundList[26] = false;
            backgroundList[27] = false;
            backgroundList[28] = false;
            backgroundList[29] = false;
            backgroundList[30] = false;
            backgroundList[31] = false;
            ES_Save.Save<bool[]>(backgroundList, "skinsBackground");
        }
        backgroundPreview.sprite = backgroundPrevieSprite[selectedBackground];
        money = ES_Save.Load<float>("money");

        backgroundBuySetText.text = "Current";
        buyButtonSkinsBackground.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
    }

    private void InitShop()
    {
        if (backgroundPanel == null)
            Debug.Log("Nie dodałeś panelu do inspectora");

        //For every children transform under our skin panel
        int i = 0;
        foreach (Transform t in backgroundPanel)
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
        backgroundBuySetText.text = "Current";
        selectedBackground = index;
        buyButtonSkinsBackground.GetComponent<Image>().color = new Color32(0, 255, 0, 255);
        ES_Save.Save(selectedBackground, "selectedBackground");
        mainMenu.SelectedSkinBackground();
    }

    private void OnSkinSelect(int currentIndex)
    {
        Debug.Log("Selecting skin button: " + currentIndex);

        backgroundSkinIndex = currentIndex;

        if (currentIndex == selectedBackground)
        {
            backgroundBuySetText.text = "Current";
            buyButtonSkinsBackground.GetComponent<Image>().color = new Color32(46, 213, 115, 255);
        }
        else
        if (backgroundList[currentIndex])
        {
            backgroundBuySetText.text = "Select";
            buyButtonSkinsBackground.GetComponent<Image>().color = new Color32(30, 144, 255, 255);
        }
        else
        {
            backgroundBuySetText.text = "Buy: " + backgroundCost[currentIndex].ToString();
            buyButtonSkinsBackground.GetComponent<Image>().color = new Color32(255, 107, 129, 255);
        }
        backgroundPreview.sprite = backgroundPrevieSprite[currentIndex];

    }

    public void OnBuySkin()
    {
        Debug.Log("Buy");
        money = ES_Save.Load<float>("money");
        if (backgroundList[backgroundSkinIndex])
        {
            //set the skin
            SetSkin(backgroundSkinIndex);
        }
        else
        {
            //buy skin
            if (money >= backgroundCost[backgroundSkinIndex])
            {
                backgroundList[backgroundSkinIndex] = true;

                money -= backgroundCost[backgroundSkinIndex];
                ES_Save.Save<float>(money, "money");
                SetSkin(backgroundSkinIndex);
                mainMenu.RoundMoney();


            }
            else
            {
                Debug.Log("Not enought money");

            }
        }

    }
}
