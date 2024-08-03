using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeStorage : MonoBehaviour
{
    public playerNumber owner;
    public float upgradeCost;
    public Resource resource;
    public Mesh upgradedMesh;
    public Currency currency;

    private GameObject player;
    private bool isInRange = false;
    private bool isUpgraded;

    private void Start()
    {
        currency = GameObject.Find("Handler").GetComponent<Currency>();
        if(owner == playerNumber.Player1)
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
                    player.transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>().text = "Press Space to upgrade storage";
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        upgradeStorage();
                    }
                }
                else
                {
                    player.transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>().text = "Press Right Shift to upgrade storage";
                    if (Input.GetKeyDown(KeyCode.RightShift))
                    {
                        upgradeStorage();
                    }
                }
            }
            else
            {
                player.transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>().text = "You need " + upgradeCost.ToString() + " to upgrade storage";
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
        resource.maxFoodResource = 20;
        resource.GetComponent<MeshFilter>().mesh = upgradedMesh;
        currency.money -= upgradeCost;
    }
}
