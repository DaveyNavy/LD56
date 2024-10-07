using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    bool stopTime = false;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject powerUp; 

    void Awake()
    {


        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this )
        {
            Destroy(gameObject);
        }
        ScoreManager.InitGame();

        InvokeRepeating("SpawnPowerup", 1, 1);
    }

    void Update()
    {
        scoreText.text = "Score: " + ScoreManager.Score;
    }

    public void StopTime()
    {
        stopTime = true;
        Invoke("RestartTime", 3);
    }

    private void RestartTime()
    {
        stopTime = false;
    }

    public bool IsTimeStopped()
    {
        return stopTime;
    }

    private void SpawnPowerup()
    {
        if (ScoreManager.Score > 100)
        {
            if (Random.Range(0,7) == 0)
            {
                float x = Random.Range(0, 3) * 20;
                Instantiate(powerUp, new Vector3(x, 0, 0), Quaternion.identity);
            }
        }
    }
}