using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Resource : MonoBehaviour
{
    public playerNumber owner;
    public float foodResource;
    public float maxFoodResource = 10;
    public Image healthbar;

    private GameObject player;

    public bool isInRange;

    private void Update()
    {
        healthbar.fillAmount = foodResource / maxFoodResource;
        if (isInRange)
        {
            if(foodResource > 0)
            {
                if (owner == playerNumber.Player1)
                {
                    player.transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>().text = "Press Space to take resource";
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        player.transform.GetChild(1).gameObject.SetActive(true);
                    }
                }
                else
                {
                    player.transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>().text = "Press Right Shift to take resource";
                    if (Input.GetKeyDown(KeyCode.RightShift))
                    {
                        player.transform.GetChild(1).gameObject.SetActive(true);
                    }
                }
            }
            else
            {
                player.transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>().text = "Your resource is empty";
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!other.transform.GetChild(1).gameObject.activeInHierarchy)
            {
                if (owner == other.GetComponent<CharMovement>().player)
                {
                    player = other.gameObject;
                    isInRange = true;
                    player.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (owner == other.GetComponent<CharMovement>().player)
            {
                player = other.gameObject;
                isInRange = false;
                player.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}
