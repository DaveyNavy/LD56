using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    int score = 0;
    void Update()
    {
        scoreText.text = "Score: " + score;
    }

    public void IncreaseScore(int s)
    {
        score += s;
    }
}