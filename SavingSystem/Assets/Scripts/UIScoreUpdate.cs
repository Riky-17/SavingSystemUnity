using TMPro;
using UnityEngine;

public class UIScoreUpdate : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    void OnEnable()
    {
        Player.OnScoreGained += OnScoreUpdate;
    }

    void OnDisable()
    {
        Player.OnScoreGained -= OnScoreUpdate;
    }

    void OnScoreUpdate(int score)
    {
        scoreText.text = $"Score: {score}";
    }
}
