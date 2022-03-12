using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : Singleton<GUIManager>
{
    [SerializeField] private Spaceship _spaceship;

    [SerializeField] private Text _coordsText;
    [SerializeField] private Text _angleText;
    [SerializeField] private Text _speedText;
    [SerializeField] private Text _laserChargeText;
    [SerializeField] private Text _laserTimeText;

    [SerializeField] private Text _pointsText;

    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private Text _finalScoreText;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;

    // Start is called before the first frame update
    void Start()
    {
        _restartButton.onClick.AddListener( () => GameManager.Instance.RestartGame() );
        _exitButton.onClick.AddListener( () => GameManager.Instance.ExitGame() );
    }

    // Update is called once per frame
    void Update()
    {
        if (! _spaceship.gameObject.activeSelf) return;

        Vector3 spaceshipCoords = _spaceship.GetCoords();
        _coordsText.text = "Coords: " + spaceshipCoords.x.ToString("F2") + ", " + spaceshipCoords.y.ToString("F2");

        _angleText.text = "Angle: " + _spaceship.GetAngle().ToString("F0");

        Vector3 spaceshipSpeed = _spaceship.GetSpeedVector();
        _speedText.text = "Speed: " + spaceshipSpeed.x.ToString("F2") + ", " + spaceshipSpeed.y.ToString("F2");

        _laserChargeText.text = "Laser charges: " + _spaceship.GetLaserCharges() + " / " + Spaceship.LASER_MAX_SHOTS_COUNT;

        _laserTimeText.text = "Time left: " + _spaceship.GetLaserLeftTime().ToString("F2");

        _pointsText.text = GameManager.Instance.Points.ToString();
    }

    public void ShowGameOverScreen(bool enable)
    {
        _gameOverScreen.SetActive(enable);
        _finalScoreText.text = GameManager.Instance.Points.ToString() + " points";
    }

}
