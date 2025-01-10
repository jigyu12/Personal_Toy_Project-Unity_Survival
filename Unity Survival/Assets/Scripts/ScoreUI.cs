using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    int score;
    TMP_Text scoreText;
    
    void Start()
    {
        score = 0;
        
        scoreText = GetComponent<TMP_Text>();
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        
        scoreText.text = $"SCORE : {score}";
    }
}
