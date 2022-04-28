using System;
using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _followTarget;

    private void Awake()
    {
        Vector3 initialPosition = transform.position;
        _offset = initialPosition - _followTarget.position;
        _fixedY = initialPosition.y;
    }

    private void LateUpdate()
    {
        if (_followTarget == null)
        {
            Destroy(this);
            return;
        }
        Vector3 newPosition = _followTarget.position + _offset;
        newPosition.y = _fixedY;
        transform.position = newPosition;
    }

    private Vector3 _offset;
    private float _fixedY;
}