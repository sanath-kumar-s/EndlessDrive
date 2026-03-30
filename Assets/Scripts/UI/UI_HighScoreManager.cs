using System;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UI_HighScoreManager : MonoBehaviour
{
    public static UI_HighScoreManager Instance;
    
    public event EventHandler OnNewHighScore;

    private int currentScore;

    private int highScore;

    private const string HighScoreKey = "HighScore";

    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private Animator highScore_animator;
    [SerializeField] private bool SetHighScoreToZeroOnAwake = false;
    private const string HighScore_NEW_HIGHSCORE = "NewHighScore";

    private bool gotNewHighScore;
    private bool animationPlayer = false;

    private void Awake()
    {
        gotNewHighScore = false;
        if(SetHighScoreToZeroOnAwake) PlayerPrefs.SetInt(HighScoreKey, 0);

        highScore = PlayerPrefs.GetInt(HighScoreKey);

        highScoreText.text = "HIGHSCORE: " + highScore.ToString();


    }

    private void Update()
    {

        currentScore = Player_UI.Instance.GetScore();

        if(currentScore > highScore)
        {
            //Got new highscore
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();

            highScoreText.text = "HIGHSCORE: " + highScore.ToString();
            gotNewHighScore = true;


        }


        if (gotNewHighScore && !animationPlayer)
        {
            //Play new high score effects
            highScore_animator.SetTrigger(HighScore_NEW_HIGHSCORE);
            OnNewHighScore?.Invoke(this, EventArgs.Empty);

            animationPlayer = true;
        }



    }
}
