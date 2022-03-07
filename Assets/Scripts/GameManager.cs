using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance => _instance;

    [SerializeField] Asteroid _asteroidPrefab;

    private int _points;

    // Start is called before the first frame update
    void Start()
    {
        _instance = this as GameManager;

        for (int i=0; i<5; i++)
        {
            Asteroid asteroid = Instantiate(_asteroidPrefab);
            asteroid.Init(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
    }
}
