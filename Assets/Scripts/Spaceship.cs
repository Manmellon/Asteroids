using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spaceship : MovingObject
{
    [SerializeField] private PlayerInput _playerInput;

    [SerializeField] private float _slowdown;

    [SerializeField] private Animator _animator;

    [SerializeField] private Bullet _bulletPrefab;

    [SerializeField] private LineRenderer _lineRenderer;

    private InputAction _moveAction;
    private InputAction _fireAction;
    private InputAction _laserAction;

    public const float LASER_DISTANCE = 100.0f;
    public const float LASER_DISAPPEAR_TIME = 0.1f;

    public const float LASER_CHARGE_TIME = 5.0f;
    public const int LASER_MAX_SHOTS_COUNT = 3;

    [SerializeField] private int _laserShotsCount;

    private float _startRechargeLaserTime;

    // Start is called before the first frame update
    void Start()
    {
        _moveAction = _playerInput.actions["Move"];
        _fireAction = _playerInput.actions["Fire"];
        _laserAction = _playerInput.actions["Laser"];

        _fireAction.performed += (context) => { Fire(context); };
        _laserAction.performed += (context) => { Laser(context); };

        Init();
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

        //AccelToCurrentDirection(- _slowdown);

        Rotate(- moveActionVector.x);
    }

    public void Init()
    {
        _laserShotsCount = LASER_MAX_SHOTS_COUNT;
    }

    public Vector3 GetCoords()
    {
        return transform.position;
    }

    public float GetAngle()
    {
        return transform.rotation.eulerAngles.z;
    }

    public int GetLaserCharges()
    {
        return _laserShotsCount;
    }

    public float GetLaserLeftTime()
    {
        if (_laserShotsCount >= LASER_MAX_SHOTS_COUNT)
        {
            return 0.0f;
        }
        else
        {
            return LASER_CHARGE_TIME + _startRechargeLaserTime - Time.time;
        }
    }

    public void Fire(InputAction.CallbackContext context)
    {
        Bullet bullet = Instantiate(_bulletPrefab, transform.position, transform.rotation);
        bullet.Init(transform.up);
    }

    public void Laser(InputAction.CallbackContext context)
    {
        if (_laserShotsCount <= 0) return;
        //Debug.Log("Laser");
        _laserShotsCount--;

        StopCoroutine("RechargeLaser");
        StartCoroutine("RechargeLaser");

        _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, transform.position + transform.up * LASER_DISTANCE);

        StartCoroutine("DisableLaser");

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.up, LASER_DISTANCE);

        for (int i = 0; i < hits.Length; i++)
        {
            //Debug.Log(hits[i].collider.tag);
            if (hits[i].collider.tag == "Asteroid" || hits[i].collider.tag == "UFO")
            {
                Destroy(hits[i].collider.gameObject);
                GameManager.Instance.AddPoints(1);
            }
        }
    }

    private IEnumerator DisableLaser()
    {
        yield return new WaitForSeconds(LASER_DISAPPEAR_TIME);
        _lineRenderer.enabled = false;
    }

    private IEnumerator RechargeLaser()
    {
        _startRechargeLaserTime = Time.time;

        yield return new WaitForSeconds(LASER_CHARGE_TIME);

        _laserShotsCount++;
        if (_laserShotsCount < LASER_MAX_SHOTS_COUNT)
        {
            StartCoroutine("RechargeLaser");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Asteroid" || other.gameObject.tag == "UFO")
        {
            GameManager.Instance.GameOver();
        }
    }
}
