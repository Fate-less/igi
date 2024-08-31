using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {
    public GameObject mainMenuPanel; // Assign in the Inspector
    public GameObject optionsPanel;  // Assign in the Inspector

    void Start() {
        ShowMainMenu(); // Start with the main menu visible
    }

    public void ShowMainMenu() {
        mainMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void ShowOptions() {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void StartGame() {
        SceneManager.LoadScene("Tutorial"); // Replace with your game scene name
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void SetVolume(float volume) {
        // Adjust the volume based on the slider value
        AudioListener.volume = volume;
    }

    public void SetFullscreen(bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
    }
}
