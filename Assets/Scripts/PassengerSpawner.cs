using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] passengerPrefabs; // Array of passenger prefabs
    [SerializeField] private Transform[] spawnPoints;

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            SpawnPassenger();
        }
        StartCoroutine(SpawnPassengerRoutine());
    }

    void SpawnPassenger()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        int randomPrefabIndex = Random.Range(0, passengerPrefabs.Length); // Randomly select a prefab
        GameObject passengerPrefab = passengerPrefabs[randomPrefabIndex];

        Instantiate(passengerPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    IEnumerator SpawnPassengerRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            SpawnPassenger();
        }
    }
}
