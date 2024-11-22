using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    
    public void StartTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    
    
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void ResumeGame()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
