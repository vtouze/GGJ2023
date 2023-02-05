using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using TMPro;


public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private GameObject _title = null;
    [SerializeField] private GameObject _mainMenu = null;
    [SerializeField] private GameObject _settingsMenu = null;
    [SerializeField] private Slider _brightnessSlider = null;
    [SerializeField] private Toggle _vSyncToggle = null;
    [SerializeField] private Toggle _fullScreenToggle = null;

    public PostProcessProfile _brightness;
    public PostProcessLayer _layer;

    AutoExposure _exposure;

    public List<ResolutionIndex> _resolutions = new List<ResolutionIndex>();
    public int _selectedResolutions;
    public TMP_Text _resolutionsText = null;
    

    void Start()
    {
        _brightness.TryGetSettings(out _exposure);
        SetBrightness(_brightnessSlider.value);

        if(QualitySettings.vSyncCount == 0)
        {
            _vSyncToggle.isOn = false;
        }
        else
        {
            _vSyncToggle.isOn = true;
        }
    }

    void Update()
    {
        
    }

    public void Back()
    {
        _title.SetActive(true);
        _mainMenu.SetActive(true);
        _settingsMenu.SetActive(false);
    }

    public void SetFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetBrightness(float value)
    {
        if(value != 0)
        {
            _exposure.keyValue.value = value;
        }
        else
        {
            _exposure.keyValue.value = .05f;
        }
    }

    public void SetVsync()
    {
        if(_vSyncToggle.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
    }

    public void ResolutionsInf()
    {
        _selectedResolutions--;
        if(_selectedResolutions < 0)
        {
            _selectedResolutions = 0;
        }
        SetResolutionText();
    }

    public void ResolutionSup()
    {
        _selectedResolutions++;
        if(_selectedResolutions > _resolutions.Count - 1)
        {
            _selectedResolutions = _resolutions.Count - 1;
        }
        SetResolutionText();
    }

    public void SetResolutionText()
    {
        _resolutionsText.text = _resolutions[_selectedResolutions].horizontal.ToString() + "x" + _resolutions[_selectedResolutions].vertical.ToString();
    }

    public void SetResolution()
    {
        Screen.SetResolution(_resolutions[_selectedResolutions].horizontal, _resolutions[_selectedResolutions].vertical, _fullScreenToggle.isOn);
    }
}

[System.Serializable]
public class ResolutionIndex
{
    public int horizontal;
    public int vertical;
}