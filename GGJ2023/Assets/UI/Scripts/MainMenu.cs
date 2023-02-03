using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _title = null;
    [SerializeField] private GameObject _mainMenu = null;
    [SerializeField] private GameObject _settingsMenu = null;
    [SerializeField] private Image _fadeImage = null;
    [SerializeField] private Animator _fadeAnimation = null;

    void Start()
    {
        _title.SetActive(true);
        _mainMenu.SetActive(true);
        _settingsMenu.SetActive(false);
        //_fadeAnimation.SetBool("Fade", true);
    }
    IEnumerator Fading()
    {
        yield return new WaitUntil(()=>_fadeImage.color.a == 1);
        _fadeAnimation.SetBool("Fade", true);
    }

    public void Play()
    {
        StartCoroutine(Fading());
        SceneManager.LoadScene("SampleScene");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OpenSettings()
    {
        StartCoroutine(Fading());
        _title.SetActive(false);
        _mainMenu.SetActive(false);
        _settingsMenu.SetActive(true);
    }
}