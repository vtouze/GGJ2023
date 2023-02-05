using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer = null;
    [SerializeField] private Sprite _spriteOpen = null;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AmibeCharacter._lastCheckPointPos = transform.position;
            Debug.Log("fdfdfdf");
            _spriteRenderer.sprite = _spriteOpen;
        }
    }
}