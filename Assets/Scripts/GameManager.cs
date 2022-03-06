using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;

    public static GameManager Instance => _instance;

    // Start is called before the first frame update
    void Start()
    {
        _instance = this as GameManager;
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
