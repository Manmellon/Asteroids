using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

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

    private UnityAction<int> _AddPointsAction;
    private UnityAction _GameOverAction;

    // Start is called before the first frame update
    void Start()
    {
        _moveAction = _playerInput.actions["Move"];
        _fireAction = _playerInput.actions["Fire"];
        _laserAction = _playerInput.actions["Laser"];

        _fireAction.performed += (context) => { Fire(context); };
        _laserAction.performed += (context) => { Laser(context); };
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveActionVector = _moveAction.ReadValue<Vector2>();

        if (moveActionVector.y > 0.0f)
        {
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

    public void Init(UnityAction<int> AddPointsAction, UnityAction GameOverAction)
    {
        _AddPointsAction = AddPointsAction;
        _GameOverAction = GameOverAction;

        gameObject.SetActive(true);

        transform.position = new Vector3(0, 0, 0);
        SetSpeedVector(new Vector3(0, 0, 0));

        _laserShotsCount = LASER_MAX_SHOTS_COUNT;

        _animator.SetBool("EnabledEngine", false);
        _lineRenderer.enabled = false;
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
        bullet.Init(_AddPointsAction, transform.up);
    }

    public void Laser(InputAction.CallbackContext context)
    {
        if (_laserShotsCount <= 0) return;

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
            if (hits[i].collider.tag == "Asteroid" || hits[i].collider.tag == "UFO")
            {
                Destroy(hits[i].collider.gameObject);
                _AddPointsAction(1);
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
            _GameOverAction();
            gameObject.SetActive(false);
        }
    }
}
