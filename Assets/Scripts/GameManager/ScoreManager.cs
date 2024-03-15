using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public event System.Action<int> OnScoreChanged;

    private int score;

    void Awake()
    {
        // ensure that there is only one instance.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    // Method to add score and invoke the score changed event.
    public void AddScore(int amount)
    {
        Debug.Log("Add 1 points");
        score += amount;
        OnScoreChanged?.Invoke(score);
    }


    // Method to get the current score
    public int GetCurrentScore()
    {
        return score;
    }

    public void ResetScore()
    {
        score = 0;
    }
}
