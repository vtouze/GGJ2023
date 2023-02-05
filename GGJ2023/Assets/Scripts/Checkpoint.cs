using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public void OnEnterTrigger2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            VirgilePlayerController._lastCheckPointPos = transform.position;
            Debug.Log("fdfdsfsd");
        }
        Debug.Log("1212121");
    }
}