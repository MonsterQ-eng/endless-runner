using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoubleCoinButton : MonoBehaviour
{

    public Admob adMob;
    private UnityEngine.UI.Button button;
    public Image buttonImage;


    private void Start()
    {
        button = gameObject.GetComponent<UnityEngine.UI.Button>();
        adMob = GameObject.Find("AdmobManager").GetComponent<Admob>();
        button.onClick.AddListener(() => adMob.ShowRewardAD());
        if (adMob.ReadyAdR())
        {
            button.interactable = true;
            
        }
        else
        {
            button.interactable = false;
            buttonImage.color = new Color32(255, 255, 255, 155);
        }
    }

}
