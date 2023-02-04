using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Transform _playerSpawn;

    private void OnEnterTrigger2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            VirgilePlayerController._lastCheckPointPos = transform.position;
            Debug.Log("fdfdsfsd");
        }
    }
}