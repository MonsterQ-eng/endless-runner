using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QuantumTek.EncryptedSave;
using Unity.Notifications.Android;

public class DailyRewardNEW : MonoBehaviour
{

	public GameObject dailyRewardOpen;
	public Text rewardTimeText;
	public Text rewardDayText;

	public Image[] dailyRewardType;
	public UnityEngine.UI.Button rewardButton;

	private float money;

	public Mainmenu mainmenu;
	public Image buttonImage;

	private IEnumerator AlphaButtonZ;
	public GameObject particleCoin;

	private AudioSource audioSource;
	public AudioClip audioClip;


	private void Start()
    {
		particleCoin.gameObject.SetActive(false);
		AlphaButtonZ = AlphaButton();
		audioSource = GetComponent<AudioSource>();

		//Checking the daily challenge system.
		if (PlayerPrefs.GetInt("RewardDateDay", -1) == -1) //First time player joined, date day, month & year is -1 then set it to current date.
		{
			PlayerPrefs.SetInt("RewardDateDay", System.DateTime.Now.Day);
			PlayerPrefs.SetInt("RewardDateMonth", System.DateTime.Now.Month);
			PlayerPrefs.SetInt("RewardDateYear", System.DateTime.Now.Year);
			PlayerPrefs.SetInt("RewardON", 1);
		}
		//Else if it's not the first time the player logs in then check if the player hasn't the current day or the correct month or the correct year.
		else if (PlayerPrefs.GetInt("RewardDateDay", -1) != System.DateTime.Now.Day || PlayerPrefs.GetInt("RewardDateMonth", -1) != System.DateTime.Now.Month || PlayerPrefs.GetInt("RewardDateYear", -1) != System.DateTime.Now.Year)
		{
			PlayerPrefs.SetInt("RewardDateDay", System.DateTime.Now.Day);
			PlayerPrefs.SetInt("RewardDateMonth", System.DateTime.Now.Month);
			PlayerPrefs.SetInt("RewardDateYear", System.DateTime.Now.Year);

			if (PlayerPrefs.GetInt("RewardON", 1) == 1) //If the challenge is on!
			{
				//Then challenge wasn't finished in its day, reset everything!
				PlayerPrefs.SetInt("RewardDay", 1);
				PlayerPrefs.SetInt("DailyRewardDay", 1);
				PlayerPrefs.SetInt("RewardItems", 0);
			}
			else if (PlayerPrefs.GetInt("RewardON", 1) == 0) //If the challenge isn't on!
			{
				//Check if this is the next day of the last challenge!
				if (PlayerPrefs.GetInt("RewardNextDateDay", 0) == System.DateTime.Now.Day && PlayerPrefs.GetInt("RewardNextDateMonth", 0) == System.DateTime.Now.Month && PlayerPrefs.GetInt("RewardNextDateYear", 0) == System.DateTime.Now.Year)
				{
					//Then challenge wasn't finished in its day, reset everything!
					PlayerPrefs.SetInt("RewardON", 1);

					//Increase player'sz challenge days:
					PlayerPrefs.SetInt("RewardDay", PlayerPrefs.GetInt("RewardDay", 1) + 1);

					//Move to next reward day, if we reached the last reward day, move it back to day 1.
					//if (PlayerPrefs.GetInt("DailyRewardDay", 1) == 5)
					//{
					//	PlayerPrefs.SetInt("DailyRewardDay", 1);
					//}
					//else
					//{
					//	PlayerPrefs.SetInt("DailyRewardDay", PlayerPrefs.GetInt("DailyRewardDay", 1) + 1);
					//}
					PlayerPrefs.SetInt("DailyRewardDay", PlayerPrefs.GetInt("DailyRewardDay", 1) + 1);
					//PlayerPrefs.SetInt("RewardItems", 0);
				}
				else
				{
					//If it's not the correct next day then reset everything!
					PlayerPrefs.SetInt("RewardDay", 1);
					PlayerPrefs.SetInt("DailyRewardDay", 1);
					PlayerPrefs.SetInt("RewardItems", 0);
					PlayerPrefs.SetInt("RewardON", 1);
				}
			}
		}

		CheckButtonAlpha();
		refreshUI();
		var c = new AndroidNotificationChannel()
		{
			Id = "channel_id",
			Name = "Default Channel",
			Importance = Importance.High,
			Description = "Generic notifications",
		};
		AndroidNotificationCenter.RegisterNotificationChannel(c);
	}


	private void Update()
	{

		if (dailyRewardOpen.activeSelf)
		{
			int Hour = 23 - System.DateTime.Now.Hour;
			int Minute = 59 - System.DateTime.Now.Minute;
			int Second = 59 - System.DateTime.Now.Second;
			string HourString = Hour.ToString();
			string MinuteString = Minute.ToString();
			string SecondString = Second.ToString();
			if (Hour < 10)
			{
				HourString = "0" + Hour.ToString();
			}
			if (Minute < 10)
			{
				MinuteString = "0" + Minute.ToString();
			}
			if (Second < 10)
			{
				SecondString = "0" + Second.ToString();
			}

			if (PlayerPrefs.GetInt("RewardON", 1) == 0)
			{
				rewardTimeText.text = "Come back in: <color=blue>" + HourString + ":" + MinuteString + ":" + SecondString + " </color>for the next reward.";
			}
			else if (PlayerPrefs.GetInt("RewardON", 1) == 1)
			{
				rewardTimeText.text = "Reward ends in: <color=blue>" + HourString + ":" + MinuteString + ":" + SecondString + "</color>";
			}
		}
	}


