using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject passengerPrefab;
    [SerializeField] private Transform[] spawnPoints;

    void Start()
    {
        SpawnPassenger();
    }

    void SpawnPassenger()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];
        Instantiate(passengerPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
