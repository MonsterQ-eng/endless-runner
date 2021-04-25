using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboutPanel : MonoBehaviour
{


    public GameObject about;
    public GameObject privacy;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }


    public void About()
    {
        privacy.SetActive(false);
        about.SetActive(true);
    }

    public void Privacy()
    {
        privacy.SetActive(true);
        about.SetActive(false);
    }

    public void Exit()
    {
        gameObject.SetActive(false);
    }

}
