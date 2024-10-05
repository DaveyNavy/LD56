using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] private AugmentManager augmentManager;

    private int waveCount = 0;
    private const int wavesPerAugment = 3;

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
        transform.position = mousePosition;
    }

    //NOTE: Feel free to change these if we are not using a wave system, this is just an example method we could
    // use for when we could grant buffs
    public void IncrementWave()
    {
        waveCount++;
        if (waveCount % wavesPerAugment == 0)
        {
            GrantRandomAugment();
        }
    }

    private void GrantRandomAugment()
    {
        //May grant speed augment before any other ones, which wouldn't make sense, I'll fix this later
        AugmentManager.AugmentType randomAugment = (AugmentManager.AugmentType)Random.Range(0, 5);
        augmentManager.ApplyAugment(randomAugment);
        Debug.Log("Augment Granted: " + randomAugment);
    }
    void OnSwapRoomOne(InputValue value)
    {
        mainCamera.transform.position = new Vector3(0, 0, -10);
    }

    void OnSwapRoomTwo(InputValue value)
    {
        mainCamera.transform.position = new Vector3(20, 0, -10);
    }
}
