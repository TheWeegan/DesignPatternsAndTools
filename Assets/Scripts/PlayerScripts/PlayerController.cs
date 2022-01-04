using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject _gameHandler;
    [SerializeField] GameObject _gameOverCanvas;

    [SerializeField] Text _scoreText;

    [SerializeField] int _scoreAmount = 1;

    [SerializeField] float _scoreCooldown = 0.1f;
    [SerializeField] float _movementSpeed = 5;
    
    int _playerScore = 0;
    float _scoreTimer = 0;

    string _borderTag = "Border";

    void Start()
    {
        _scoreTimer = _scoreCooldown;
        _scoreText.text = "Score: " + _playerScore.ToString();
    }

    void Update()
    {
        UpdateScore();
        ManageMovement();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(_borderTag))
        {
            PlayerDied();
        }
    }

    void UpdateScore()
    {
        if (_scoreTimer > 0)
        {
            _scoreTimer -= Time.deltaTime;
        }
        else
        {
            _scoreTimer = _scoreCooldown;
            _playerScore += _scoreAmount;
            _scoreText.text = "Score: " + _playerScore.ToString();
        }
    }

    void ManageMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        transform.position += new Vector3(horizontal, vertical, 0) * _movementSpeed * Time.deltaTime;
    }

    void PlayerDied()
    {
        _gameOverCanvas.SetActive(true);
        _gameOverCanvas.GetComponentInChildren<Text>().text = "Final score: " + _playerScore.ToString();
        _gameHandler.SetActive(false);   
    }
}
