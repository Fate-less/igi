using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonScript : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MoveToMenu()
    {
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        SceneManager.LoadScene("Mainmenu");
    }
    public void MoveToTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void MoveToGame()
    {
        SceneManager.LoadScene("PlayGame");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
