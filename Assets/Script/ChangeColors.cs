using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColors : MonoBehaviour
{
    public Text sprite;

    int count = 0;

    void Start()
    {
        StartCoroutine(Renk());

        sprite = GetComponent<Text>();
    }


    void Update()
    {

    }

    IEnumerator Renk()
    {
        while (count < 100)
        {
            yield return new WaitForSeconds(0.50f);

            sprite.color = new Color

                (

            Random.value,
             Random.value,
              Random.value

                );
            count += 1;

        }
    }
}
