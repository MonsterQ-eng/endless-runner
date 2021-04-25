using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuantumTek.EncryptedSave;
using UnityEngine.Audio;
using UnityEngine.UI;

public class musicSett : MonoBehaviour
{
    public AudioSource[] audioSourceArray;
    public AudioClip[] audioClipArray;
    int nextClip = 0;
    int toggle = 0;

    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;
    public Slider slider;
    public Slider sliderSFX;
    public static musicSett sharedInstanceMusic = null;
    private double nextStartTime = 0.5d;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if (sharedInstanceMusic == null)
        {
            sharedInstanceMusic = this;
        }
        else if (sharedInstanceMusic != this)
        {
            Destroy(gameObject);
        }

       if(musicMixer == null)
        {
            musicMixer = Resources.Load<AudioMixer>("MusicMixer");           
        }
       if(sfxMixer == null)
        {
            sfxMixer = Resources.Load<AudioMixer>("SFXMixer");
        }

        if (slider == null)
        {
            slider = GameObject.Find("Slider").GetComponent<Slider>();
            slider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        }
        else
        {
            slider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        }
        if(sliderSFX == null)
        {
            sliderSFX = GameObject.Find("SliderSFX").GetComponent<Slider>();
            sliderSFX.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
        }
        else
        {
            sliderSFX.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
        }

        //AudioClip clipToPlay = audioClipArray[nextClip];

        //// Loads the next Clip to play and schedules when it will start
        //audioSourceArray[toggle].clip = clipToPlay;
        //audioSourceArray[toggle].PlayScheduled(nextStartTime);

        //// Checks how long the Clip will last and updates the Next Start Time with a new value
        //double duration = (double)clipToPlay.samples / clipToPlay.frequency;
        //nextStartTime = nextStartTime + duration;



    }

    public void SetLevel(float sliderValue)
    {
        if(musicMixer == null)
        {
          
            musicMixer = Resources.Load<AudioMixer>("MusicMixer");
            musicMixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
            PlayerPrefs.SetFloat("MusicVolume", sliderValue);
        }
        else
        {
          
            musicMixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
            PlayerPrefs.SetFloat("MusicVolume", sliderValue);
        }
        
       

    }

    public void SetLevelSFX(float sliderValue)
    {
        if(sfxMixer == null)
        {
            sfxMixer = Resources.Load<AudioMixer>("SFXMixer");
            sfxMixer.SetFloat("SFXVol", Mathf.Log10(sliderValue) * 20);
            PlayerPrefs.SetFloat("SFXVolume", sliderValue);
        }
        else
        {
            sfxMixer.SetFloat("SFXVol", Mathf.Log10(sliderValue) * 20);
            PlayerPrefs.SetFloat("SFXVolume", sliderValue);
        }
 
    }

    //void Update()
    //{

    //    if (AudioSettings.dspTime > nextStartTime - 1)
    //    {

    //        AudioClip clipToPlay = audioClipArray[nextClip];

    //        // Loads the next Clip to play and schedules when it will start
    //        audioSourceArray[toggle].clip = clipToPlay;
    //        audioSourceArray[toggle].PlayScheduled(nextStartTime);

    //        // Checks how long the Clip will last and updates the Next Start Time with a new value
    //        double duration = (double)clipToPlay.samples / clipToPlay.frequency;
    //        nextStartTime = nextStartTime + duration;

    //        // Switches the toggle to use the other Audio Source next
    //        toggle = 1 - toggle;

    //        // Increase the clip index number, reset if it runs out of clips
    //        nextClip = nextClip < audioClipArray.Length - 1 ? nextClip + 1 : 0;
    //    }
    //}



}
