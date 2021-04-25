using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchivmentyGoogle : MonoBehaviour
{
    public void BoosterAchiv(int i)
    {
        for(int k=0; k<i; k++)
        {
            Debug.Log("BoosterAchiv: " + k);
            if (Social.localUser.authenticated)
            {
                PlayGamesPlatform.Instance.IncrementAchievement("CgkI9IfZjt8OEAIQGQ", 1, (bool success) =>
                {
                    Debug.Log(success);
                });
                PlayGamesPlatform.Instance.IncrementAchievement("CgkI9IfZjt8OEAIQGg", 1, (bool success) =>
                {
                    Debug.Log(success);
                });
                PlayGamesPlatform.Instance.IncrementAchievement("CgkI9IfZjt8OEAIQGw", 1, (bool success) =>
                {
                    Debug.Log(success);
                });
                PlayGamesPlatform.Instance.IncrementAchievement("CgkI9IfZjt8OEAIQHA", 1, (bool success) =>
                {
                    Debug.Log(success);
                });
                PlayGamesPlatform.Instance.IncrementAchievement("CgkI9IfZjt8OEAIQHQ", 1, (bool success) =>
                {
                    Debug.Log(success);
                });
            }
        }
        
    }
    public void EnemyAchiv(int i)
    {
        for (int k = 0; k < i; k++)
        {
            Debug.Log("EnemyAchiv: " + k);
            if (Social.localUser.authenticated)
            {
                PlayGamesPlatform.Instance.IncrementAchievement("CgkI9IfZjt8OEAIQEw", 1, (bool success) =>
                {
                    Debug.Log(success);
                });
                PlayGamesPlatform.Instance.IncrementAchievement("CgkI9IfZjt8OEAIQFA", 1, (bool success) =>
                {
                    Debug.Log(success);
                });
                PlayGamesPlatform.Instance.IncrementAchievement("CgkI9IfZjt8OEAIQFQ", 1, (bool success) =>
                {
                    Debug.Log(success);
                });
                PlayGamesPlatform.Instance.IncrementAchievement("CgkI9IfZjt8OEAIQFg", 1, (bool success) =>
                {
                    Debug.Log(success);
                });
                PlayGamesPlatform.Instance.IncrementAchievement("CgkI9IfZjt8OEAIQFw", 1, (bool success) =>
                {
                    Debug.Log(success);
                });
                PlayGamesPlatform.Instance.IncrementAchievement("CgkI9IfZjt8OEAIQGA", 1, (bool success) =>
                {
                    Debug.Log(success);
                });
            }
        }
        }
        
    }


