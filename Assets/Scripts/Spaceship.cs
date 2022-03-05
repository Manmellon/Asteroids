using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spaceship : MovingObject
{
    [SerializeField] PlayerInput _playerInput;

    [SerializeField] private float _slowdown;

    private InputAction _moveAction;

    private void Awake()
    {
        _moveAction = _playerInput.actions["Move"];
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movingVector = _moveAction.ReadValue<Vector2>();
        Debug.Log(movingVector);

        if (movingVector.y > 0.0f)
        {
            Accel(movingVector.y);
        }

        Accel(- _slowdown);

        Rotate(- movingVector.x);
    }
    
    public void ControlsMove(InputAction.CallbackContext context)
    {
        
    }
}