	private void CheckButtonAlpha()
	{
		if (PlayerPrefs.GetInt("RewardON", 1) == 1)
		{
			StartCoroutine(AlphaButtonZ);
		}
		else
		{
			StopCoroutine(AlphaButtonZ);
		}
	}

	public void refreshUI()
	{
		if (PlayerPrefs.GetInt("RewardON", 1) == 1)
		{
			rewardButton.interactable = true;
		}
		else
			rewardButton.interactable = false;


		if(PlayerPrefs.GetInt("DailyRewardDay", 1) == 1)
		{
			for (int i = 0; i < dailyRewardType.Length; i++)
			{
				if (i == 0)
				{
					dailyRewardType[i].color = new Color(dailyRewardType[i].color.r, dailyRewardType[i].color.g, dailyRewardType[i].color.b, 1f);
				}
				else
					dailyRewardType[i].color = new Color(dailyRewardType[i].color.r, dailyRewardType[i].color.g, dailyRewardType[i].color.b, 0.5f); 
			}
		}
		if (PlayerPrefs.GetInt("DailyRewardDay", 1) == 2)
		{
			for (int i = 0; i < dailyRewardType.Length; i++)
			{
				if (i == 1)
				{
					dailyRewardType[i].color = new Color(dailyRewardType[i].color.r, dailyRewardType[i].color.g, dailyRewardType[i].color.b, 1f);
				}
				else
					dailyRewardType[i].color = new Color(dailyRewardType[i].color.r, dailyRewardType[i].color.g, dailyRewardType[i].color.b, 0.5f);
			}
		}
		if (PlayerPrefs.GetInt("DailyRewardDay", 1) == 3)
		{
			for (int i = 0; i < dailyRewardType.Length; i++)
			{
				if (i == 2)
				{
					dailyRewardType[i].color = new Color(dailyRewardType[i].color.r, dailyRewardType[i].color.g, dailyRewardType[i].color.b, 1f);
				}
				else
					dailyRewardType[i].color = new Color(dailyRewardType[i].color.r, dailyRewardType[i].color.g, dailyRewardType[i].color.b, 0.5f);
			}
		}
		if (PlayerPrefs.GetInt("DailyRewardDay", 1) >= 4)
		{
			for (int i = 0; i < dailyRewardType.Length; i++)
			{
				if (i == 3)
				{
					dailyRewardType[i].color = new Color(dailyRewardType[i].color.r, dailyRewardType[i].color.g, dailyRewardType[i].color.b, 1f);
				}
				else
					dailyRewardType[i].color = new Color(dailyRewardType[i].color.r, dailyRewardType[i].color.g, dailyRewardType[i].color.b, 0.5f);
			}
		}

		rewardDayText.text = "Day: <color=blue>" + PlayerPrefs.GetInt("DailyRewardDay", 1).ToString() + "</color>";

	}


	public void CollectReward()
	{

		audioSource.PlayOneShot(audioClip);


		//Desactivate challenge:

		PlayerPrefs.SetInt("RewardON", 0);
		StopCoroutine(AlphaButtonZ);
		NotificationCreater();
		//Set tomorrow's challange date:
		PlayerPrefs.SetInt("RewardNextDateDay", System.DateTime.Today.AddDays(1).Day);
		PlayerPrefs.SetInt("RewardNextDateMonth", System.DateTime.Today.AddDays(1).Month);
		PlayerPrefs.SetInt("RewardNextDateYear", System.DateTime.Today.AddDays(1).Year);

		if (PlayerPrefs.GetInt("DailyRewardDay", 1) == 1)
		{
			money = ES_Save.Load<float>("money");
			money += 250;
			ES_Save.Save(money, "money");
			if (mainmenu != null)
			{
				mainmenu.RoundMoney();
			}
			else
			{
				mainmenu = GameObject.Find("Canvas").GetComponent<Mainmenu>();
				mainmenu.RoundMoney();
			}
		}
		if (PlayerPrefs.GetInt("DailyRewardDay", 1) == 2)
		{
			money = ES_Save.Load<float>("money");
			money += 500;
			ES_Save.Save(money, "money");
			if (mainmenu != null)
			{
				mainmenu.RoundMoney();
			}
			else
			{
				mainmenu = GameObject.Find("Canvas").GetComponent<Mainmenu>();
				mainmenu.RoundMoney();
			}
		}
		if (PlayerPrefs.GetInt("DailyRewardDay", 1) == 3)
		{
			money = ES_Save.Load<float>("money");
			money += 750;
			ES_Save.Save(money, "money");
			if (mainmenu != null)
			{
				mainmenu.RoundMoney();
			}
			else
			{
				mainmenu = GameObject.Find("Canvas").GetComponent<Mainmenu>();
				mainmenu.RoundMoney();
			}
		}
		if (PlayerPrefs.GetInt("DailyRewardDay", 1) >= 4)
		{
			money = ES_Save.Load<float>("money");
			money += 1000;
			ES_Save.Save(money, "money");
			if (mainmenu != null)
			{
				mainmenu.RoundMoney();
			}
			else
			{
				mainmenu = GameObject.Find("Canvas").GetComponent<Mainmenu>();
				mainmenu.RoundMoney();
			}
		}
		refreshUI();
		particleCoin.gameObject.SetActive(true);
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

	private void NotificationCreater()
	{
		Debug.Log("Notofication created");
		var notification = new AndroidNotification();
		notification.SmallIcon = "icon_0";
		notification.LargeIcon = "icon_1";
		notification.Title = "Get Your free coins!";
		notification.Text = "Your daily prize is ready to be picked up!";
		notification.FireTime = System.DateTime.Now.AddHours(24);

		AndroidNotificationCenter.SendNotification(notification, "channel_id");
	}

}
