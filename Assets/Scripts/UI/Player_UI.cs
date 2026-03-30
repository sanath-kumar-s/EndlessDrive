using System.Collections;
using TMPro;
using UnityEngine;

public class Player_UI : MonoBehaviour
{
    public static Player_UI Instance ;

    [SerializeField] private float scorePerSecond = .5f;
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private Animator score_animator;
    [SerializeField] private Animator highScore_animator;
    [SerializeField] private float gameOverMenuOpenDelay = 1f;

    private const string Score_REPOSITION = "Reposition";
    private const string HighScore_REPOSITION = "Reposition";

    private bool doCountScore = true;
    private float score;

    private int scoreInt;

    private void Awake()
    {
        Instance = this;

        score = 0;
        SetDoCountScore(false);

    }

    private void Update()
    {
        ScoreHandler();

        if(PlayerCollisionDetection.Instance.GetIfIsDead())
        {
            StartCoroutine(RepositionScoreText());
        }
    }

    private IEnumerator RepositionScoreText()
    {
        yield return new WaitForSeconds(gameOverMenuOpenDelay);
        score_animator.SetTrigger(Score_REPOSITION);
        highScore_animator.SetTrigger(HighScore_REPOSITION);
    }

    private void ScoreHandler()
    {
        if (PlayerCollisionDetection.Instance.GetIfIsDead()) SetDoCountScore(false);

        if (doCountScore)score += Time.deltaTime * scorePerSecond  ;

        scoreInt = Mathf.FloorToInt(score);

        scoreText.text = "Score: " + scoreInt.ToString();
    }

    public int GetScore()
    {
        return scoreInt;
    }


    public void SetDoCountScore(bool value)
    {
        doCountScore = value;
    }

    public void SetScorePerSecond(float value)
    {
        scorePerSecond = value;
    }
    public float GetScorePerSecond()
    {
        return scorePerSecond;
    }
}
