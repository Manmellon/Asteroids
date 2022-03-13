using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] PlayerInput _playerInput;

    [SerializeField] private BoundingBox _boundingBox;

    [SerializeField] private Asteroid _asteroidPrefab;
    [SerializeField] private UFO _ufoPrefab;
    [SerializeField] private Spaceship _spaceship;

    private int _points;
    public int Points => _points;

    private const float MIN_UFO_SPAWN_TIME = 10.0f;
    private const float MAX_UFO_SPAWN_TIME = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Init()
    {
        _points = 0;

        _spaceship.Init();

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        SpawnWave();
        StartCoroutine("SpawnUFO");
    }

    private Vector2 ChooseRandomPosition()
    {
        float min_x, max_x, min_y, max_y;

        if (_spaceship.transform.position.x > 0)
        {
            min_x = _boundingBox.Box.bounds.min.x;
            max_x = 0;
        }
        else
        {
            min_x = 0;
            max_x = _boundingBox.Box.bounds.max.x;
        }

        if (_spaceship.transform.position.y > 0)
        {
            min_y = _boundingBox.Box.bounds.min.y;
            max_y = 0;
        }
        else
        {
            min_y = 0;
            max_y = _boundingBox.Box.bounds.max.y;
        }


        //Vector2 random_position = new Vector2(Random.Range(_boundingBox.Box.bounds.min.x, _boundingBox.Box.bounds.max.x),
        //                                      Random.Range(_boundingBox.Box.bounds.min.y, _boundingBox.Box.bounds.max.y));

        Vector2 random_position = new Vector2(Random.Range(min_x, max_x),
                                              Random.Range(min_y, max_y));
        return random_position;
    }

    private void SpawnWave()
    {
        for (int i = 0; i < 5; i++)
        {
            Asteroid asteroid = Instantiate(_asteroidPrefab, ChooseRandomPosition(), _asteroidPrefab.transform.rotation, transform);
            asteroid.Init(1);
        }
    }

    private void CheckWave()
    {
        if (transform.childCount <= 1)
        {
            SpawnWave();
        }
    }

    private IEnumerator SpawnUFO()
    {
        float random_time = Random.Range(MIN_UFO_SPAWN_TIME, MAX_UFO_SPAWN_TIME);
        yield return new WaitForSeconds(random_time);

        UFO ufo = Instantiate(_ufoPrefab, ChooseRandomPosition(), _ufoPrefab.transform.rotation, transform);
        ufo.Init(_spaceship);

        StartCoroutine("SpawnUFO");
    }

    public void AddPoints(int addingPoints)
    {
        _points += addingPoints;
        CheckWave();
    }

    public void GameOver()
    {
        _playerInput.actions.Disable();
        GUIManager.Instance.ShowGameOverScreen(true);
        StopAllCoroutines();
    }

    public void RestartGame()
    {
        GUIManager.Instance.ShowGameOverScreen(false);
        Init();
        _playerInput.actions.Enable();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
