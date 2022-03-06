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

    // Start is called before the first frame update
    void Start()
    {
        _moveAction = _playerInput.actions["Move"];
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Asteroid" || other.gameObject.tag == "UFO")
        {
            GameManager.Instance.GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}
