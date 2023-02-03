using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float _lengthX = 0;
    private float _lengthY = 0;

    private float _startposX = 0;
    private float _startposY = 0;

    [SerializeField] private Camera _camera = null;
    [SerializeField] private float _parralaxEffectHorizontal = 0;
    [SerializeField] private float _parralaxEffectVertical = 0;



    private void Start()
    {
        _startposX = transform.position.x;
        _startposY = transform.position.y;

        _lengthX = GetComponent<SpriteRenderer>().bounds.size.x;
        _lengthY = GetComponent<SpriteRenderer>().bounds.size.y; ;

    }

    void Update()
    {
        float tempX = (_camera.transform.position.x * (1 - _parralaxEffectHorizontal));
        float tempY = (_camera.transform.position.y * (1 - _parralaxEffectVertical));


        float distX = (_camera.transform.position.x * _parralaxEffectHorizontal);
        float distY = (_camera.transform.position.y * _parralaxEffectVertical);


        transform.position = new Vector3(_startposX + distX, _startposY + distY, transform.position.z);

        if (tempX > _startposX + _lengthX) _startposX += _lengthX;
        else if (tempX < _startposX - _lengthX) _startposX -= _lengthX;

      

    }
}
