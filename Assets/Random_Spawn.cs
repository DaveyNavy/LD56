using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_Spawner : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    // Reference to the object you want to spawn
    public GameObject objectToSpawn;

    // Time between spawns
    public float spawnInterval = 1f;

    // Spawn area boundaries
    public float spawnAreaSize = 10f;

    // Minimum distance from the center
    public float minDistanceFromCenter = 3f;

    // Maximum number of objects to spawn
    public int maxSpawnCount = 9999;

    private int currentSpawnCount = 0;

    private void Start()
    {
        // Start the coroutine to spawn objects
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        while (currentSpawnCount < maxSpawnCount)
        {
            SpawnObjectAtRandom();
            currentSpawnCount++;
            yield return new WaitForSeconds(spawnInterval); // Wait for specified seconds
        }
    }

    private void SpawnObjectAtRandom()
    {
        Vector3 randomPosition;

        do
        {
            randomPosition = new Vector3(
                Mathf.Pow(-1, Random.Range(1,2)) * Random.Range(3, spawnAreaSize),
                Mathf.Pow(-1, Random.Range(1, 2)) * Random.Range(3, spawnAreaSize),
                0
            ) + offset;
        } 
        while (randomPosition.magnitude < minDistanceFromCenter); // Ensure it is far from the center

        Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
    }
}