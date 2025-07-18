using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public playerNumber owner;
    public float health = 5f;
    public TextMeshProUGUI winloseTMP;
    public GameObject[] ratingIMG;

    public void healthDecrease()
    {
        health--;
        try
        {
            ratingIMG[(int)health].SetActive(false);
        }
        catch { }
    }

    private void Update()
    {
        if(health <= 0)
        {
            if(owner == playerNumber.Player1)
            {
                winloseTMP.gameObject.SetActive(true);
                winloseTMP.text = "RED WIN";
                StartCoroutine(MoveToScene());
            }
            else
            {
                winloseTMP.gameObject.SetActive(true);
                winloseTMP.text = "BLUE WIN";
                StartCoroutine(MoveToScene());
            }
        }
    }

    private IEnumerator MoveToScene()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Mainmenu");
    }
}
