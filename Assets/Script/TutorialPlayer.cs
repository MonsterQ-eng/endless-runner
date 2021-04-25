using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuantumTek.EncryptedSave;
using DigitalRubyShared;
using UnityEngine.UI;
using System;
using GooglePlayGames;

public class TutorialPlayer : MonoBehaviour
{

    public GameObject canvasTuto;
    public GameObject textPanel;


    private void Start()
    {
        canvasTuto.transform.localScale = new Vector3(0, 0, 0);
        textPanel.transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(canvasTuto, new Vector3(1, 1, 1), 0.3f);
        LeanTween.scale(textPanel, new Vector3(1, 1, 1), 0.3f);
    }


}
