using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public float countdownTime = 300f; // 5 minutes in seconds
    public TextMeshProUGUI timerText;
    public GameObject endScreen;

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
        EndGame();
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        //Debug.Log(string.Format("{0:00}:{1:00}", minutes, seconds));
    }

    void EndGame()
    {
        if (!isGameEnded)
        {
            isGameEnded = true;
            Time.timeScale = 0f; // Pause the game
            endScreen.SetActive(true);
        }
    }
}
