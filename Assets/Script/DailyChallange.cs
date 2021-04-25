using QuantumTek.EncryptedSave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyChallange : MonoBehaviour
{

	public static DailyChallange sharedInstance = null;

	public GameObject dailyChallangeOpen;
	public Text ChallengeTimerText;
	public Text ChallengeStatusText;
	public Image ChallengeItem1UI;
	public Image ChallengeItem2UI;
	public Image ChallengeItem3UI;

	public GameObject dailyChallangePopUp;
	public Text ChallengeMsg;

	public Mainmenu mainMenu;


	// Start is called before the first frame update
	void Start()
	{

		

		//Checking the daily challenge system.
		if (PlayerPrefs.GetInt("ChallengeDateDay", -1) == -1) //First time player joined, date day, month & year is -1 then set it to current date.
		{
			PlayerPrefs.SetInt("ChallengeDateDay", System.DateTime.Now.Day);
			PlayerPrefs.SetInt("ChallengeDateMonth", System.DateTime.Now.Month);
			PlayerPrefs.SetInt("ChallengeDateYear", System.DateTime.Now.Year);
			PlayerPrefs.SetInt("ChallengeON", 1);
			PlayerPrefs.SetInt("ChallengeDay", 1);
		}
		//Else if it's not the first time the player logs in then check if the player hasn't the current day or the correct month or the correct year.
		else if (PlayerPrefs.GetInt("ChallengeDateDay", -1) != System.DateTime.Now.Day || PlayerPrefs.GetInt("ChallengeDateMonth", -1) != System.DateTime.Now.Month || PlayerPrefs.GetInt("ChallengeDateYear", -1) != System.DateTime.Now.Year)
		{
			PlayerPrefs.SetInt("ChallengeDateDay", System.DateTime.Now.Day);
			PlayerPrefs.SetInt("ChallengeDateMonth", System.DateTime.Now.Month);
			PlayerPrefs.SetInt("ChallengeDateYear", System.DateTime.Now.Year);

			if (PlayerPrefs.GetInt("ChallengeON", 1) == 1) //If the challenge is on!
			{
				//Then challenge wasn't finished in its day, reset everything!
				PlayerPrefs.SetInt("ChallengeDay", 1);
				PlayerPrefs.SetInt("ChallengeRewardDay", 1);
				PlayerPrefs.SetInt("ChallengeItems", 0);
			}
			else if (PlayerPrefs.GetInt("ChallengeON", 1) == 0) //If the challenge isn't on!
			{
				//Check if this is the next day of the last challenge!
				if (PlayerPrefs.GetInt("ChallengeNextDateDay", 0) == System.DateTime.Now.Day && PlayerPrefs.GetInt("ChallengeNextDateMonth", 0) == System.DateTime.Now.Month && PlayerPrefs.GetInt("ChallengeNextDateYear", 0) == System.DateTime.Now.Year)
				{
					//Then challenge wasn't finished in its day, reset everything!
					PlayerPrefs.SetInt("ChallengeON", 1);

					//Increase player'sz challenge days:
					PlayerPrefs.SetInt("ChallengeDay", PlayerPrefs.GetInt("ChallengeDay", 1) + 1);

					//Move to next reward day, if we reached the last reward day, move it back to day 1.
					if (PlayerPrefs.GetInt("ChallengeRewardDay", 1) == 5)
					{
						PlayerPrefs.SetInt("ChallengeRewardDay", 1);
					}
					else
					{
						PlayerPrefs.SetInt("ChallengeRewardDay", PlayerPrefs.GetInt("ChallengeRewardDay", 1) + 1);
					}

					PlayerPrefs.SetInt("ChallengeItems", 0);
				}
				else
				{
					//If it's not the correct next day then reset everything!
					PlayerPrefs.SetInt("ChallengeDay", 1);
					PlayerPrefs.SetInt("ChallengeRewardDay", 1);
					PlayerPrefs.SetInt("ChallengeItems", 0);
					PlayerPrefs.SetInt("ChallengeON", 1);
				}
			}
		}
		RefreshUI();
		CheckDailyChallange();
	}

	// Update is called once per frame
	void Update()
	{
		if (dailyChallangeOpen.activeSelf) //Daily challenge menu:
		{
			//Calculating the time in hours, seconds and minutes till tomorrow!
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

			if (PlayerPrefs.GetInt("ChallengeON", 1) == 0)
			{
				ChallengeTimerText.text = "Come back in: <color=blue>" + HourString + ":" + MinuteString + ":" + SecondString + "</color> for the next challenge.";
			}
			else if (PlayerPrefs.GetInt("ChallengeON", 1) == 1)
			{
				ChallengeTimerText.text = "Challenge ends in: <color=blue>" + HourString + ":" + MinuteString + ":" + SecondString + "</color>";
			}
		}
	}



	private void RefreshUI()
	{
		string ChallengeStatus;
		if (PlayerPrefs.GetInt("ChallengeItems", 0) >= 3)
		{
			ChallengeStatus = " - All daily games done!";
		}
		else
		{
			ChallengeStatus = "  games!";
		}
		//show the day of the challenge and today's challenge items collected.
		if(PlayerPrefs.GetInt("ChallengeItems", 0) <= 3)
		{
			ChallengeStatusText.text = "Day " + PlayerPrefs.GetInt("ChallengeDay", 1) + ": Play <color=blue>" + PlayerPrefs.GetInt("ChallengeItems", 0) + "/3 </color>" + ChallengeStatus;

		}
		else
		{
			ChallengeStatusText.text = "Day " + PlayerPrefs.GetInt("ChallengeDay", 1) + ": Play <color=blue>" + "3" + "/3 </color>" + ChallengeStatus;
		}

		if (PlayerPrefs.GetInt("ChallengeItems", 0) == 0)
		{
			ChallengeItem1UI.color = new Color(ChallengeItem1UI.color.r, ChallengeItem1UI.color.g, ChallengeItem1UI.color.b, 0.5f);
			ChallengeItem2UI.color = new Color(ChallengeItem1UI.color.r, ChallengeItem1UI.color.g, ChallengeItem1UI.color.b, 0.5f);
			ChallengeItem3UI.color = new Color(ChallengeItem1UI.color.r, ChallengeItem1UI.color.g, ChallengeItem1UI.color.b, 0.5f);
		}
		if (PlayerPrefs.GetInt("ChallengeItems", 0) == 1)
		{
			ChallengeItem1UI.color = new Color(ChallengeItem1UI.color.r, ChallengeItem1UI.color.g, ChallengeItem1UI.color.b, 1f);
			ChallengeItem2UI.color = new Color(ChallengeItem1UI.color.r, ChallengeItem1UI.color.g, ChallengeItem1UI.color.b, 0.5f);
			ChallengeItem3UI.color = new Color(ChallengeItem1UI.color.r, ChallengeItem1UI.color.g, ChallengeItem1UI.color.b, 0.5f);

		}
		if (PlayerPrefs.GetInt("ChallengeItems", 0) == 2)
		{
			ChallengeItem1UI.color = new Color(ChallengeItem1UI.color.r, ChallengeItem1UI.color.g, ChallengeItem1UI.color.b, 1f);
			ChallengeItem2UI.color = new Color(ChallengeItem1UI.color.r, ChallengeItem1UI.color.g, ChallengeItem1UI.color.b, 1f);
			ChallengeItem3UI.color = new Color(ChallengeItem1UI.color.r, ChallengeItem1UI.color.g, ChallengeItem1UI.color.b, 0.5f);

		}
		if (PlayerPrefs.GetInt("ChallengeItems", 0) == 3)
		{
			ChallengeItem1UI.color = new Color(ChallengeItem1UI.color.r, ChallengeItem1UI.color.g, ChallengeItem1UI.color.b, 1f);
			ChallengeItem2UI.color = new Color(ChallengeItem1UI.color.r, ChallengeItem1UI.color.g, ChallengeItem1UI.color.b, 1f);
			ChallengeItem3UI.color = new Color(ChallengeItem1UI.color.r, ChallengeItem1UI.color.g, ChallengeItem1UI.color.b, 1f);
		}

	}

	public void CheckDailyChallange()
	{
		if(mainMenu == null)
		{
			mainMenu = GameObject.Find("Canvas").GetComponent<Mainmenu>();
		}
		int Amount;
		float money;
		money = ES_Save.Load<float>("money");
		if (PlayerPrefs.GetInt("ChallengeItems", 0) == 3 && PlayerPrefs.GetInt("ChallengeON", 1) == 1) //If the player collected all items.  
		{
			//Desactivate challenge:
			PlayerPrefs.SetInt("ChallengeON", 0);

			//Set tomorrow's challange date:
			PlayerPrefs.SetInt("ChallengeNextDateDay", System.DateTime.Today.AddDays(1).Day);
			PlayerPrefs.SetInt("ChallengeNextDateMonth", System.DateTime.Today.AddDays(1).Month);
			PlayerPrefs.SetInt("ChallengeNextDateYear", System.DateTime.Today.AddDays(1).Year);
			if (PlayerPrefs.GetInt("ChallengeRewardDay", 1) == 1)
			{

				Amount = Random.Range(150, 300);
				dailyChallangePopUp.SetActive(true);
				ChallengeMsg.text = "You earned: " + "<color=blue>" + Amount.ToString() + "</color> coins!";
				money += Amount;
				ES_Save.Save(money, "money");
				mainMenu.RoundMoney();
			}

			//Day 2: more coins.
			if (PlayerPrefs.GetInt("ChallengeRewardDay", 1) == 2)
			{

				Amount = Random.Range(300, 450);
				dailyChallangePopUp.SetActive(true);
				ChallengeMsg.text = "You earned: " + "<color=blue>" + Amount.ToString() + "</color> coins!";
				money += Amount;
				ES_Save.Save(money, "money");
				mainMenu.RoundMoney();
			}

			//Day 3: even more coins.
			if (PlayerPrefs.GetInt("ChallengeRewardDay", 1) == 3)
			{

				Amount = Random.Range(450, 1000);
				dailyChallangePopUp.SetActive(true);
				ChallengeMsg.text = "You earned: " + "<color=blue>" + Amount.ToString() + "</color> coins!";
				money += Amount;
				ES_Save.Save(money, "money");
				mainMenu.RoundMoney();
			}

			//Day 4: more and more coins.
			if (PlayerPrefs.GetInt("ChallengeRewardDay", 1) == 4)
			{

				Amount = Random.Range(1000, 1500);
				dailyChallangePopUp.SetActive(true);
				ChallengeMsg.text = "You earned: " + "<color=blue>" + Amount.ToString() + "</color> coins!";
				money += Amount;
				ES_Save.Save(money, "money");
				mainMenu.RoundMoney();
			}

			//Day 5: some free boost points.
			if (PlayerPrefs.GetInt("ChallengeRewardDay", 1) == 5)
			{

				Amount = Random.Range(1500, 2000);
				dailyChallangePopUp.SetActive(true);
				ChallengeMsg.text = "You earned: " + "<color=blue>" + Amount.ToString() + "</color> coins!";
				money += Amount;
				ES_Save.Save(money, "money");
				mainMenu.RoundMoney();
			}
		}



	}


	public void ExitDailyPopUp()
	{
		LeanTween.scale(dailyChallangePopUp, new Vector3(0, 0, 0), 0.3f).setOnComplete(()=> dailyChallangePopUp.SetActive(false));
	}


}

