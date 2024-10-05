using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Camera mainCamera;

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
        transform.position = mousePosition;
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
