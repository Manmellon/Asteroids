using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private float _maxSpeed;
    public float MaxSpeed => _maxSpeed;

    [SerializeField] private float _minSpeed;

    //[SerializeField] private float _speed;
    [SerializeField] private Vector3 _speedVector;

    [SerializeField] private float _maxAccel;
    [SerializeField] private float _maxRotation;

    [SerializeField] private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position += _speedVector * Time.fixedDeltaTime;
        _rigidbody.MovePosition(transform.position + _speedVector * Time.fixedDeltaTime);
    }

    private void LimitSpeed()
    {
        float _speed = _speedVector.magnitude;

        if (_speed > _maxSpeed)
        {
            _speed = _maxSpeed;
        }
        else if (_speed < _minSpeed)
        {
            _speed = _minSpeed;
        }

        _speedVector.Normalize();
        _speedVector *= _speed;
    }

    public void SetMaxSpeed(float newMaxSpeed)
    {
        _maxSpeed = newMaxSpeed;
    }

    public void SetSpeedVector(Vector3 newSpeedVector)
    {
        _speedVector = newSpeedVector;

        LimitSpeed();
    }

    public void AccelToCurrentDirection(float acceleration)
    {
        float _speed = _speedVector.magnitude;

        _speed += acceleration * _maxAccel;

        LimitSpeed();    
    }

    public void Accel(Vector3 accelVector)
    {
        _speedVector += accelVector.normalized * _maxAccel;

        LimitSpeed();
    }

    public void Rotate(float angle)
    {
        transform.Rotate(Vector3.forward, angle * _maxRotation);
        //_rigidbody.MoveRotation(angle * _maxRotation);
    }
}
