using QuantumTek.EncryptedSave;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MissionHandler : MonoBehaviour
{
	//Klasa Misje
	public class Goal
	{
		public string name;
		public string name2;
		public int progress;
		public int payOut;
		public int IDMission;
		public int score;
		public float multiplier;
		public int unclock;
	}

	public List<Goal> myMissionStore = new List<Goal>();
	
	//UI
	[System.Serializable]
	public class UIMissionVar
	{
		public Text nameMission;
		public Text payOut;
		public UnityEngine.UI.Button button;
		public GameObject completed;
	}

	public UIMissionVar[] UiMission;
	public Text levelText;

	//Zmienne
	private float money;
	public Mainmenu canvas;
	public Player player;
	private int playerLevel;


	int temp0;
	int temp1;
	int temp2;
	int temp3;
	int temp4;
	int temp5;
	int temp6;
	int temp7;
	int temp8;
	int temp9;

	int temp;
	int tempM2;
	int tempM3;

	int unlocked1;
	int unlocked2;
	int unlocked3;


	private AudioSource audioSource;
	public AudioClip audioClip;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
		temp = 0;
		tempM2 = 0;
		tempM3 = 0;
		canvas = GameObject.Find("Canvas").GetComponent<Mainmenu>();
		if (ES_Save.Exists("playerLevel"))
		{
			playerLevel = ES_Save.Load<int>("playerLevel");
			levelText.text = "Level " + playerLevel;
		}
		else
		{
			playerLevel = 1;
			ES_Save.Save(playerLevel, "playerLevel");
			levelText.text = "Level " + playerLevel;
		}
		if(SceneManager.GetActiveScene().buildIndex == 1)
		player = GameObject.Find("Player").GetComponent<Player>();
		CreateMission();

		StartCoroutine(MissionDO());

		UpdatePopUp();

	}


	private void UpdatePopUp()
	{
		int mission1 = PlayerPrefs.GetInt("Mission0");
		int mission2 = PlayerPrefs.GetInt("Mission1");
		int mission3 = PlayerPrefs.GetInt("Mission2");

		if (SceneManager.GetActiveScene().buildIndex == 0)
		{
			if (myMissionStore[mission1].unclock == 1)
			{
				PlayerPrefs.SetInt("unlocked1", 1);
			}
			else
			{
				PlayerPrefs.SetInt("unlocked1", 0);
			}
			if (myMissionStore[mission2].unclock == 1)
			{
				PlayerPrefs.SetInt("unlocked2", 1);
			}
			else
			{
				PlayerPrefs.SetInt("unlocked2", 0);
			}
			if (myMissionStore[mission3].unclock == 1)
			{
				PlayerPrefs.SetInt("unlocked3", 1);
			}
			else
			{
				PlayerPrefs.SetInt("unlocked3", 0);
			}

		}
	}


	private void CreateMission()
	{
		Goal mission1 = new Goal();
		Goal mission2 = new Goal();
		Goal mission3 = new Goal();
		Goal mission4 = new Goal();
		Goal mission5 = new Goal();
		Goal mission6 = new Goal();
		Goal mission7 = new Goal();
		Goal mission8 = new Goal();
		Goal mission9 = new Goal();
		Goal mission10 = new Goal();
		//Goal mission11 = new Goal();

		myMissionStore.Clear();

		//Misja TOTAL SCORE
		mission1.name = "Get in <color=yellow>TOTAL ";
		mission1.score = 500;
		mission1.name2 = "score!";
		mission1.progress = 0;
		mission1.payOut = 500;
		mission1.IDMission = 0;
		mission1.multiplier = 2.0f;
		mission1.unclock = 0;	//0 -nie 1- tak


		//Misja ONE GAME SCORE
		mission2.name = "Get in <color=yellow>ONE GAME ";
		mission2.score = 300;
		mission2.name2 = "score!";
		mission2.progress = 0;
		mission2.payOut = 500;
		mission2.IDMission = 1;
		mission2.multiplier = 2.0f;
		mission2.unclock = 0;

		//Misja MONEY TOTAL
		mission3.name = "Get in <color=yellow>TOTAL ";
		mission3.score = 250;
		mission3.name2 = "coins!";
		mission3.progress = 0;
		mission3.payOut = 500;
		mission3.IDMission = 2;
		mission3.multiplier = 2.0f;
		mission3.unclock = 0;

		//Misja MONEY ONE GAME
		mission4.name = "Get in <color=yellow>ONE GAME ";
		mission4.score = 20;
		mission4.name2 = "coins!";
		mission4.progress = 0;
		mission4.payOut = 500;
		mission4.IDMission = 3;
		mission4.multiplier = 1.0f;
		mission4.unclock = 0;

		//Misja BOOSTER TOTAL
		mission5.name = "Get in <color=yellow>TOTAL ";
		mission5.score = 5;
		mission5.name2 = "boosters!";
		mission5.progress = 0;
		mission5.payOut = 500;
		mission5.IDMission = 4;
		mission5.multiplier = 1.0f;
		mission5.unclock = 0;

		//Misja BOOSTER ONE GAME	
		mission6.name = "Get in <color=yellow>ONE GAME ";
		mission6.score = 2;
		mission6.name2 = "boosters!";
		mission6.progress = 0;
		mission6.payOut = 500;
		mission6.IDMission = 5;
		mission6.multiplier = 0.5f;
		mission6.unclock = 0;

		//Misja BEAT HIGHSCORE
		mission7.name = "Beat <color=yellow>yours ";
		mission7.score = 1;
		mission7.name2 = "highscore!";
		mission7.progress = 0;
		mission7.payOut = 750;
		mission7.IDMission = 6;
		mission7.multiplier = 0.0f;
		mission7.unclock = 0;

		//Misja PLAY GAMES
		mission8.name = "Play <color=yellow> ";
		mission8.score = 3;
		mission8.name2 = "games!";
		mission8.progress = 0;
		mission8.payOut = 750;
		mission8.IDMission = 7;
		mission8.multiplier = 0.0f;
		mission8.unclock = 0;

		//Misja KILL ENEMY TOTAL
		mission9.name = "Kill in <color=yellow>TOTAL ";
		mission9.score = 5;
		mission9.name2 = "enemy!";
		mission9.progress = 0;
		mission9.payOut = 500;
		mission9.IDMission = 8;
		mission9.multiplier = 1.0f;
		mission9.unclock = 0;

		//Misja KILL ENEMY ONE GAME
		mission10.name = "Kill in <color=yellow>ONE GAME ";
		mission10.score = 2;
		mission10.name2 = "enemy!";
		mission10.progress = 0;
		mission10.payOut = 500;
		mission10.IDMission = 9;
		mission10.multiplier = 0.5f;
		mission10.unclock = 0;

		//Misja SPIKE TOTAL
		//mission11.name = "Trigger in <color=yellow>TOTAL ";
		//mission11.score = 5;
		//mission11.name2 = "spike!";
		//mission11.progress = 0;
		//mission11.payOut = 500;
		//mission11.IDMission = 10;
		//mission11.multiplier = 1.0f;
		//mission11.unclock = 0;


		myMissionStore.Add(mission1);
		myMissionStore.Add(mission2);
		myMissionStore.Add(mission3);
		myMissionStore.Add(mission4);
		myMissionStore.Add(mission5);
		myMissionStore.Add(mission6);
		myMissionStore.Add(mission7);
		myMissionStore.Add(mission8);
		myMissionStore.Add(mission9);
		myMissionStore.Add(mission10);
		//myMissionStore.Add(mission11);


		if(playerLevel > 1)
		{
			for (int i = 0; i < myMissionStore.Count; i++)
			{
				if(myMissionStore[i].multiplier != 0.0f)
				{
					myMissionStore[i].score *= (int)Math.Round(playerLevel * myMissionStore[i].multiplier);
				}				
			}
		}
		RefreshMission();
	}

	public void RefreshMission()
	{
		if(PlayerPrefs.GetInt("MissionSelected", 0) == 1)
		{
			//Ładuje i wyświetlam misje
			int mission1 = PlayerPrefs.GetInt("Mission0");
			int mission2 = PlayerPrefs.GetInt("Mission1");
			int mission3 = PlayerPrefs.GetInt("Mission2");
			int mission1Progress = PlayerPrefs.GetInt("Mission0Progress", 0);
			int mission2Progress = PlayerPrefs.GetInt("Mission1Progress", 0);
			int mission3Progress = PlayerPrefs.GetInt("Mission2Progress", 0);
			int mission1Unlock = PlayerPrefs.GetInt("Mission0Unlock", 0);
			int mission2Unlock = PlayerPrefs.GetInt("Mission1Unlock", 0);
			int mission3Unlock = PlayerPrefs.GetInt("Mission2Unlock", 0);

			Debug.Log("Numery Misji Stare: " + mission1 + " " + mission2 + " " + mission3);

			if(mission1Progress != 0)
			{
				myMissionStore[mission1].progress = mission1Progress;
			}
			if (mission2Progress != 0)
			{
				myMissionStore[mission2].progress = mission2Progress;
			}
			if (mission3Progress != 0)
			{
				myMissionStore[mission3].progress = mission3Progress;
			}
			if(mission1Unlock != 0)
			{
				myMissionStore[mission1].unclock = mission1Unlock;
			}
			if (mission2Unlock != 0)
			{
				myMissionStore[mission2].unclock = mission2Unlock;
			}
			if (mission3Unlock != 0)
			{
				myMissionStore[mission3].unclock = mission3Unlock;
			}



			ShowMission(mission1, mission2, mission3);
		}
		else
		{
			//Losowanie 3 misji i wyświetlenie
			int newMision1 = Random.Range(0, myMissionStore.Count);
			int newMision2 = Random.Range(0, myMissionStore.Count);
			while(newMision2 == newMision1)
			{
				newMision2 = Random.Range(0, myMissionStore.Count);
			}
			int newMision3 = Random.Range(0, myMissionStore.Count);
			while(newMision3 == newMision2 || newMision3 == newMision1)
			{
				newMision3 = Random.Range(0, myMissionStore.Count);
			}
			Debug.Log("Numery Misji: " + newMision1 + newMision2 + newMision3);
			if(newMision1 != newMision2 || newMision2 != newMision3 || newMision1 != newMision3)
			{
				PlayerPrefs.SetInt("Mission0", newMision1);
				PlayerPrefs.SetInt("Mission1", newMision2);
				PlayerPrefs.SetInt("Mission2", newMision3);
				PlayerPrefs.SetInt("MissionSelected", 1);

				myMissionStore[newMision1].progress = 0;
				myMissionStore[newMision2].progress = 0;
				myMissionStore[newMision3].progress = 0;
				myMissionStore[newMision1].unclock = 0;
				myMissionStore[newMision2].unclock = 0;
				myMissionStore[newMision3].unclock = 0;

				PlayerPrefs.SetInt("Mission0Progress", 0);
				PlayerPrefs.SetInt("Mission1Progress", 0);
				PlayerPrefs.SetInt("Mission2Progress", 0);
				PlayerPrefs.SetInt("Mission0Unlock", 0);
				PlayerPrefs.SetInt("Mission1Unlock", 0);
				PlayerPrefs.SetInt("Mission2Unlock", 0);

				ShowMission(newMision1, newMision2, newMision3);

				if (player != null)
				{
					switch (newMision1)
					{
						case 0:
							temp = (int)player.Distance();
							break;
						case 1:
							temp = (int)player.Distance();
							break;
						case 2:
							temp = (int)player.HowMoney();
							break;
						case 3:
							temp = (int)player.HowMoney();
							break;
						case 4:
							temp = player.HowBoosterCounter();
							break;
						case 5:
							temp = player.HowBoosterCounter();
							break;
						case 6:
							temp = 0;
							break;
						case 7:
							temp = 0;
							break;
						case 8:
							temp = player.HowEnemyCounter();
							break;
						case 9:
							temp = player.HowEnemyCounter();
							break;
					}
					switch (newMision2)
					{
						case 0:
							tempM2 = (int)player.Distance();
							break;
						case 1:
							tempM2 = (int)player.Distance();
							break;
						case 2:
							tempM2 = (int)player.HowMoney();
							break;
						case 3:
							tempM2 = (int)player.HowMoney();
							break;
						case 4:
							tempM2 = player.HowBoosterCounter();
							break;
						case 5:
							tempM2 = player.HowBoosterCounter();
							break;
						case 6:
							tempM2 = 0;
							break;
						case 7:
							tempM2 = 0;
							break;
						case 8:
							tempM2 = player.HowEnemyCounter();
							break;
						case 9:
							tempM2 = player.HowEnemyCounter();
							break;
					}
					switch (newMision3)
					{
						case 0:
							tempM3 = (int)player.Distance();
							break;
						case 1:
							tempM3 = (int)player.Distance();
							break;
						case 2:
							tempM3 = (int)player.HowMoney();
							break;
						case 3:
							tempM3 = (int)player.HowMoney();
							break;
						case 4:
							tempM3 = player.HowBoosterCounter();
							break;
						case 5:
							tempM3 = player.HowBoosterCounter();
							break;
						case 6:
							tempM3 = 0;
							break;
						case 7:
							tempM3 = 0;
							break;
						case 8:
							tempM3 = player.HowEnemyCounter();
							break;
						case 9:
							tempM3 = player.HowEnemyCounter();
							break;
					}
				}

			}
		}


	}

	public void ShowMission(int a, int b, int c)
	{
		//Wyświetlanie nazw misji
		UiMission[0].nameMission.text = myMissionStore[a].name + myMissionStore[a].progress + "/" + myMissionStore[a].score + "</color> " + myMissionStore[a].name2;
		UiMission[0].payOut.text = myMissionStore[a].payOut.ToString();
		UiMission[1].nameMission.text = myMissionStore[b].name + myMissionStore[b].progress + "/" + myMissionStore[b].score + "</color> " + myMissionStore[b].name2;
		UiMission[1].payOut.text = myMissionStore[b].payOut.ToString();
		UiMission[2].nameMission.text = myMissionStore[c].name + myMissionStore[c].progress + "/" + myMissionStore[c].score + "</color> " + myMissionStore[c].name2;
		UiMission[2].payOut.text = myMissionStore[c].payOut.ToString();
		//Dodawanie przycisków
		UiMission[0].button.onClick.RemoveAllListeners();
		UiMission[1].button.onClick.RemoveAllListeners();
		UiMission[2].button.onClick.RemoveAllListeners();
		Debug.Log("Tworzenie przycisków");
		UiMission[0].button.onClick.AddListener(() => UnlockMissionPayOut(a, 0));
		UiMission[1].button.onClick.AddListener(() => UnlockMissionPayOut(b, 1));
		UiMission[2].button.onClick.AddListener(() => UnlockMissionPayOut(c, 2));
		//Pokazanie completed
		if(myMissionStore[a].unclock == 1)
		{
			UiMission[0].completed.SetActive(true);
			//if (player != null)
			//	player.MissionCompleted(1);
		}else
			UiMission[0].completed.SetActive(false);
		if (myMissionStore[b].unclock == 1)
		{
			UiMission[1].completed.SetActive(true);
			//if (player != null)
			//	player.MissionCompleted(2);
		}
		else
			UiMission[1].completed.SetActive(false);
		if (myMissionStore[c].unclock == 1)
		{
			UiMission[2].completed.SetActive(true);
			//if (player != null)
			//	player.MissionCompleted(3);
		}
		else
			UiMission[2].completed.SetActive(false);
		
		CheckAllMission();
		
	}


	public void UnlockMissionPayOut(int idMission, int idButton)
	{
		Debug.Log("Przycisk skipe włączony: "+ idButton);
		audioSource.PlayOneShot(audioClip);

		money = ES_Save.Load<float>("money");
		if(money >= myMissionStore[idMission].payOut)
		{
			money -= myMissionStore[idMission].payOut;
			ES_Save.Save(money, "money");
			if(canvas != null)
			{
				canvas.RoundMoney();
			}
			myMissionStore[idMission].unclock = 1;
			myMissionStore[idMission].progress = myMissionStore[idMission].score;
			UiMission[idButton].nameMission.text = myMissionStore[idMission].name + myMissionStore[idMission].progress + "/" + myMissionStore[idMission].score + "</color> " + myMissionStore[idMission].name2;
			UiMission[idButton].completed.SetActive(true);

			//Zapisanie wykupionych misji
			string number = idButton.ToString();
			PlayerPrefs.SetInt("Mission" + number + "Progress", myMissionStore[idMission].progress);
			PlayerPrefs.SetInt("Mission" + number + "Unlock", myMissionStore[idMission].unclock);
			CheckAllMission();
		}
		

	
	}

	public void CheckAllMission()
	{
		int mission0 = PlayerPrefs.GetInt("Mission0");
		int mission1 = PlayerPrefs.GetInt("Mission1");
		int mission2 = PlayerPrefs.GetInt("Mission2");

		if(myMissionStore[mission0].unclock == 1 && myMissionStore[mission1].unclock == 1 && myMissionStore[mission2].unclock == 1)
		{
			playerLevel += 1;
			levelText.text = "Level " + playerLevel;

			ES_Save.Save(playerLevel, "playerLevel");

			temp0 = 0;
			temp1 = 0;
			temp2 = 0;
			temp3 = 0;
			temp4 = 0;
			temp5 = 0;
			temp6 = 0;
			temp7 = 0;
			temp8 = 0;
			temp9 = 0;
			doOnce0 = true;
			doOnce1 = true;
			doOnce2 = true;

			PlayerPrefs.SetInt("unlocked1", 0);
			PlayerPrefs.SetInt("unlocked2", 0);
			PlayerPrefs.SetInt("unlocked3", 0);

			PlayerPrefs.SetInt("Mission0Progress", 0);
			PlayerPrefs.SetInt("Mission1Progress", 0);
			PlayerPrefs.SetInt("Mission2Progress", 0);
			PlayerPrefs.SetInt("Mission0Unlock", 0);
			PlayerPrefs.SetInt("Mission1Unlock", 0);
			PlayerPrefs.SetInt("Mission2Unlock", 0);

			PlayerPrefs.SetInt("MissionSelected", 0);

			CreateMission();

			player.RefreshLevel();
			player.LevelUP();
			
		}
	}

	

	public void Mission0()
	{
		//int _speed = (int)player.Speed();
		//if (doubleScore)
		//{
		//	temp0 += (int)Math.Round(_speed * Time.deltaTime * 2 );
		//}
		//else
		//{
		//	temp0 += (int)Math.Round(_speed * Time.deltaTime );
		//}
		if (player != null)
			temp0 = (int)player.Distance();
	}
	public void Mission1()
	{
		if (player != null)
			temp1 = (int)player.Distance();
	}

	public void Mission2()		// 0- coin  1- enemy
	{
		//if (doubleCoin && type == 0)
		//{
		//	temp2 += 2;
		//}
		//else
		//{
		//	temp2 += 1;
		//}
		//if(doubleCoin && type == 1)
		//{
		//	temp2 += 20;

		//}
		//else
		//{
		//	temp2 += 10;
		//}
		if (player != null)
			temp2 = (int)player.HowMoney();
	}


	public void Mission3()
	{
		if (player != null)
			temp3 = (int)player.HowMoney();
	}

	public void Mission4()
	{
		temp4 += 1; 
	}

	public void Mission5()
	{
		if (player != null)
			temp5 = player.HowBoosterCounter();
	}

	public void Mission6()
	{
		temp6 += 1;
	}

	public void Mission7()
	{
		temp7 += 1;
	}

	public void Mission8()
	{
		temp8 += 1;
	}

	public void Mission9()
	{
		if(player != null)
		temp9 = player.HowEnemyCounter();
	}

	IEnumerator MissionDO()
	{
		while (true)
		{
			MissionDoIt();
			UpdatePopUp();
			ShowPopUP();
			yield return new WaitForSeconds(5f);
		}
	}


	public void MissionDoIt()
	{
		int mission1 = PlayerPrefs.GetInt("Mission0");
		int mission2 = PlayerPrefs.GetInt("Mission1");
		int mission3 = PlayerPrefs.GetInt("Mission2");

		switch (mission1)
		{
			case 0:
				Mission0();
				myMissionStore[mission1].progress += temp0 - temp;
				temp = temp0;
				PlayerPrefs.SetInt("Mission0Progress", myMissionStore[mission1].progress);
				if(myMissionStore[mission1].progress >= myMissionStore[mission1].score)
				{
					myMissionStore[mission1].progress = myMissionStore[mission1].score;
					myMissionStore[mission1].unclock = 1;
					PlayerPrefs.SetInt("Mission0Progress", myMissionStore[mission1].progress);
					PlayerPrefs.SetInt("Mission0Unlock", myMissionStore[mission1].unclock);
					RefreshMission();
				}
				else
				{
					RefreshMission();
				}
				break;
			case 1:
				Mission1();
				myMissionStore[mission1].progress += temp1 - temp;
				temp = temp1;
				if (myMissionStore[mission1].progress >= myMissionStore[mission1].score)
				{
					myMissionStore[mission1].progress = myMissionStore[mission1].score;
					myMissionStore[mission1].unclock = 1;
					PlayerPrefs.SetInt("Mission0Progress", myMissionStore[mission1].progress);
					PlayerPrefs.SetInt("Mission0Unlock", myMissionStore[mission1].unclock);
					RefreshMission();
				}
				else
				{
					RefreshMission();
				}
				break;
			case 2:
				Mission2();
				myMissionStore[mission1].progress += temp2 - temp;
				temp = temp2;
				PlayerPrefs.SetInt("Mission0Progress", myMissionStore[mission1].progress);
				if (myMissionStore[mission1].progress >= myMissionStore[mission1].score)
				{
					myMissionStore[mission1].progress = myMissionStore[mission1].score;
					myMissionStore[mission1].unclock = 1;
					PlayerPrefs.SetInt("Mission0Progress", myMissionStore[mission1].progress);
					PlayerPrefs.SetInt("Mission0Unlock", myMissionStore[mission1].unclock);
					RefreshMission();
				}
				else
				{
					RefreshMission();
				}
				break;
			case 3:
				Mission3();
				myMissionStore[mission1].progress += temp3 - temp;
				temp = temp3;
				if (myMissionStore[mission1].progress >= myMissionStore[mission1].score)
				{
					myMissionStore[mission1].progress = myMissionStore[mission1].score;
					myMissionStore[mission1].unclock = 1;
					PlayerPrefs.SetInt("Mission0Progress", myMissionStore[mission1].progress);
					PlayerPrefs.SetInt("Mission0Unlock", myMissionStore[mission1].unclock);
					RefreshMission();
				}
				else
				{
					RefreshMission();
				}
				break;
			case 4:
				myMissionStore[mission1].progress += temp4 - temp;
				temp = temp4;
				PlayerPrefs.SetInt("Mission0Progress", myMissionStore[mission1].progress);
				if (myMissionStore[mission1].progress >= myMissionStore[mission1].score)
				{
					myMissionStore[mission1].progress = myMissionStore[mission1].score;
					myMissionStore[mission1].unclock = 1;
					PlayerPrefs.SetInt("Mission0Progress", myMissionStore[mission1].progress);
					PlayerPrefs.SetInt("Mission0Unlock", myMissionStore[mission1].unclock);
					RefreshMission();
				}
				else
				{
					RefreshMission();
				}
				break;
			case 5:
				Mission5();
				myMissionStore[mission1].progress += temp5 - temp;
				temp = temp5;
				if (myMissionStore[mission1].progress >= myMissionStore[mission1].score)
				{
					myMissionStore[mission1].progress = myMissionStore[mission1].score;
					myMissionStore[mission1].unclock = 1;
					PlayerPrefs.SetInt("Mission0Progress", myMissionStore[mission1].progress);
					PlayerPrefs.SetInt("Mission0Unlock", myMissionStore[mission1].unclock);
					RefreshMission();
				}
				else
				{
					RefreshMission();
				}
				break;
			case 6:
				if(temp6 >= 1)
				{
					myMissionStore[mission1].progress = myMissionStore[mission1].score;
					myMissionStore[mission1].unclock = 1;
					PlayerPrefs.SetInt("Mission0Progress", myMissionStore[mission1].progress);
					PlayerPrefs.SetInt("Mission0Unlock", myMissionStore[mission1].unclock);
					RefreshMission();
				}
				break;
			case 7:
				myMissionStore[mission1].progress += temp7 - temp;
				temp = temp7;
				PlayerPrefs.SetInt("Mission0Progress", myMissionStore[mission1].progress);
				if (myMissionStore[mission1].progress >= myMissionStore[mission1].score)
				{
					myMissionStore[mission1].progress = myMissionStore[mission1].score;
					myMissionStore[mission1].unclock = 1;
					PlayerPrefs.SetInt("Mission0Progress", myMissionStore[mission1].progress);
					PlayerPrefs.SetInt("Mission0Unlock", myMissionStore[mission1].unclock);
					RefreshMission();
				}
				else
				{
					RefreshMission();
				}
				break;
			case 8:
				myMissionStore[mission1].progress += temp8 - temp;
				temp = temp8;
				PlayerPrefs.SetInt("Mission0Progress", myMissionStore[mission1].progress);
				if (myMissionStore[mission1].progress >= myMissionStore[mission1].score)
				{
					myMissionStore[mission1].progress = myMissionStore[mission1].score;
					myMissionStore[mission1].unclock = 1;
					PlayerPrefs.SetInt("Mission0Progress", myMissionStore[mission1].progress);
					PlayerPrefs.SetInt("Mission0Unlock", myMissionStore[mission1].unclock);
					RefreshMission();
				}
				else
				{
					RefreshMission();
				}
				break;
			case 9:
				Mission9();
				myMissionStore[mission1].progress += temp9 - temp;
				temp = temp9;
				if (myMissionStore[mission1].progress >= myMissionStore[mission1].score)
				{
					myMissionStore[mission1].progress = myMissionStore[mission1].score;
					myMissionStore[mission1].unclock = 1;
					PlayerPrefs.SetInt("Mission0Progress", myMissionStore[mission1].progress);
					PlayerPrefs.SetInt("Mission0Unlock", myMissionStore[mission1].unclock);
					RefreshMission();
				}
				else
				{
					RefreshMission();
				}
				break;
		}
		switch (mission2)
		{
			case 0:
				Mission0();
				myMissionStore[mission2].progress += temp0 - tempM2;
				tempM2 = temp0;
				PlayerPrefs.SetInt("Mission1Progress", myMissionStore[mission2].progress);
				if (myMissionStore[mission2].progress >= myMissionStore[mission2].score)
				{
					myMissionStore[mission2].progress = myMissionStore[mission2].score;
					myMissionStore[mission2].unclock = 1;
					PlayerPrefs.SetInt("Mission1Progress", myMissionStore[mission2].progress);
					PlayerPrefs.SetInt("Mission1Unlock", myMissionStore[mission2].unclock);
					RefreshMission();
				}
				else
				{
					RefreshMission();
				}
				break;
			case 1:
				Mission1();
				myMissionStore[mission2].progress += temp1 - tempM2;
				tempM2 = temp1;
				if (myMissionStore[mission2].progress >= myMissionStore[mission2].score)
				{
					myMissionStore[mission2].progress = myMissionStore[mission2].score;
					myMissionStore[mission2].unclock = 1;
					PlayerPrefs.SetInt("Mission1Progress", myMissionStore[mission2].progress);
					PlayerPrefs.SetInt("Mission1Unlock", myMissionStore[mission2].unclock);
					RefreshMission();
				}
				else
				{
					RefreshMission();
				}
				break;
			case 2:
				Mission2();
				myMissionStore[mission2].progress += temp2 - tempM2;
				tempM2 = temp2;
				PlayerPrefs.SetInt("Mission1Progress", myMissionStore[mission2].progress);
				if (myMissionStore[mission2].progress >= myMissionStore[mission2].score)
				{
					myMissionStore[mission2].progress = myMissionStore[mission2].score;
					myMissionStore[mission2].unclock = 1;
					PlayerPrefs.SetInt("Mission1Progress", myMissionStore[mission2].progress);
					PlayerPrefs.SetInt("Mission1Unlock", myMissionStore[mission2].unclock);
					RefreshMission();
				}
				else
				{
					RefreshMission();
				}
				break;
			case 3:
				Mission3();
				myMissionStore[mission2].progress += temp3 - tempM2;
				tempM2 = temp3;
				if (myMissionStore[mission2].progress >= myMissionStore[mission2].score)
				{
					myMissionStore[mission2].progress = myMissionStore[mission2].score;
					myMissionStore[mission2].unclock = 1;
					PlayerPrefs.SetInt("Mission1Progress", myMissionStore[mission2].progress);
					PlayerPrefs.SetInt("Mission1Unlock", myMissionStore[mission2].unclock);
					RefreshMission();
				}
				else
				{
					RefreshMission();
				}
				break;
			case 4:
				myMissionStore[mission2].progress += temp4 - tempM2;
				tempM2 = temp4;
				PlayerPrefs.SetInt("Mission1Progress", myMissionStore[mission2].progress);
				if (myMissionStore[mission2].progress >= myMissionStore[mission2].score)
				{
					myMissionStore[mission2].progress = myMissionStore[mission2].score;
					myMissionStore[mission2].unclock = 1;
					PlayerPrefs.SetInt("Mission1Progress", myMissionStore[mission2].progress);
					PlayerPrefs.SetInt("Mission1Unlock", myMissionStore[mission2].unclock);
					RefreshMission();
				}
				else
				{
					RefreshMission();
				}
				break;
			case 5:
				Mission5();
				myMissionStore[mission2].progress += temp5 - tempM2;
				tempM2 = temp5;
				if (myMissionStore[mission2].progress >= myMissionStore[mission2].score)
				{
					myMissionStore[mission2].progress = myMissionStore[mission2].score;
					myMissionStore[mission2].unclock = 1;
					PlayerPrefs.SetInt("Mission1Progress", myMissionStore[mission2].progress);
					PlayerPrefs.SetInt("Mission1Unlock", myMissionStore[mission2].unclock);
					RefreshMission();
				}
				else
				{
					RefreshMission();
				}
				break;
			case 6:
				if (temp6 >= 1)
				{
					myMissionStore[mission2].progress = myMissionStore[mission2].score;
					myMissionStore[mission2].unclock = 1;
					PlayerPrefs.SetInt("Mission1Progress", myMissionStore[mission2].progress);
					PlayerPrefs.SetInt("Mission1Unlock", myMissionStore[mission2].unclock);
					RefreshMission();
				}
				break;
			case 7:
				myMissionStore[mission2].progress += temp7 - tempM2;
				tempM2 = temp7;
				PlayerPrefs.SetInt("Mission1Progress", myMissionStore[mission2].progress);
				if (myMissionStore[mission2].progress >= myMissionStore[mission2].score)
				{
					myMissionStore[mission2].progress = myMissionStore[mission2].score;
					myMissionStore[mission2].unclock = 1;
					PlayerPrefs.SetInt("Mission1Progress", myMissionStore[mission2].progress);
					PlayerPrefs.SetInt("Mission1Unlock", myMissionStore[mission2].unclock);
					RefreshMission();
				}
				else
				{
					RefreshMission();
				}
				break;
			case 8:
				myMissionStore[mission2].progress += temp8 - tempM2;
				tempM2 = temp8;
				PlayerPrefs.SetInt("Mission1Progress", myMissionStore[mission2].progress);
				if (myMissionStore[mission2].progress >= myMissionStore[mission2].score)
				{
					myMissionStore[mission2].progress = myMissionStore[mission2].score;
					myMissionStore[mission2].unclock = 1;
					PlayerPrefs.SetInt("Mission1Progress", myMissionStore[mission2].progress);
					PlayerPrefs.SetInt("Mission1Unlock", myMissionStore[mission2].unclock);
					RefreshMission();
				}
				else
				{
					RefreshMission();
				}
				break;
			case 9:
				Mission9();
				myMissionStore[mission2].progress += temp9 - tempM2;
				tempM2 = temp9;
				if (myMissionStore[mission2].progress >= myMissionStore[mission2].score)
				{
					myMissionStore[mission2].progress = myMissionStore[mission2].score;
					myMissionStore[mission2].unclock = 1;
					PlayerPrefs.SetInt("Mission1Progress", myMissionStore[mission2].progress);
					PlayerPrefs.SetInt("Mission1Unlock", myMissionStore[mission2].unclock);
					RefreshMission();
				}
				else
				{
					RefreshMission();
				}
				break;
		}
		switch (mission3)
		{
			case 0:
				Mission0();
				myMissionStore[mission3].progress += temp0 - tempM3;
				tempM3 = temp0;
				PlayerPrefs.SetInt("Mission2Progress", myMissionStore[mission3].progress);
				if (myMissionStore[mission3].progress >= myMissionStore[mission3].score)
				{
					myMissionStore[mission3].progress = myMissionStore[mission3].score;
					myMissionStore[mission3].unclock = 1;
					PlayerPrefs.SetInt("Mission2Progress", myMissionStore[mission3].progress);
					PlayerPrefs.SetInt("Mission2Unlock", myMissionStore[mission3].unclock);
					RefreshMission();
				}
				else
				{
					RefreshMission();
				}
				break;
			case 1:
				Mission1();
				myMissionStore[mission3].progress += temp1 - tempM3;
				tempM3 = temp1;
				if (myMissionStore[mission3].progress >= myMissionStore[mission3].score)
				{
					myMissionStore[mission3].progress = myMissionStore[mission3].score;
					myMissionStore[mission3].unclock = 1;
					PlayerPrefs.SetInt("Mission2Progress", myMissionStore[mission3].progress);
					PlayerPrefs.SetInt("Mission2Unlock", myMissionStore[mission3].unclock);
					RefreshMission();
				}
				else
				{
					RefreshMission();
				}
				break;
			case 2:
				Mission2();
				myMissionStore[mission3].progress += temp2 - tempM3;
				tempM3 = temp2;
				PlayerPrefs.SetInt("Mission2Progress", myMissionStore[mission3].progress);
				if (myMissionStore[mission3].progress >= myMissionStore[mission3].score)
				{
					myMissionStore[mission3].progress = myMissionStore[mission3].score;
					myMissionStore[mission3].unclock = 1;
					PlayerPrefs.SetInt("Mission2Progress", myMissionStore[mission3].progress);
					PlayerPrefs.SetInt("Mission2Unlock", myMissionStore[mission3].unclock);
					RefreshMission();
				}
				else
				{
					RefreshMission();
				}
				break;
			case 3:
				Mission3();
				myMissionStore[mission3].progress += temp3 - tempM3;
				tempM3 = temp3;
				if (myMissionStore[mission3].progress >= myMissionStore[mission3].score)
				{
					myMissionStore[mission3].progress = myMissionStore[mission3].score;
					myMissionStore[mission3].unclock = 1;
					PlayerPrefs.SetInt("Mission2Progress", myMissionStore[mission3].progress);
					PlayerPrefs.SetInt("Mission2Unlock", myMissionStore[mission3].unclock);
					RefreshMission();
				}
				else
				{
					RefreshMission();
				}
				break;
			case 4:
				myMissionStore[mission3].progress += temp4 - tempM3;
				tempM3 = temp4;
				PlayerPrefs.SetInt("Mission2Progress", myMissionStore[mission3].progress);
				if (myMissionStore[mission3].progress >= myMissionStore[mission3].score)
				{
					myMissionStore[mission3].progress = myMissionStore[mission3].score;
					myMissionStore[mission3].unclock = 1;
					PlayerPrefs.SetInt("Mission2Progress", myMissionStore[mission3].progress);
					PlayerPrefs.SetInt("Mission2Unlock", myMissionStore[mission3].unclock);
					RefreshMission();
				}
				else
				{
					RefreshMission();
				}
				break;
			case 5:
				Mission5();
				myMissionStore[mission3].progress += temp5 - tempM3;
				tempM3 = temp5;
				if (myMissionStore[mission3].progress >= myMissionStore[mission3].score)
				{
					myMissionStore[mission3].progress = myMissionStore[mission3].score;
					myMissionStore[mission3].unclock = 1;
					PlayerPrefs.SetInt("Mission2Progress", myMissionStore[mission3].progress);
					PlayerPrefs.SetInt("Mission2Unlock", myMissionStore[mission3].unclock);
					RefreshMission();
				}
				else
				{
					RefreshMission();
				}
				break;
			case 6:
				if (temp6 >= 1)
				{
					myMissionStore[mission3].progress = myMissionStore[mission3].score;
					myMissionStore[mission3].unclock = 1;
					PlayerPrefs.SetInt("Mission2Progress", myMissionStore[mission3].progress);
					PlayerPrefs.SetInt("Mission2Unlock", myMissionStore[mission3].unclock);
					RefreshMission();
				}
				break;
			case 7:
				myMissionStore[mission3].progress += temp7 - tempM3;
				tempM3 = temp7;
				PlayerPrefs.SetInt("Mission2Progress", myMissionStore[mission3].progress);
				if (myMissionStore[mission3].progress >= myMissionStore[mission3].score)
				{
					myMissionStore[mission3].progress = myMissionStore[mission3].score;
					myMissionStore[mission3].unclock = 1;
					PlayerPrefs.SetInt("Mission2Progress", myMissionStore[mission3].progress);
					PlayerPrefs.SetInt("Mission2Unlock", myMissionStore[mission3].unclock);
					RefreshMission();
				}
				else
				{
					RefreshMission();
				}
				break;
			case 8:
				myMissionStore[mission3].progress += temp8 - tempM3;
				tempM3 = temp8;
				PlayerPrefs.SetInt("Mission2Progress", myMissionStore[mission3].progress);
				if (myMissionStore[mission3].progress >= myMissionStore[mission3].score)
				{
					myMissionStore[mission3].progress = myMissionStore[mission3].score;
					myMissionStore[mission3].unclock = 1;
					PlayerPrefs.SetInt("Mission2Progress", myMissionStore[mission3].progress);
					PlayerPrefs.SetInt("Mission2Unlock", myMissionStore[mission3].unclock);
					RefreshMission();
				}
				else
				{
					RefreshMission();
				}
				break;
			case 9:
				Mission9();
				myMissionStore[mission3].progress += temp9 - tempM3;
				tempM3 = temp9;
				if (myMissionStore[mission3].progress >= myMissionStore[mission3].score)
				{
					myMissionStore[mission3].progress = myMissionStore[mission3].score;
					myMissionStore[mission3].unclock = 1;
					PlayerPrefs.SetInt("Mission2Progress", myMissionStore[mission3].progress);
					PlayerPrefs.SetInt("Mission2Unlock", myMissionStore[mission3].unclock);
					RefreshMission();
				}
				else
				{
					RefreshMission();
				}
				break;
		}

		Debug.Log("tempy: " + temp + " "+ tempM2 + " "+ tempM3);
	}

	private bool doOnce0 = true;
	private bool doOnce1 = true;
	private bool doOnce2 = true;

	private void ShowPopUP()
	{
		int mission0 = PlayerPrefs.GetInt("Mission0");
		int mission1 = PlayerPrefs.GetInt("Mission1");
		int mission2 = PlayerPrefs.GetInt("Mission2");
		unlocked1 = PlayerPrefs.GetInt("unlocked1");
		unlocked2 = PlayerPrefs.GetInt("unlocked2");
		unlocked3 = PlayerPrefs.GetInt("unlocked3");

		if (myMissionStore[mission0].unclock == 1 && doOnce0 && unlocked1 == 0)
		{
			if (player != null)
			{
				player.MissionCompleted(1);
				doOnce0 = false;
				PlayerPrefs.SetInt("unlocked1", 1);
			}
				
		}
		if (myMissionStore[mission1].unclock == 1 && doOnce1 && unlocked2 == 0)
		{
			if (player != null)
			{
				player.MissionCompleted(2);
				doOnce1 = false;
				PlayerPrefs.SetInt("unlocked2", 1);
			}
				
		}
		if (myMissionStore[mission2].unclock == 1 && doOnce2 && unlocked3 == 0)
		{
			if (player != null)
			{
				player.MissionCompleted(3);
				doOnce2 = false;
				PlayerPrefs.SetInt("unlocked3", 1);
			}
				
		}
	}


}
