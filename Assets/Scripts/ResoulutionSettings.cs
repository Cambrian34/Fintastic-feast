using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ResoulutionSettings : MonoBehaviour
{
    
    [SerializeField] TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;

    public void Start()
    {
        resolutions = Screen.resolutions; 

        Resolution currentResolution = Screen.currentResolution; 

        for (int i=0; i < resolutions.Length; i++)
        {
            string resolutionString = resolutions[i].width.ToString() + "x" + resolutions[i].height.ToString();

            resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(resolutionString));
        }

        int currentResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", resolutions.Length-1);
        currentResolutionIndex = Math.Min(currentResolutionIndex, resolutions.Length-1);
        resolutionDropdown.value = currentResolutionIndex;
        SetResolution();
    }

    public void SetResolution()
    {
        int resolutionIndex = resolutionDropdown.value;

        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, true);

        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
    }
}
