using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI scoreText;
    private int score = 0;

    public void IncrementScore()
    {
        score++;
        scoreText.text = "Score: " + score;
    }
}
