using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Transform _playerSpawn;

    private void OnEnterTrigger2D(Collider2D player)
    {
        if (player.tag == "Player")
        {
            VirgilePlayerController._lastCheckPointPos = transform.position;
            Debug.Log("fdfdsfsd");
        }
    }
}