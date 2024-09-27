using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseScript : MonoBehaviour
{
    public bool isPaused;
    public GameObject pauseScreen;
    public Tutorial tutorialScript;

    private void Start()
    {
        Time.timeScale = 0;
        isPaused = true;
        pauseScreen.SetActive(true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Time.timeScale = 0;
                isPaused = true;
                pauseScreen.SetActive(true);
            }
            else
            {
                if (!tutorialScript.isOn)
                {
                    Time.timeScale = 1;
                    isPaused = false;
                    pauseScreen.SetActive(false);
                }
            }
        }
    }
}