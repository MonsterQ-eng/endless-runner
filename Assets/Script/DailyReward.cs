using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QuantumTek.EncryptedSave;
using Unity.Notifications.Android;

public class DailyReward : MonoBehaviour
{
    //UI
    public Text timeLabel; //only use if your timer uses a label
    public UnityEngine.UI.Button timerButton; //used to disable button when needed
    public Image _progress;
    public Text remainTime;
    //TIME ELEMENTS
    public int hours; //to set the hours
    public int minutes; //to set the minutes
    public int seconds; //to set the seconds
    private bool _timerComplete = false;
    private bool _timerIsReady;
    private TimeSpan _startTime;
    private TimeSpan _endTime;
    private TimeSpan _remainingTime;
    //progress filler
    private float _value = 1f;
    //reward to claim
    public int RewardToEarn;

    public Mainmenu mainMenu;
    public Image buttonImage;


    private IEnumerator AlphaButtonZ;


    public Text popUpCoinsText;
    public GameObject particleCoin;


    //startup
    void Start()
    {
        particleCoin.gameObject.SetActive(false);
        if (PlayerPrefs.GetString("_timer") == "")
        {
            Debug.Log("==> Enableing button");
            enableButton();
            StartCoroutine(AlphaButton());
        }
        else
        {
            disableButton();
            StartCoroutine("CheckTime");
        }
        remainTime.text = "";
        mainMenu = GameObject.Find("Canvas").GetComponent<Mainmenu>();
        AlphaButtonZ = AlphaButton();

        var c = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.High,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(c);
    }



    //update the time information with what we got some the internet
    private void updateTime()
    {
        if (PlayerPrefs.GetString("_timer") == "Standby")
        {
            PlayerPrefs.SetString("_timer", TimeManager.sharedInstance.getCurrentTimeNow());
            PlayerPrefs.SetInt("_date", TimeManager.sharedInstance.getCurrentDateNow());
        }
        else if (PlayerPrefs.GetString("_timer") != "" && PlayerPrefs.GetString("_timer") != "Standby")
        {
            int _old = PlayerPrefs.GetInt("_date");
            int _now = TimeManager.sharedInstance.getCurrentDateNow();


            //check if a day as passed
            if (_now > _old)
            {//day as passed
                Debug.Log("Day has passed");
                enableButton();
                StartCoroutine(AlphaButtonZ);
                return;
            }
            else if (_now == _old)
            {//same day
                Debug.Log("Same Day - configuring now");
                _configTimerSettings();
                return;
            }
            else
            {
                Debug.Log("error with date");
                return;
            }
        }
        Debug.Log("Day had passed - configuring now");
        _configTimerSettings();
    }

    //setting up and configureing the values
    //update the time information with what we got some the internet
    private void _configTimerSettings()
    {
        _startTime = TimeSpan.Parse(PlayerPrefs.GetString("_timer"));
        _endTime = TimeSpan.Parse(hours + ":" + minutes + ":" + seconds);
        TimeSpan temp = TimeSpan.Parse(TimeManager.sharedInstance.getCurrentTimeNow());
        TimeSpan diff = temp.Subtract(_startTime);
        _remainingTime = _endTime.Subtract(diff);
        //start timmer where we left off
        setProgressWhereWeLeftOff();

        if (diff >= _endTime)
        {
            _timerComplete = true;
            enableButton();
            StartCoroutine(AlphaButtonZ);
        }
        else
        {
            _timerComplete = false;
            disableButton();
            StopCoroutine(AlphaButtonZ);
            _timerIsReady = true;
        }
    }


    IEnumerator AlphaButton()
    {
        while (true)
        {
            //Debug.Log("Going to AlphaButton!");
            buttonImage.color = new Color32(255, 255, 255, 155);
            yield return new WaitForSeconds(1f);
           // Debug.Log("Now normal!");
            buttonImage.color = new Color32(255, 255, 255, 255);
            yield return new WaitForSeconds(1f);
        }
    }


    //initializing the value of the timer
    private void setProgressWhereWeLeftOff()
    {
        float ah = 1f / (float)_endTime.TotalSeconds;
        float bh = 1f / (float)_remainingTime.TotalSeconds;
        _value = ah / bh;
        _progress.fillAmount = _value;
    }



    //enable button function
    private void enableButton()
    {
        timerButton.interactable = true;
        timeLabel.text = "CLAIM REWARD";
    }



    //disable button function
    private void disableButton()
    {
        timerButton.interactable = false;
        timeLabel.text = "NOT READY";
        buttonImage.color = new Color32(255, 255, 255, 155);
    }


    //use to check the current time before completely any task. use this to validate
    private IEnumerator CheckTime()
    {
        disableButton();
        timeLabel.text = "Checking the time";
        Debug.Log("==> Checking for new time");
        yield return StartCoroutine(
            TimeManager.sharedInstance.getTime()
        );
        updateTime();
        Debug.Log("==> Time check complete!");

    }


    //trggered on button click
    public void rewardClicked()
    {
        Debug.Log("==> Claim Button Clicked");
        claimReward(RewardToEarn);
        StopCoroutine(AlphaButtonZ);
        NotificationCreater();
        PlayerPrefs.SetString("_timer", "Standby");
        StartCoroutine("CheckTime");
    }



    //update method to make the progress tick
    void Update()
    {
        if (_timerIsReady)
        {
            if (!_timerComplete && PlayerPrefs.GetString("_timer") != "")
            {
                _value -= Time.deltaTime * 1f / (float)_endTime.TotalSeconds;
                _progress.fillAmount = _value;
                remainTime.text = "Come back tommorow at " + _startTime.ToString();


                //this is called once only
                if (_value <= 0 && !_timerComplete)
                {
                    //when the timer hits 0, let do a quick validation to make sure no speed hacks.
                    validateTime();
                    _timerComplete = true;
                    remainTime.text = "";
                }
            }
        }
    }



    //validator
    private void validateTime()
    {
        Debug.Log("==> Validating time to make sure no speed hack!");
        StartCoroutine("CheckTime");
    }


    private void claimReward(int x)
    {
        Debug.Log("YOU EARN " + x + " REWARDS");

        float money;
        money = ES_Save.Load<float>("money");
        money += x;
        ES_Save.Save(money, "money");
        mainMenu.RoundMoney();
        StartCoroutine(popUpCoins());

    }

    IEnumerator popUpCoins()
    {
        popUpCoinsText.gameObject.SetActive(true);
        popUpCoinsText.gameObject.GetComponent<Animator>().SetTrigger("500COINS");
        particleCoin.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        popUpCoinsText.gameObject.SetActive(false);
    }


    private void NotificationCreater()
    {
        Debug.Log("Notofication created");
        var notification = new AndroidNotification();
        notification.SmallIcon = "icon_0";
        notification.LargeIcon = "icon_1";
        notification.Title = "Get Your 500 coins!";
        notification.Text = "Your daily prize is ready to be picked up!";
        notification.FireTime = System.DateTime.Now.AddHours(24);

        AndroidNotificationCenter.SendNotification(notification, "channel_id");
    }

}