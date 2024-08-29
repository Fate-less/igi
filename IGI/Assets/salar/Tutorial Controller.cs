using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour {
    public GameObject[] tutorialPages; // Array of tutorial pages
    private int currentPageIndex = 0;

    public Button nextButton; // Assign this in the Inspector
    public Button backButton; // Assign this in the Inspector
    public Button finishButton; // Assign this in the Inspector

    void Start() {
        ShowPage(currentPageIndex);

        // Add listeners to the buttons
        nextButton.onClick.AddListener(NextPage);
        backButton.onClick.AddListener(PreviousPage);
        finishButton.onClick.AddListener(FinishTutorial);
    }

    void ShowPage(int index) {
        for (int i = 0; i < tutorialPages.Length; i++) {
            tutorialPages[i].SetActive(i == index);
        }

        // Enable/Disable buttons based on current page
        nextButton.gameObject.SetActive(currentPageIndex < tutorialPages.Length - 1);
        backButton.gameObject.SetActive(currentPageIndex > 0);
        finishButton.gameObject.SetActive(currentPageIndex == tutorialPages.Length - 1);
    }

    public void NextPage() {
        if (currentPageIndex < tutorialPages.Length - 1) {
            currentPageIndex++;
            ShowPage(currentPageIndex);
        }
    }

    public void PreviousPage() {
        if (currentPageIndex > 0) {
            currentPageIndex--;
            ShowPage(currentPageIndex);
        }
    }

    public void FinishTutorial() {
        // Load the next scene or start the game
        Debug.Log("Tutorial Finished!");
        SceneManager.LoadScene("PlayGame");
    }
}
