using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spaceship : MovingObject
{
    [SerializeField] private PlayerInput _playerInput;

    [SerializeField] private float _slowdown;

    [SerializeField] private Animator _animator;

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
        Vector2 moveActionVector = _moveAction.ReadValue<Vector2>();
        //Debug.Log(moveActionVector);

        if (moveActionVector.y > 0.0f)
        {
            //Accel(moveActionVector.y);
            //Debug.Log(transform.up * moveActionVector.y);
            Accel(transform.up * moveActionVector.y);

            _animator.SetBool("EnabledEngine", true);
        }
        else
        {
            _animator.SetBool("EnabledEngine", false);
        }

        //Accel(- _slowdown);

        Rotate(- moveActionVector.x);
    }
    
    public void ControlsMove(InputAction.CallbackContext context)
    {
        
    }
}
