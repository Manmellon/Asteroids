using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _minSpeed;

    //[SerializeField] private float _speed;
    [SerializeField] private Vector3 _speedVector;

    [SerializeField] private float _maxAccel;
    [SerializeField] private float _maxRotation;

    [SerializeField] private Collider2D _collider;
    [SerializeField] private BoxCollider2D _boundingBox;

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position += transform.up * _speed * Time.fixedDeltaTime;
        transform.position += _speedVector * Time.fixedDeltaTime;

        /*if (! _collider.IsTouching(_boundingBox))
        {
            if (transform.position.x < _boundingBox.transform.position.x)
            {
                transform.position = new Vector3(_boundingBox.transform.position.x + _boundingBox.);
            }
        }*/
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
    }
}
