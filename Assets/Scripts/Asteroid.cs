using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MovingObject
{
    [SerializeField] Sprite[] _asteroidSprites;
    [SerializeField] Sprite[] _asteroidFragmentSprites;

    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Collider2D _collider;

    // Start is called before the first frame update
    void Start()
    {
        int r = Random.Range(0, _asteroidSprites.Length);
        _spriteRenderer.sprite = _asteroidSprites[r];

        _collider = gameObject.AddComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
