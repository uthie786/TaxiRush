using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerController : MonoBehaviour
{
    public float pickupTime;
    public float dropOffTime;
    public float score;
    public float scoreScale;
    [SerializeField] GameObject[] destinations;
    public GameObject finalDest;
    private Transform child;

    void Start()
    {
        scoreScale = 3;
        

        if (transform.childCount > 0)
        {
            child = transform.GetChild(0);
        }
    }

     public void SetRandomInactiveDestination()
    {
        List<GameObject> inactiveDestinations = new List<GameObject>();
        foreach (GameObject destination in destinations)
        {
            if (!destination.activeSelf)
            {
                inactiveDestinations.Add(destination);
            }
        }

        if (inactiveDestinations.Count > 0)
        {
            int rand = Random.Range(0, inactiveDestinations.Count);
            finalDest = inactiveDestinations[rand];
            finalDest.SetActive(true);
        }
        else
        {
            Debug.LogWarning("No inactive destinations available.");
        }
        HighlightDestination();
    }

    public void HighlightDestination()
    {
        finalDest.gameObject.SetActive(true);
    }
}