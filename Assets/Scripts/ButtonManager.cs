using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject HowToScreen;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void HowToPlayEnable()
    {
        HowToScreen.SetActive(true);
    }

    public void HowToPlayDisable()
    {
        HowToScreen.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
