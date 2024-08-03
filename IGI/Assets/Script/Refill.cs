using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Refill : MonoBehaviour
{
    public playerNumber owner;
    public float refillCost;
    public Currency currency;
    public Resource resourceStorage;

    private bool isInRange = false;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        currency = GameObject.Find("Handler").GetComponent<Currency>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange)
        {
            if (currency.money >= refillCost)
            {
                if (owner == playerNumber.Player1)
                {
                    player.transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>().text = "Press Space to Refill";
                    if (Input.GetKeyDown(KeyCode.Space) && resourceStorage.foodResource < resourceStorage.maxFoodResource)
                    {
                        currency.money -= refillCost;
                        resourceStorage.foodResource += 1;
                    }
                }
                else
                {
                    player.transform.GetChild(0).gameObject.SetActive(true);
                    player.transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>().text = "Press Right Shift to Refill";
                    if (Input.GetKeyDown(KeyCode.RightShift) && resourceStorage.foodResource < resourceStorage.maxFoodResource)
                    {
                        currency.money -= refillCost;
                        resourceStorage.foodResource += 1;
                    }
                }
            }
            else
            {
                player.transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>().text = "You need " + refillCost.ToString() + " to refill";
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(other.GetComponent<CharMovement>().player == owner)
            {
                isInRange = true;
                player = other.gameObject;
                player.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<CharMovement>().player == owner)
            {
                isInRange = false;
                player.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}
