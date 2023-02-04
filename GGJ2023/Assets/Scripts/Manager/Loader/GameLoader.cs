using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{

    [SerializeField] private GameManager _gameManager = null;
    [SerializeField] private AudioManager _audioManager = null;

    [SerializeField] string _sceneToLoadAtStart = "Game";


    private void Start()
    {
        _gameManager.Initialize();
        _audioManager.Initialize();
        SceneManager.LoadScene(_sceneToLoadAtStart, LoadSceneMode.Single);
    }

}

