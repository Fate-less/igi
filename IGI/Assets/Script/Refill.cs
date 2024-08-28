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
    public Sprite refillIcon;

    private AudioManager audioManager;
    private bool isInRange = false;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("Audio Handler").GetComponent<AudioManager>();
        if (owner == playerNumber.Player1)
        {
            currency = GameObject.FindGameObjectWithTag("CurrencyP1").GetComponent<Currency>();
        }
        else
        {
            currency = GameObject.FindGameObjectWithTag("CurrencyP2").GetComponent<Currency>();
        }
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
                    Color tmp = player.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
                    tmp.a = 1f;
                    player.transform.GetChild(0).GetComponent<SpriteRenderer>().color = tmp;
                    player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = refillIcon;
                    if (Input.GetKeyDown(KeyCode.Space) && resourceStorage.foodResource < resourceStorage.maxFoodResource)
                    {
                        currency.money -= refillCost;
                        resourceStorage.foodResource += 2;
                        audioManager.audioSource.PlayOneShot(audioManager.buyResource);
                    }
                }
                else
                {
                    player.transform.GetChild(0).gameObject.SetActive(true);
                    Color tmp = player.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
                    tmp.a = 1f;
                    player.transform.GetChild(0).GetComponent<SpriteRenderer>().color = tmp;
                    player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = refillIcon;
                    if (Input.GetKeyDown(KeyCode.RightShift) && resourceStorage.foodResource < resourceStorage.maxFoodResource)
                    {
                        currency.money -= refillCost;
                        resourceStorage.foodResource += 2;
                        audioManager.audioSource.PlayOneShot(audioManager.buyResource);
                    }
                }
            }
            else
            {
                Color tmp = player.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
                tmp.a = 0.5f;
                player.transform.GetChild(0).GetComponent<SpriteRenderer>().color = tmp;
                player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = refillIcon;
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
