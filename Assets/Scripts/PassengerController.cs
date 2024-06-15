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
    private int rand;
    void Start()
    {
        scoreScale = 3;
        rand = Random.Range(0, 1);
        finalDest = destinations[rand];

        if (transform.childCount > 0)
        {
            child = transform.GetChild(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HighlightDestination()
    {
        finalDest.gameObject.SetActive(true);
    }
    
}
