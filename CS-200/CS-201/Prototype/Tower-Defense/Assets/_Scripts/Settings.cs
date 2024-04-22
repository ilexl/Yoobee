using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider audioSlider;
    [SerializeField] TMP_Dropdown graphicsDropdown;
    [SerializeField] Toggle fullscreenToggle;
    [SerializeField] TMP_Dropdown resolutionDropdown;
    [SerializeField] int resolutionIndex = -1;
    Resolution[] resolutionsRaw = new Resolution[] { };
    readonly List<Resolution> resolutionsAllowed = new();
    [SerializeField]List<string> resolutionList = new();
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("LEGNER-STUDIO-GAME*TD*" + "Volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
        int toSave = (fullScreen ? 1 : 0);
        PlayerPrefs.SetInt("LEGNER-STUDIO-GAME*TD*" + "Fullscreen", toSave);
    }

    public void SetResolution(int selectedResolution)
    {
        int width = resolutionsAllowed[selectedResolution].width;
        int height = resolutionsAllowed[selectedResolution].height;
        int refreshRate = resolutionsAllowed[selectedResolution].refreshRate;

        if (!resolutionDropdown.enabled) { return; }

        PlayerPrefs.SetInt("LEGNER-STUDIO-GAME*TD*" + "RefreshRate", refreshRate);
        Screen.SetResolution(width, height, Screen.fullScreen, refreshRate);
        //Debug.Log(width + " x " +  height + " @ " + refreshRate + " , " + Screen.fullScreen);
    }

    private void GetResolutions()
    {
        resolutionDropdown.enabled = false;
        resolutionDropdown.ClearOptions();
        resolutionsAllowed.Clear();
        resolutionList.Clear();
        resolutionsRaw = Screen.resolutions;
        
        foreach (Resolution resolution in resolutionsRaw)
        {
            if (resolution.width % 16 == 0 && resolution.height % 9 == 0) // 16:9 resolutions
            {
                resolutionsAllowed.Add(resolution);
                resolutionList.Add(resolution.width + " x " + resolution.height + " @ " + resolution.refreshRate);
            }
        }
        resolutionDropdown.AddOptions(resolutionList);
        int currentCounter = 0;
        foreach (Resolution resolution in resolutionsAllowed)
        {
            //Debug.Log(resolution);
            if ((resolution.refreshRate == PlayerPrefs.GetInt("LEGNER-STUDIO-GAME*TD*" + "RefreshRate", 60)) && (resolution.width == Screen.width) && (resolution.height == Screen.height))
            {
                resolutionIndex = currentCounter;
                break;
            }
            currentCounter++;
        }
        resolutionDropdown.value = resolutionIndex;
        resolutionDropdown.RefreshShownValue();

        
        resolutionDropdown.enabled = true;
    }

    private void SetValueLoad()
    {
        resolutionIndex = -1;
        graphicsDropdown.value = QualitySettings.GetQualityLevel();
        graphicsDropdown.RefreshShownValue();
        float volume = PlayerPrefs.GetFloat("LEGNER-STUDIO-GAME*TD*" + "Volume", 80);
        audioMixer.SetFloat("volume", volume);
        audioSlider.value = volume;
        fullscreenToggle.isOn = Screen.fullScreen;
        //SetResolution(resolutionIndex);
    }

    private void OnEnable()
    {
        GetResolutions();
        SetValueLoad();
    }

    private void OnDisable()
    {
        GetResolutions();
        SetValueLoad();
    }
}
