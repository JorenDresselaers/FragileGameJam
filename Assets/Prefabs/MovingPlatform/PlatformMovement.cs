using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{

    [SerializeField] private Vector2 _endPos;
    [SerializeField] private float _speed = 1;
    private Vector2 _startPos;
    private int _direction = 1;

    void Start()
    {
        _startPos = transform.position;
    }

    void Update()
    {
        Vector2 pos = transform.position;
        Vector2 toEndPos = _endPos - pos;
        if (Vector2.Distance(pos, _endPos) < 0.1f)
        {
            _endPos = _startPos;
            _startPos = pos;
        }

        pos += toEndPos.normalized * _speed * Time.deltaTime;
        transform.position = pos;
    }
}
