using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WiaterTextSet : MonoBehaviour
{

    public Text wiaterText;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void StartCouri()
    {
        StartCoroutine(TextShower());
    }

    IEnumerator TextShower()
    {
        gameObject.SetActive(true);
        wiaterText.text = "3";
        yield return new WaitForSecondsRealtime(1);
        wiaterText.text = "2";
        yield return new WaitForSecondsRealtime(1);
        wiaterText.text = "1";
        yield return new WaitForSecondsRealtime(1);
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
