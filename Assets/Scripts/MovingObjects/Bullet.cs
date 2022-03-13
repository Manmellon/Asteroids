using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MovingObject
{
    private UnityAction<int> _AddPointsAction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(UnityAction<int> AddPointsAction, Vector3 direction)
    {
        _AddPointsAction = AddPointsAction;
        SetSpeedVector(direction * MaxSpeed);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Asteroid" || other.gameObject.tag == "UFO")
        {
            _AddPointsAction(1);
            Destroy(gameObject);
        }
    }

}
