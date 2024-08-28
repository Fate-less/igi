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
    public Sprite resourceIcon;

    private GameObject player;
    private AudioManager audioManager;

    public bool isInRange;

    private void Start()
    {
        audioManager = GameObject.Find("Audio Handler").GetComponent<AudioManager>();
    }

    private void Update()
    {
        healthbar.fillAmount = foodResource / maxFoodResource;
        if (isInRange)
        {
            if(foodResource > 0)
            {
                if (owner == playerNumber.Player1)
                {
                    Color tmp = player.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
                    tmp.a = 1f;
                    player.transform.GetChild(0).GetComponent<SpriteRenderer>().color = tmp;
                    player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = resourceIcon;
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        player.transform.GetChild(1).gameObject.SetActive(true);
                        audioManager.audioSource.PlayOneShot(audioManager.takeResource);
                    }
                }
                else
                {
                    Color tmp = player.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
                    tmp.a = 1f;
                    player.transform.GetChild(0).GetComponent<SpriteRenderer>().color = tmp;
                    player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = resourceIcon;
                    if (Input.GetKeyDown(KeyCode.RightShift))
                    {
                        player.transform.GetChild(1).gameObject.SetActive(true);
                        audioManager.audioSource.PlayOneShot(audioManager.takeResource);
                    }
                }
            }
            else
            {
                Color tmp = player.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
                tmp.a = 0.5f;
                player.transform.GetChild(0).GetComponent<SpriteRenderer>().color = tmp;
                player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = resourceIcon;
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
