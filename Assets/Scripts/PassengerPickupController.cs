using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PassengerPickupController : MonoBehaviour
{
    private Rigidbody rb;
    private float time;
    private GameObject taxi;
    public int totalScore;
    public int scoreGained;
    public int scoreLost;
    public List<PassengerController> pcArray;
    public TextMeshProUGUI passengerCountText;
    private int passengerCount;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private AudioSource pickupSound;
    [SerializeField] private TimerController timerController;

    void Start()
    {
        taxi = gameObject;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        passengerCountText.text = "Passengers: " + passengerCount;
        time += Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Passenger"))
        {
            if (rb.velocity.magnitude <= 2f)
            {
                PickupPassenger(other);
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
        foreach (PassengerController pc in pcArray)
        {
            if (pc.finalDest == destination.gameObject && pc.gameObject != null)
            {
                pc.dropOffTime = time;
                float timeTaken = pc.dropOffTime - pc.pickupTime;
                pc.score = (pc.scoreScale / timeTaken) * 100;
                totalScore += (int)pc.score;
                scoreGained += (int)pc.score;
                pcArray.Remove(pc);
                Destroy(pc.gameObject);
                destination.gameObject.SetActive(false);
                scoreText.text = "R" + totalScore.ToString("0.00");
                StartCoroutine(FlickerScoreText(Color.green));
                passengerCount--;
                if (SceneManager.GetActiveScene().name == "Tutorial")
                {
                    TutorialManager.Instance.passengerDroppedOff = true;
                }

                timerController.countdownTime += 25f;
                StartCoroutine(FlickerTimeText());


            }
        }
    }

    IEnumerator FlickerTimeText()
    {
        Color orinalColor = timerController.timerText.color;
        timerController.timerText.color = Color.green;
        yield return new WaitForSeconds(0.75f);
        timerController.timerText.color = orinalColor;
    }
    IEnumerator FlickerScoreText(Color flickerColor)
    {
        Color originalColor = scoreText.color;
        scoreText.color = flickerColor;
        yield return new WaitForSeconds(0.75f);
        scoreText.color = originalColor;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 48f) // Adjust the threshold as needed
        {
            totalScore -= 5;
            scoreLost += 5;
            scoreText.text = "R" + totalScore.ToString("0.00");
            StartCoroutine(FlickerScoreText(Color.red));
        }
    }

    public void EndGame()
    {
        timerController.EndGame(totalScore, scoreGained, scoreLost, passengerCount, time);
    }
}