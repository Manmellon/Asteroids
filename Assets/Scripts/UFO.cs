using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MovingObject
{
    [SerializeField] Spaceship _spaceship;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetSpeedVector((_spaceship.transform.position - transform.position) * MaxSpeed);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        }
    }
}
