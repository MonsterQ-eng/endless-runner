using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyRewardExit : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }
}
