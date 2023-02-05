using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax2 : MonoBehaviour
{
   

    [SerializeField] private Camera _camera = null;

    [SerializeField] private float _parralaxEffectHorizontal = 0.3f;
    [SerializeField] private float _parralaxEffectVertical = 0.1f;

    [SerializeField] private bool _lockY = true;



    private void Start()
    {


    }

    void Update()
    {
        if(_lockY)
        {
            transform.position = new Vector2(_camera.transform.position.x * _parralaxEffectHorizontal, transform.position.y);

        }
        else
        {
            transform.position = new Vector2(_camera.transform.position.x * _parralaxEffectHorizontal, _camera.transform.position.y * _parralaxEffectVertical);

        }
    



    }
}
