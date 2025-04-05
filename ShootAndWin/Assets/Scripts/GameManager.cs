using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TextMeshProUGUI scoreText;
    private int score = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddPoints(int value)
    {
        score += value;
        Debug.Log("Score: " + score);
        UpdateScoreText();
    }
    
    void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }
}
