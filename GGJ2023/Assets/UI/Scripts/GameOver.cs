using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _desktop;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void DisplayQuit()
    {
        _mainMenu.SetActive(true);
        _desktop.SetActive(true);

    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Desktop()
    {
        Application.Quit();
    }

    public void Retry()
    {
        SceneManager.LoadScene("VirgileTest");
    }

    /*public void ClearDisplay()
    {
        if(_mainMenu == true && _desktop == true)
        {
            _mainMenu.SetActive(false);
            _desktop.SetActive(false);
        }
    }*/
}
