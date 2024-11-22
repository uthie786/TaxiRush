using System.Collections;
using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public float countdownTime; 
    public TextMeshProUGUI timerText;
    public GameObject endScreen;
    public TextMeshProUGUI endScreenText;
    public PassengerPickupController ppc;
    
    private float currentTime;
    private bool isGameEnded = false;

    void Start()
    {
        currentTime = countdownTime;
        endScreen.SetActive(false);
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f);
            currentTime--;
            UpdateTimerText();
        }
        ppc.EndGame(); // Default values if the game ends due to time running out
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void EndGame(int totalScore, int scoreGained, int scoreLost, int passengersTransported, float gameDuration)
    {
        if (!isGameEnded)
        {
            isGameEnded = true;
            Time.timeScale = 0f; // Pause the game
            endScreen.SetActive(true);
            endScreenText.text = $"Game Duration: {gameDuration:F2} seconds\nTotal: {totalScore}\nRands Gained: {scoreGained}\nRands Lost: {scoreLost}\nPassengers Transported: {passengersTransported}";
        }
    }
}