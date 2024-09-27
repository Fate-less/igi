using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject tutorialScreen;
    [SerializeField] GameObject[] allScene;
    public bool isOn = false;
    private int sceneIndex = 0;

    private void Start()
    {
        StartCoroutine(delayOneFrame());
    }
    private void Update()
    {
        if (isOn)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                allScene[sceneIndex].SetActive(false);
                tutorialScreen.SetActive(false);
                isOn = false;
            }
        }
    }

    public void OpenTutorial()
    {
        if (!isOn)
        {
            tutorialScreen.SetActive(true);
            isOn = true;
            sceneIndex = 0;
            allScene[0].SetActive(true);
        }
    }

    public void NextPage()
    {
        if(sceneIndex == 4) { }
        else
        {
            allScene[sceneIndex].SetActive(false);
            sceneIndex++;
            allScene[sceneIndex].SetActive(true);
        }
    }
    public void PrevPage()
    {
        if(sceneIndex == 0) { }
        else
        {
            allScene[sceneIndex].SetActive(false);
            sceneIndex--;
            allScene[sceneIndex].SetActive(true);
        }
    }

    IEnumerator delayOneFrame()
    {
        yield return null;
        Debug.Log("open tutorial");
        OpenTutorial();
    }
}
