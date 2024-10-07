using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject WaspSpawner;

    [SerializeField] GameObject secondRoomAntSpawner;
    [SerializeField] GameObject secondRoomWaspSpawner;

    [SerializeField] GameObject thirdRoomAntSpawner;
    [SerializeField] GameObject thirdRoomWaspSpawner;

    [SerializeField] TextMeshProUGUI secondRoomText;
    [SerializeField] TextMeshProUGUI thirdRoomText;

    [SerializeField] GameObject cockroachSpawner;
    [SerializeField] GameObject waspSpawner;

    bool firstThreshold = false;
    bool secondThreshold = false;
    bool thirdThreshold = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (ScoreManager.Score == 0)
        {
            WaspSpawner.SetActive(false);
            firstThreshold = false;

            secondRoomAntSpawner.SetActive(false);
            secondRoomWaspSpawner.SetActive(false);
            secondRoomText.gameObject.SetActive(false);
            cockroachSpawner.gameObject.SetActive(false);
            secondThreshold = false;

            thirdRoomAntSpawner.SetActive(false);
            thirdRoomWaspSpawner.SetActive(false);
            thirdRoomText.gameObject.SetActive(false);
            waspSpawner.gameObject.SetActive(false);
            thirdThreshold = false;
        }
        if (!firstThreshold && ScoreManager.Score >= 25)
        {
            WaspSpawner.SetActive(true);
            firstThreshold = true;
        }
        if (!secondThreshold && ScoreManager.Score >= 50) {
            secondRoomAntSpawner.SetActive(true);
            secondRoomWaspSpawner.SetActive(true);
            secondRoomText.gameObject.SetActive(true);
            cockroachSpawner.gameObject.SetActive(true);
            secondThreshold = true;
            Invoke("HideText", 3);
        }
        if (!thirdThreshold && ScoreManager.Score >= 200)
        {
            thirdRoomAntSpawner.SetActive(true);
            thirdRoomWaspSpawner.SetActive(true);
            thirdRoomText.gameObject.SetActive(true);
            waspSpawner.gameObject.SetActive(true);
            thirdThreshold = true;
            Invoke("HideText", 3);
        }
    }

    void HideText()
    {
        secondRoomText.gameObject.SetActive(false);
        thirdRoomText.gameObject.SetActive(false);
    }
}
