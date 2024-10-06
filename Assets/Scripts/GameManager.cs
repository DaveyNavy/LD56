using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] TextMeshProUGUI scoreText;
    int score = 0;

    void Awake()
    {

        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);

            instance = this;
        }

        else if (instance != this)
        {

            Destroy(gameObject);
        }
    }

    void Update()
    {
        scoreText.text = "Score: " + score;
    }

    public void IncreaseScore(int s)
    {
        score += s;
    }
}