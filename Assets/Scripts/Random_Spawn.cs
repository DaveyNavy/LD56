using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_Spawn : MonoBehaviour
{
    public GameObject objectToSpawn;

    public float spawnInterval;

    public float baseSpawnInterval;

    public float spawnAreaSize = 10f;

    public float minDistanceFromCenter = 3f;

    public int maxSpawnCount = 9999;

    private int currentSpawnCount = 0;

    [SerializeField] Vector3 offset;

    Vector3 bottomLeft;
    Vector3 topRight;

    private void Start()
    {
        Camera camera = Camera.main;
        if (camera == null) return;

        bottomLeft = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
        topRight = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));
        Debug.Log(topRight);

        StartCoroutine(SpawnObjects());
    }

    private void Update()
    {
        spawnInterval = baseSpawnInterval - 0.2f * ((int) GameManager.instance.GetScore() / 250);
    }

    private IEnumerator SpawnObjects()
    {
        while (currentSpawnCount < maxSpawnCount)
        {
            SpawnObjectAtRandom();
            currentSpawnCount++;
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnObjectAtRandom()
    {
        float x = 0;
        float y = 0;

        int edge = Random.Range(0, 4);
        switch (edge)
        {
            case 0: // Bottom edge
                x = Random.Range(bottomLeft.x, topRight.x);
                y = bottomLeft.y;
                break;
            case 1: // Top edge
                x = Random.Range(bottomLeft.x, topRight.x);
                y = topRight.y;
                break;
            case 2: // Left edge
                x = bottomLeft.x;
                y = Random.Range(bottomLeft.y, topRight.y);
                break;
            case 3: // Right edge
                x = topRight.x;
                y = Random.Range(bottomLeft.y, topRight.y);
                break;
        }

        Vector3 randomPosition = new Vector3(x, y, 0) + offset;

        if (!GameManager.instance.IsTimeStopped())
            Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
    }
}