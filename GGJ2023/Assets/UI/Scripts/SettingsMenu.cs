using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private GameObject _title = null;
    [SerializeField] private GameObject _mainMenu = null;
    [SerializeField] private GameObject _settingsMenu = null;
    void Start()
    {
        
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
}
