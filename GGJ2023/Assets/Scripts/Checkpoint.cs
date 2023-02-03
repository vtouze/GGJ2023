using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Transform _playerSpawn;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnEnterTrigger2D(Collider2D player)
    {
        if(player.CompareTag("Player"))
        {
            _playerSpawn.position = transform.position;
        }
    }
}
