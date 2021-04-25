using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class Admob : MonoBehaviour
{
    [SerializeField] private string appID = "ca-app-pub-4631150661042402~8543082073";

    private RewardedAd rewardedAd;
    private InterstitialAd interstitial;
    public Player player;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (GameObject.Find(gameObject.name)
                && GameObject.Find(gameObject.name) != this.gameObject)
        {
            Destroy(GameObject.Find(gameObject.name));
        }
        MobileAds.Initialize(appID);
        RequsetRegularAd();
        RequestRewardAd();
        
    }

    


// REGULAR AD CODE START
    public void RequsetRegularAd()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-4631150661042402/7010508552";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-4631150661042402/7010508552";
#else
        string adUnitId = "unexpected_platform";
#endif

        this.interstitial = new InterstitialAd(adUnitId);

        this.interstitial.OnAdClosed += HandleOnAdClosed;
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        //TEST
        //AdRequest request = new AdRequest.Builder()
        //    .AddTestDevice("2077ef9a63d2b398840261c8221a0c9b")
        //    .Build();
        // REAL
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);

    }



    public void ShowAd() {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
}

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
       // Time.timeScale = 0f;
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {

        interstitial.Destroy();
        RequsetRegularAd();
       // Time.timeScale = 1f;
    }
// REGULAR AD CODE END


// REWARD AD CODE START
    public void RequestRewardAd()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-4631150661042402/9337288990";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-4631150661042402/9337288990";
#else
            string adUnitId = "unexpected_platform";
#endif

        this.rewardedAd = new RewardedAd(adUnitId);

       // this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;

        // Create an empty ad request. TEST
        //AdRequest request = new AdRequest.Builder()
        //    .AddTestDevice("2077ef9a63d2b398840261c8221a0c9b")
        //    .Build();

        //REAL
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }

    //public void HandleRewardedAdOpening(object sender, EventArgs args)
    //{
    //    Time.timeScale = 0f;
    //}


    public bool ReadyAdR()
    {
        if (this.rewardedAd.IsLoaded())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ShowRewardAD()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
        else
        {
            player.AdNotLoad();
        }
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        player.FailToLoad();
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        player.RewardText();
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        this.RequestRewardAd();
        Time.timeScale = 1f;
        player.AdClosed();
    }
// REWARD AD CODE END



}
