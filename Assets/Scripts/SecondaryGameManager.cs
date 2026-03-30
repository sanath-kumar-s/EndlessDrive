using UnityEngine;

public class SecondaryGameManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private ObstacleSpawner _obstacleSpawner;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject audioManager;

    private void Awake()
    {


        if (_playerMovement != null) _playerMovement.enabled = false;
        if(_obstacleSpawner != null)_obstacleSpawner.enabled = false;
        if (_gameManager != null) _gameManager.enabled = false; 

    }

    private void Update()
    {
        audioManager.SetActive(true);
    }
}
