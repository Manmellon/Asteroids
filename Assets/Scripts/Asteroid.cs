using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MovingObject
{
    [SerializeField] Sprite[] _asteroidSprites;
    [SerializeField] Sprite[] _asteroidFragmentSprites;

    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Collider2D _collider;

    private int _size;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(int size)
    {
        //int r_size = Random.Range(0, 2);
        //_size = r_size;
        _size = size;

        if (_size > 0)
        {
            int r = Random.Range(0, _asteroidSprites.Length);
            _spriteRenderer.sprite = _asteroidSprites[r];
        }

        else
        {
            int r = Random.Range(0, _asteroidFragmentSprites.Length);
            _spriteRenderer.sprite = _asteroidFragmentSprites[r];
        }

        Destroy(_collider);
        _collider = gameObject.AddComponent<CircleCollider2D>();

        float movingAngle = Random.Range(0.0f, 360.0f);
        Vector3 randomDirection = Quaternion.AngleAxis(movingAngle, Vector3.forward) * Vector3.up;
        //Debug.Log(randomDirection);
        SetSpeedVector(randomDirection * MaxSpeed);

        float rotationAngle = Random.Range(0.0f, 360.0f);
        transform.Rotate(Vector3.forward, rotationAngle);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            if (_size > 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    Asteroid asteroid = Instantiate(this, GameManager.Instance.transform);
                    asteroid.SetMaxSpeed(MaxSpeed * 2);
                    asteroid.Init(_size - 1);
                }
            }
            Destroy(gameObject);
        }
    }
}
