using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _minSpeed;

    [SerializeField] private float _speed;

    [SerializeField] private float _maxAccel;
    [SerializeField] private float _maxRotation;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.up * _speed * Time.fixedDeltaTime;
    }

    public void Accel(float acceleration)
    {
        _speed += acceleration * _maxAccel;
        if (_speed > _maxSpeed)
        {
            _speed = _maxSpeed;
        }
        else if (_speed < _minSpeed)
        {
            _speed = _minSpeed;
        }
    }

    public void Rotate(float angle)
    {
        transform.Rotate(Vector3.forward, angle * _maxRotation);
    }
}
