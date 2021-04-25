using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject platformGenerator;
    public CameraFollow cameraMain;
    private bool isRestart = false;

    public GameObject canvasGame;


    // Start is called before the first frame update
    void Start()
    {
        canvasGame.transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(canvasGame, new Vector3(1, 1, 1), 0.6f).setEase(LeanTweenType.easeOutBack);
        cameraMain = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
    }

    // Update is called once per frame
    void Update()
    {


        //if (Input.GetKeyDown(KeyCode.R) && isRestart || Input.touchCount > 0 && isRestart)
        //{
        //    Debug.Log("pressed r");
        //    SceneManager.LoadScene(1);
        //}
    }


    public void OnDeath()
    {
        Debug.Log("Death");
        platformGenerator.SetActive(false);
        Debug.Log("platform off");
       // cameraMain.IsGameRun();
        Debug.Log("Camera off");
        isRestart = true;

       
    }

    public void LoadReset()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMainmenu()
    {
        SceneManager.LoadScene(0);
    }



}
