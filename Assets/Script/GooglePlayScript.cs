using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using QuantumTek.EncryptedSave;
using System.Text;
using System;
using UnityEngine.SceneManagement;
using System.Linq;

public class GooglePlayScript : MonoBehaviour
{


    public GameObject logingGP;
    public GameObject okButton;
    public Text logingText;
    string username;
    private bool isSaving;

    public GameObject first;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if (GameObject.Find(gameObject.name)
                && GameObject.Find(gameObject.name) != this.gameObject)
        {
            Destroy(GameObject.Find(gameObject.name));
        }
       logingGP.gameObject.SetActive(true);
        AuthenticateUser();
        
    }


    void AuthenticateUser()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success, string err) =>
        {
            if (success == true)
            {
                LeanTween.scale(first, new Vector3(0, 0, 0), 0.3f).setOnComplete(TurnOff);
                ((GooglePlayGames.PlayGamesPlatform)Social.Active).SetGravityForPopups(Gravity.TOP);
            }
            else
            {
                Debug.Log("Error= " + err);
                logingText.text = "Logowanie nie powiodlo sie!";
                okButton.gameObject.SetActive(true);
            }
        });
    }

    private void TurnOff()
    {
        logingGP.gameObject.SetActive(false);
    }



    // Leaderboard
    public static void PostScore(long score)
    {
        Social.ReportScore(score, "CgkI9IfZjt8OEAIQAA", (bool success) =>
        {
            if (success == true)
            {
                Debug.Log("Added!");          //FULL
            }
            else
            {
                Debug.Log("error no added!" + success);
            }
        });
        //Social.ReportScore(score, "CgkI9IfZjt8OEAIQHg", (bool success) =>
        //{
        //    if (success == true)
        //    {
        //        Debug.Log("Added!");    //BETA
        //    }
        //    else
        //    {
        //        Debug.Log("error no added!" + success);
        //    }
        //});
    }


    public static void ShowLeaderBoard()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkI9IfZjt8OEAIQAA");  // Full
       // PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkI9IfZjt8OEAIQHg"); //BETA
    }


    //Achivments

    public void ShowAchievements()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowAchievementsUI();
        }
        else
        {
            Debug.Log("Cannot show Achievements, not logged in");
        }
    }

    // Sing Out
    public void SingOut()
    {
        if (Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.SignOut();
            SceneManager.LoadScene(0);
        }
    }


    string MakeSaveString()//finalScore:money:selectedSkin:ABFB:UB:DSB:JBB:skinList
    {
        int finalScore = ES_Save.Load<int>("finalScore");
        float money = ES_Save.Load<float>("money");
        int selectedSkinNEW = ES_Save.Load<int>("selectedSkinNEW");
        bool[] allBoosterFasterBool = ES_Save.Load<bool[]>("allBoosterFasterBool");

        string allBoosterFasterBoolS = string.Join(",", values: allBoosterFasterBool.Select(x => x ? "1" : "0"));

        bool[] ubreakableBool = ES_Save.Load<bool[]>("ubreakableBool");

        string ubreakableBoolS = string.Join(",", values: ubreakableBool.Select(x => x ? "1" : "0"));

        bool[] doubleScoreBooster = ES_Save.Load<bool[]>("doubleScoreBooster");

        string doubleScoreBoosterS = string.Join(",", values: doubleScoreBooster.Select(x => x ? "1" : "0"));

        bool[] jumpBoosterBool = ES_Save.Load<bool[]>("jumpBoosterBool");

        string jumpBoosterBoolS = string.Join(",", values: jumpBoosterBool.Select(x => x ? "1" : "0"));

        bool[] skinlistNEW = ES_Save.Load<bool[]>("skinlistNEW");

        string skinlistNEWS = string.Join(",", values: skinlistNEW.Select(x => x ? "1" : "0"));

        bool[] coinMagnetBool = ES_Save.Load<bool[]>("coinMagnetBool");

        string coinMagnetBoolS = string.Join(",", values: coinMagnetBool.Select(x => x ? "1" : "0"));

        int playerLevel = ES_Save.Load<int>("playerLevel");

        string s = finalScore.ToString() + ":" +
                   money.ToString() + ":" +
                   selectedSkinNEW.ToString() + ":" +
                   allBoosterFasterBoolS + ":" +
                   ubreakableBoolS + ":" +
                   doubleScoreBoosterS + ":" +
                   jumpBoosterBoolS + ":" +
                   skinlistNEWS + ":" + 
                   coinMagnetBoolS + ":" +
                   playerLevel.ToString();
        return s;
    }

    byte[] ToBytes()
    {
        return System.Text.ASCIIEncoding.Default.GetBytes(MakeSaveString());
    }
    string FromBytes(byte[] b)
    {
        return System.Text.ASCIIEncoding.Default.GetString(b);
    }



    void LoadFromSaveString(string s)
    {
        string[] data = s.Split(new char[] { ':' });
        int finalScore = Int32.Parse(data[0]);
        float money = float.Parse(data[1]);
        int selectedSkinNEW = Int32.Parse(data[2]);
        bool[] allBoosterFasterBool = data[3].Split(',').Select(x => Convert.ToBoolean(int.Parse(x))).ToArray();
        bool[] ubreakableBool = data[4].Split(',').Select(x => Convert.ToBoolean(int.Parse(x))).ToArray();
        bool[] doubleScoreBooster = data[5].Split(',').Select(x => Convert.ToBoolean(int.Parse(x))).ToArray();
        bool[] jumpBoosterBool = data[6].Split(',').Select(x => Convert.ToBoolean(int.Parse(x))).ToArray();
        bool[] skinlistNEW = data[7].Split(',').Select(x => Convert.ToBoolean(int.Parse(x))).ToArray();
        if(data[8] != null)
        {
            bool[] coinMagnetBool = data[8].Split(',').Select(x => Convert.ToBoolean(int.Parse(x))).ToArray();
            ES_Save.Save(coinMagnetBool, "coinMagnetBool");
        }

        if (data[9] != null || data[9] != "") 
        {
            int playerLevel = Int32.Parse(data[9]);
            ES_Save.Save(playerLevel, "playerLevel");
        }

        ES_Save.Save(finalScore, "finalScore");
        ES_Save.Save(money, "money");
        ES_Save.Save(selectedSkinNEW, "selectedSkinNEW");
        ES_Save.Save(allBoosterFasterBool, "allBoosterFasterBool");
        ES_Save.Save(ubreakableBool, "ubreakableBool");
        ES_Save.Save(doubleScoreBooster, "doubleScoreBooster");
        ES_Save.Save(jumpBoosterBool, "jumpBoosterBool");
        ES_Save.Save(skinlistNEW, "skinlistNEW");
        
       

        SceneManager.LoadScene(0);

    }

    public void SaveToCloud()
    {
        if (Authenticated)
        {
            // Cloud save is not in ISocialPlatform, it's a Play Games extension,
            // so we have to break the abstraction and use PlayGamesPlatform:
            Debug.Log("Saving progress to the cloud...");
            isSaving = true;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution(
                     "saveFile",
                     DataSource.ReadCacheOrNetwork,
                     ConflictResolutionStrategy.UseLongestPlaytime,
                     SaveFileOpened);
            _ShowAndroidToastMessage("Saving Game!");
        }
    }

        public void LoadFromCloud()
    {
        if (Authenticated)
        {
            // Cloud save is not in ISocialPlatform, it's a Play Games extension,
            // so we have to break the abstraction and use PlayGamesPlatform:
            Debug.Log("Loading progress from cloud");
            isSaving = false;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution(
                     "saveFile",
                     DataSource.ReadCacheOrNetwork,
                     ConflictResolutionStrategy.UseLongestPlaytime,
                     SaveFileOpened);
            _ShowAndroidToastMessage("Wait until game reload!");
        }
    }

    void SaveFileOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        //if we have opened the file then we are either saving or loading
        if (status == SavedGameRequestStatus.Success)
        {
            if (isSaving) //writting save
            {
                byte[] data = ToBytes();
                //Can update metadata with played time description and screenshoot
                SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
                SavedGameMetadataUpdate updatedMetadata = builder.Build();
                ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(game, updatedMetadata, data, WroteSavedGame);
            }
            else // loading save
            {
                ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(game, LoadedSavedGame);
            }

        }
        else
        {
            Debug.LogWarning("Error opening game: " + status);
        }
    }

    public void LoadedSavedGame(SavedGameRequestStatus status, byte[] data)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("SaveGameLoaded, success=" + status);
            LoadFromSaveString(FromBytes(data));
        }
        else
        {
            Debug.LogWarning("Error reading game: " + status);
        }
    }
    public void WroteSavedGame(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Game " + game.Description + " written");
            _ShowAndroidToastMessage("Game Saved!");
        }
        else
        {
            Debug.LogWarning("Error saving game: " + status);
        }
    }
    public bool Authenticated
    {
        get
        {
            return Social.Active.localUser.authenticated;
        }
    }


    private void _ShowAndroidToastMessage(string message)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        if (unityActivity != null)
        {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 0);
                toastObject.Call("show");
            }));
        }
    }
}
