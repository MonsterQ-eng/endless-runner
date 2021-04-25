using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QuantumTek.EncryptedSave;

public class PremiumShop : MonoBehaviour
{
    public GameObject enemySkinPanel;
    public GameObject platfromSkinPanel;
    public GameObject premiumShopPanel;
    public Admob admob;
    public GameObject notmoneyText;
    

    public Mainmenu mainMenu;
    private void Start()
    {
        admob = GameObject.Find("AdmobManager").GetComponent<Admob>();
        this.gameObject.SetActive(false);
        enemySkinPanel.gameObject.SetActive(false);
       // platfromSkinPanel.gameObject.SetActive(false);
        premiumShopPanel.gameObject.SetActive(false);

        
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            this.gameObject.SetActive(false);
            enemySkinPanel.gameObject.SetActive(false);
           // platfromSkinPanel.gameObject.SetActive(false);
            premiumShopPanel.gameObject.SetActive(false);
        }
    }



    


    public void EnemySkinLoad()
    {
        enemySkinPanel.gameObject.SetActive(true);
        //platfromSkinPanel.gameObject.SetActive(false);
        premiumShopPanel.gameObject.SetActive(false);
    }
    public void PlatfromSkinLoad()
    {
        enemySkinPanel.gameObject.SetActive(false);
        //platfromSkinPanel.gameObject.SetActive(true);
        premiumShopPanel.gameObject.SetActive(false);
    }
    public void PremiumShopLoad()
    {
        enemySkinPanel.gameObject.SetActive(false);
       // platfromSkinPanel.gameObject.SetActive(false);
        premiumShopPanel.gameObject.SetActive(true);
    }

    public void Return()
    {
        this.gameObject.SetActive(false);
        enemySkinPanel.gameObject.SetActive(false);
      //  platfromSkinPanel.gameObject.SetActive(false);
        premiumShopPanel.gameObject.SetActive(false);
        
    }

}
