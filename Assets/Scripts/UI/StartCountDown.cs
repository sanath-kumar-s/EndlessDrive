using System;
using TMPro;
using UnityEngine;

public class StartCountDown : MonoBehaviour
{
    public static StartCountDown Instance;

    public event EventHandler OnCountDownValueChanged;

    [SerializeField] private TextMeshProUGUI countDownText;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform settingsMenu;
    private const string VALUE_CHANGED = "ChangedValue";

    private int previousTimeToStart = -1;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameManager.Instance.OnGameStarted += GameManager_OnGameStarted;
    }

    private void GameManager_OnGameStarted(object sender, EventArgs e)
    {
    }

    private void Update()
    {
        float rawTime = GameManager.Instance.GetTimeToStart();
        int timeToStart = Mathf.FloorToInt(rawTime);

        if (timeToStart != previousTimeToStart)
        {
            // Value changed
            if(timeToStart >= 0)OnCountDownValueChanged?.Invoke(this, EventArgs.Empty);

            if (_animator != null)
            {
                //_animator.SetTrigger(VALUE_CHANGED);
            }

            countDownText.text = timeToStart.ToString();
            previousTimeToStart = timeToStart;
        }

        if (rawTime <= 0f)
        {
            gameObject.SetActive(false);
        }
    }
}
