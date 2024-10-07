using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreManager
{
    public static int Score;
    public static void InitGame()
    {
        Score = 0;
    }

    public static void AddScore(int points)
    {
        Score += points;
    }
}
