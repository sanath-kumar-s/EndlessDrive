using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnGameStarted;

    [SerializeField] private float gameStartDelayMax = 3f;
    [SerializeField] private float primaryMoveSpeed = 5f;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private float primaryDelay = 1.5f;
    

    private float timer;

    private bool gameStarted = false;
    private void Awake()
    {
        Instance = this;
    }



    private void Update()
    {
        StartCoroutine(PrimaryDelay());


    }

    private IEnumerator PrimaryDelay()
    {
        yield return new WaitForSeconds(primaryDelay);
        StartDelayManager();

    }

    private void StartDelayManager()
    {
        if (gameStarted) return;

        gameStartDelayMax -= Time.deltaTime;

        if (gameStartDelayMax > 0)
        {
            SettingVariableContainer.Instance.SetMoveSpeed(0);

            _playerMovement.enabled = false;


            if (Player_UI.Instance != null)Player_UI.Instance.SetDoCountScore(false);
        }
        else
        {
            //game Started

            OnGameStarted?.Invoke(this, EventArgs.Empty);

            CarAudioManager.Instance.carStarted = true;

            SettingVariableContainer.Instance.SetMoveSpeed(primaryMoveSpeed);
            _playerMovement.enabled = true;

            gameStarted = true;
            if (Player_UI.Instance != null) Player_UI.Instance.SetDoCountScore(true);

        }
    }


    public float GetTimeToStart()
    {
        return gameStartDelayMax;
    }
}
