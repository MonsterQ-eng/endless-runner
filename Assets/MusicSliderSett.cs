using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSliderSett : MonoBehaviour
{

    public Slider slider;
    public Slider sliderSFX;

    public musicSett musicSett;

    void Start()
    {

        musicSett = GameObject.Find("Music").GetComponent<musicSett>();

        if (slider.onValueChanged == null)
        {
            slider.onValueChanged.AddListener(musicSett.SetLevel);
        }
        else
        {
            slider.onValueChanged.RemoveAllListeners();
            slider.onValueChanged.AddListener(musicSett.SetLevel);
        }
        if (sliderSFX.onValueChanged == null)
        {
            sliderSFX.onValueChanged.AddListener(musicSett.SetLevelSFX);
        }
        else
        {
            sliderSFX.onValueChanged.RemoveAllListeners();
            sliderSFX.onValueChanged.AddListener(musicSett.SetLevelSFX);
        }
    }

}
