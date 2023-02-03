using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _spawn;

    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.tag == "Player")
        {
            player.transform.position = _spawn.transform.position;
        }
        Debug.Log("dfdfdf");
    }
}