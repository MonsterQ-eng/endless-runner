using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsGenerator : MonoBehaviour
{

    private string[] tipsText = { "You can do double jump! Just TAP and TAP!", "If you SWIPE DOWN you will fall faster!", "If you fall on your opponent, you will defeat them!", "Defeating enemies gives you coins!", "Don't forget to do Daily Challange!", "Missions help you get a higher score!", "Don't forget collect Daily Reward!" };
    public Text uiText;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        index = Random.Range(0, tipsText.Length);
        uiText.text = "Tips: " + tipsText[index];
    }


}
