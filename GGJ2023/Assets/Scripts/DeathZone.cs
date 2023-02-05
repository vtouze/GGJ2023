using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu = null;
    //[SerializeField] private GameObject _spawn = null;
    [SerializeField] private GameObject _gameOver = null;
    [SerializeField] private Transform _player;

    void Start()
    {
        _gameOver.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D player)
    {
        if (player.tag == "Player")
        {
            _gameOver.SetActive(true);
            Time.timeScale = 0;

        }
    }

    public void Retry()
    {
            _gameOver.SetActive(false);
            Time.timeScale = 1;
            VirgilePlayerController._lastCheckPointPos = transform.position;
    }
}