using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public void LoadMainmenu()
    {
        SceneManager.LoadScene(0);
    }

}
