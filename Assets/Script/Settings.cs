using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QuantumTek.EncryptedSave;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public Toggle toggleSwipe;
    public bool swipeUp;

    public GameObject aboutPanel;

    private void Awake()
    {
        
        toggleSwipe = GameObject.Find("SwipeTapToggle").GetComponent<Toggle>();
        if (ES_Save.Exists("swipeup"))
        {
            return;
        }
        else
        {
            swipeUp = false;
            ES_Save.Save<bool>(swipeUp, "swipeup");
        }
    }


    private void Update()
    {
        swipeUp = ES_Save.Load<bool>("swipeup");

        if (swipeUp)
        {
            toggleSwipe.isOn = true;
        }
        else
        {
            toggleSwipe.isOn = false;
        }

        if(Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                gameObject.SetActive(false);
            }
        }

    }

    public void SwipeUp(bool swipueUpToggle)
    {
        ES_Save.Save<bool>(swipueUpToggle, "swipeup");

    }

    public void About()
    {
        aboutPanel.SetActive(true);
    }

    public void Tutorial()
    {
        SceneManager.LoadScene(2);
    }
       

}
