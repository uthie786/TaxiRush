using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PassengerPickupController : MonoBehaviour
{
    private Rigidbody rb;
    private float time;
    private GameObject taxi;
    public int totalScore;
    public List<PassengerController> pcArray;
    public TextMeshProUGUI passengerCountText;
    private int passengerCount;
    [SerializeField] private TextMeshProUGUI scoreText;
    
    [SerializeField] private AudioSource pickupSound;
    void Start()
    {
        taxi = gameObject;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        passengerCountText.text = "Passengers: " + passengerCount;
        time += Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
       
        if (other.CompareTag("Passenger"))
        {
            Debug.Log("pass");
            if (rb.velocity.magnitude <= 2f)
            {
                PickupPassenger(other);
                Debug.Log(time);
            }
        }

        if (other.CompareTag("Destination"))
        {
            if (rb.velocity.magnitude <= 2f)
            {
                DropOffPassenger(other);
            }
        }
    }

    void PickupPassenger(Collider passenger)
    {
        pickupSound.Play();
        PassengerController pc = passenger.GetComponent<PassengerController>();
        pc.pickupTime = time;
        pc.gameObject.transform.SetParent(taxi.transform);
        passenger.gameObject.SetActive(false);
        //passenger.GetComponent<MeshRenderer>().enabled = false;
        passenger.GetComponent<CapsuleCollider>().enabled = false;
        pc.SetRandomInactiveDestination();
        pcArray.Add(pc);
        passengerCount++;
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            TutorialManager.Instance.passengerPickedUp = true;
        }
    }

    void DropOffPassenger(Collider destination)
    {
        pickupSound.Play();
        foreach(PassengerController pc in pcArray)
        {
            if (pc.finalDest == destination.gameObject && pc.gameObject != null)
            {
                pc.dropOffTime = time;
                float timeTaken = pc.dropOffTime - pc.pickupTime;
                pc.score = (pc.scoreScale / timeTaken) * 100;
                totalScore += (int)pc.score;
                pcArray.Remove(pc);
                Destroy(pc.gameObject);
                destination.gameObject.SetActive(false);
                scoreText.text = "R" + totalScore.ToString("0.00");
                passengerCount--;
                if (SceneManager.GetActiveScene().name == "Tutorial")
                {
                    TutorialManager.Instance.passengerDroppedOff = true;
                }
            }
        }
    }
}
