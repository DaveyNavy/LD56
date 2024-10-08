using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene("Main_Room");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CreditsMenu()
    {
        SceneManager.LoadScene("CreditScene");
    } 

    public void PlayAgain()
    {
        SceneManager.LoadScene("Main_Menu");
        ScoreManager.InitGame();
    }
    
}
