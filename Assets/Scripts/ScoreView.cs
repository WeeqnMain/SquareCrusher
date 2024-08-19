using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private int score = 0;

    private int scoreMultiplier = 1;

    private void Awake()
    {
        UpdateScore(0);
    }

    public void UpdateScore(int value)
    {
        score += value * scoreMultiplier;
        scoreText.text = $"{score}";
    }

    public void SetScoreDoubled()
    {
        scoreMultiplier = 2;
    }

    public void SetScoreNormal()
    {
        scoreMultiplier = 1;
    }
}