using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject stage1Hint;
    [SerializeField] private GameObject stage2Hint;
    [SerializeField] private GameObject stage3Hint;
    [SerializeField] private TextMeshProUGUI countdownText;
    public bool passengerPickedUp = false;
    public bool passengerDroppedOff = false;
    private int timer = 0;
    private static TutorialManager _instance;
    public static TutorialManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TutorialManager>();
            }
            return _instance;
        }
    }
    void Start()
    {
        stage1Hint.SetActive(true);
        stage2Hint.SetActive(false);
        stage3Hint.SetActive(false);
    }
    void Update()
    {
        if (passengerPickedUp)
        {
            stage1Hint.SetActive(false);
            stage2Hint.SetActive(true);
            stage3Hint.SetActive(false);
        }

        if (passengerDroppedOff)
        {
            stage1Hint.SetActive(false);
            stage2Hint.SetActive(false);
            stage3Hint.SetActive(true);
            StartCoroutine(CloseTutorial());

        }
    }

    IEnumerator CloseTutorial()
    {
        countdownText.text = "Returning to MainMenu" + "\n" + "3";
        yield return new WaitForSeconds(1);
        countdownText.text = "Returning to MainMenu:" + "\n" + "2";
        yield return new WaitForSeconds(1);
        countdownText.text = "Returning to MainMenu" + "\n" + "1";
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("MainMenu");
    }
    
}
