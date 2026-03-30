using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class UI_MainMenu : MonoBehaviour
{
    public static UI_MainMenu Instance;

    public event EventHandler OnPlayButtonPressed;

    public event EventHandler OnSettingsOpened;

    private PlayerInputActions _input;

    [SerializeField] private Transform playerMenu;
    [SerializeField] private Transform GameStartCountDownMenu;
    [SerializeField] private Transform obstacleSpawner;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Transform StartCountDownText;

    [SerializeField] private Transform SettingsMenu;

    [SerializeField] private float actionWaitTime = 1f;

    [SerializeField] private Animator startButton_animator;
    private const string startButton_EXIT = "Exit";

    [SerializeField] private Animator mainMenu_animator;
    private const string mainMenu_EXIT = "Exit";


    private void Awake()
    {
        _input = new PlayerInputActions();

        Instance = this;
        SettingsMenu.gameObject.SetActive(false);

    }




    public void StartGame()
    {
        StartCoroutine(StartGameIEnumarator());

    }

    private IEnumerator StartGameIEnumarator()
    {
        startButton_animator.SetTrigger(startButton_EXIT);
        mainMenu_animator.SetTrigger(mainMenu_EXIT);
        OnPlayButtonPressed?.Invoke(this, EventArgs.Empty);
        settingsMenu.SetActive(false);

        yield return new WaitForSeconds(actionWaitTime);

        playerMenu.gameObject.SetActive(true);
        GameStartCountDownMenu.gameObject.SetActive(true);
        obstacleSpawner.gameObject.SetActive(true);

        ObstacleSpawner _obstracleSpawner = obstacleSpawner.GetComponent<ObstacleSpawner>();

        _obstracleSpawner.enabled = true;


        gameManager.enabled = true;

        gameObject.SetActive(false);
    }

    public void OpenSettingsMenu()
    {
        OnSettingsOpened?.Invoke(this, EventArgs.Empty);

        SettingsMenu.gameObject.SetActive(true);

    }
}
