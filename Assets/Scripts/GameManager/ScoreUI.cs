using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public TMP_Text scoreText;

    void Start()
    {
        // Ensure that there's a score manager present in the scene.
        if (ScoreManager.Instance != null)
        {
            // Subscribe to the OnScoreChanged event.
            ScoreManager.Instance.OnScoreChanged += UpdateScoreUI;
        }
    }

    void OnDestroy()
    {
        // Always unsubscribe from the event when the object is destroyed to prevent memory leaks.
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged -= UpdateScoreUI;
        }
    }

    private void UpdateScoreUI(int newScore)
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {newScore}";
        }
    }
}
