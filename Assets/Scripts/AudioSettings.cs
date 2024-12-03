using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider masterVolumeSlider;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider sfxVolumeSlider;

    string masterVolumeKey = "MasterVolume";
    string musicVolumeKey = "MusicVolume";
    string sfxVolumeKey = "SFXVolume";

    public void Start()
    {
        masterVolumeSlider.value = PlayerPrefs.GetFloat(masterVolumeKey, 0.5f);
        //musicVolumeSlider.value = PlayerPrefs.GetFloat(musicVolumeKey, 0.5f);
        //sfxVolumeSlider.value = PlayerPrefs.GetFloat(sfxVolumeKey, 0.5f);

        SetMasterVolume();
        //SetMusicVolume();
        //SetSFXVolume();
    }

    public void SetMasterVolume()
    {
        SetVolume(masterVolumeKey, masterVolumeSlider.value);
        PlayerPrefs.SetFloat(masterVolumeKey, masterVolumeSlider.value);

    }

    public void SetMusicVolume()
    {
        SetVolume(musicVolumeKey, musicVolumeSlider.value);
        PlayerPrefs.SetFloat(musicVolumeKey, musicVolumeSlider.value);
    }

    public void SetSFXVolume()
    {
        SetVolume(sfxVolumeKey, sfxVolumeSlider.value);
        PlayerPrefs.SetFloat(sfxVolumeKey, sfxVolumeSlider.value);
    }
    
    public void SetVolume(string groupName, float value)
    {
        float adjustedVolume = Mathf.Log10(value) * 20;
        if (value == 0)
        {
            adjustedVolume = -80f;
        }

        audioMixer.SetFloat(groupName, adjustedVolume);
    }
}