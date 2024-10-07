using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DisplayScore : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI scoreText;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + ScoreManager.Score;
    }
}
