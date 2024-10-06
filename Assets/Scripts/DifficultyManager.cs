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

    bool firstThreshold = false;
    bool secondThreshold = false;
    bool thirdThreshold = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!firstThreshold && GameManager.instance.GetScore() >= 25)
        {
            WaspSpawner.SetActive(true);
            firstThreshold = true;
        }
        if (!secondThreshold && GameManager.instance.GetScore() >= 50) {
            secondRoomAntSpawner.SetActive(true);
            secondRoomWaspSpawner.SetActive(true);
            secondRoomText.gameObject.SetActive(true);
            secondThreshold = true;
            Invoke("HideText", 3);
        }
        if (!thirdThreshold && GameManager.instance.GetScore() >= 200)
        {
            thirdRoomAntSpawner.SetActive(true);
            thirdRoomWaspSpawner.SetActive(true);
            thirdRoomText.gameObject.SetActive(true);
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
