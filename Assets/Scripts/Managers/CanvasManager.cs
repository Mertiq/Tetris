using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;

    [SerializeField] private TMP_Text scoreText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Start() => SetScoreText();

    public void SetScoreText()
    {
        scoreText.text = $"Your Score: {ScoreManager.Instance.CurrentScore}\n\n" +
                         $"High Score: {ScoreManager.Instance.HighScore}";
    }

    public void RestartButton()
    {
        BoardManager.Instance.GameOver();
        SetScoreText();
    }
}