using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cthulhu : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _cthulhu = null;
    [SerializeField] private Sprite _cthulhuFly = null;
    [SerializeField] private float _delay = 0f;
    [SerializeField] private float _timeStamp = 5;

 
    void Start()
    {
        
    }

    void Update()
    {
        _timeStamp += Time.deltaTime;
        if (_timeStamp >= _delay)
        {
            _cthulhu.sprite = _cthulhuFly;
        }
    }
}
