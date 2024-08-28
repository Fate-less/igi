using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeStorage : MonoBehaviour
{
    public playerNumber owner;
    public float upgradeCost;
    public Resource resource;
    public Currency currency;
    public Sprite upgradeStorageIcon;

    private GameObject player;
    private bool isInRange = false;
    private bool isUpgraded;
    private AudioManager audioManager;

    private void Start()
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
        if (owner == playerNumber.Player1)
        {
            resource = GameObject.FindGameObjectWithTag("Storage1").GetComponent<Resource>();
        }
        else
        {
            resource = GameObject.FindGameObjectWithTag("Storage2").GetComponent<Resource>();
        }
    }

    private void Update()
    {
        if (isInRange && !isUpgraded)
        {
            if(currency.money > upgradeCost)
            {
                if(owner == playerNumber.Player1)
                {
                    Color tmp = player.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
                    tmp.a = 1f;
                    player.transform.GetChild(0).GetComponent<SpriteRenderer>().color = tmp;
                    player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = upgradeStorageIcon;
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        upgradeStorage();
                        audioManager.audioSource.PlayOneShot(audioManager.upgradeMachine);
                    }
                }
                else
                {
                    Color tmp = player.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
                    tmp.a = 1f;
                    player.transform.GetChild(0).GetComponent<SpriteRenderer>().color = tmp;
                    player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = upgradeStorageIcon;
                    if (Input.GetKeyDown(KeyCode.RightShift))
                    {
                        upgradeStorage();
                        audioManager.audioSource.PlayOneShot(audioManager.upgradeMachine);
                    }
                }
            }
            else
            {
                Color tmp = player.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
                tmp.a = 0.5f;
                player.transform.GetChild(0).GetComponent<SpriteRenderer>().color = tmp;
                player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = upgradeStorageIcon;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(owner == other.GetComponent<CharMovement>().player)
            {
                player = other.gameObject;
                isInRange = true;
                player.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (owner == other.GetComponent<CharMovement>().player)
            {
                isInRange = false;
                player.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    public void upgradeStorage()
    {
        resource.maxFoodResource = 40;
        currency.money -= upgradeCost;
    }
}
