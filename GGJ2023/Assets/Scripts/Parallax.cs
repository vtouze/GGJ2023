using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float _length = 0;
    private float _startpos = 0;
    [SerializeField] private Camera _camera = null;
    [SerializeField] private float _parralaxEffect = 0;

    private void Start()
    {
        _startpos = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float temp = (_camera.transform.position.x * (1 - _parralaxEffect));

        float dist = (_camera.transform.position.x * _parralaxEffect);

        transform.position = new Vector3(_startpos + dist, transform.position.y, transform.position.z);

        if (temp > _startpos + _length) _startpos += _length;
        else if (temp < _startpos - _length) _startpos -= _length;
    }
}
