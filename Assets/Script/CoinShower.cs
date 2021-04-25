using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinShower : MonoBehaviour
{
    public Text textMesh;
    private Color textColor;
    private float disapperTime = 1f;
    private bool isShownig;

    void Start()
    {
        textColor = textMesh.color;
        gameObject.SetActive(false);
        isShownig = false;
    }


    private void Update()
    {
        if (isShownig)
        {
            disapperTime -= Time.deltaTime;
            if (disapperTime < 0)
            {
                Debug.Log("Znikanie");
                float disappearSpeed = 2.5f;
                textColor.a -= disappearSpeed * Time.deltaTime;
                textMesh.color = textColor;
            }
            if (textColor.a < 0)
            {
                Debug.Log("Znikło");
                gameObject.SetActive(false);
                isShownig = false;
            }
        }

    }
    public void ShowTextCoin(string s)
    {
        gameObject.SetActive(true);
        textMesh.text = "+" + s;
        isShownig = true;
        textColor.a = 255;
    }

}
