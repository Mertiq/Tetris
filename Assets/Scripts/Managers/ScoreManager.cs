using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private int currentScore;

    public int HighScore;

    public int CurrentScore
    {
        get => currentScore;
        set
        {
            currentScore += value;
            CanvasManager.Instance.SetScoreText();
            CheckHighScore();
        }
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        HighScore = PlayerPrefs.GetInt("HighScore");
    }

    private void CheckHighScore()
    {
        if (currentScore <= HighScore) return;
        HighScore = currentScore;
        PlayerPrefs.SetInt("HighScore", currentScore);
        CanvasManager.Instance.SetScoreText();
    }
}