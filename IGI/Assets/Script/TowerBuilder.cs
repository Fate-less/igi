using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerBuilder : MonoBehaviour
{
    public GameObject popupUI;
    public Image buildProgressSlider;
    public GameObject towerPrefab;
    public Transform buildSpot;
    public playerNumber towerNumber;
    public float buildTime = 5f;
    public bool towerBuilt = false;
    public Currency currency;
    public float buildCost;
    public float upgradeCost;
    public GameObject upgradePrefab;
    public bool towerUpgraded;

    private bool isPlayerInRange = false;
    private bool isBuilding = false;
    private float elapsedTime = 0;
    private GameObject player;
    private GameObject towerObject;

    void Start()
    {
        currency = GameObject.Find("Handler").GetComponent<Currency>();
        popupUI.SetActive(false);
        buildProgressSlider.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isPlayerInRange)
        {
            //kalo belom di build
            if (!towerBuilt)
            {
                if (currency.money >= buildCost)
                {
                    if (towerNumber == playerNumber.Player1)
                    {
                        if (!isBuilding)
                        {
                            player.transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>().text = "Press Space to build food machine";
                            if (Input.GetKeyDown(KeyCode.Space))
                            {
                                StartCoroutine(BuildTower());
                            }
                        }
                    }
                    else
                    {
                        if (!isBuilding)
                        {
                            player.transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>().text = "Press Right Shift to build food machine";
                            if (Input.GetKeyDown(KeyCode.RightShift))
                            {
                                StartCoroutine(BuildTower());
                            }
                        }
                    }
                }
                else
                {
                    player.transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>().text = "You need " + buildCost.ToString() + " to build food machine";
                }
            }
            //kalo mau di upgrade
            else
            {
                if (currency.money >= upgradeCost)
                {
                    if (towerNumber == playerNumber.Player1)
                    {
                        if (!isBuilding && !towerUpgraded)
                        {
                            player.transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>().text = "Press Space to upgrade food machine";
                            if (Input.GetKeyDown(KeyCode.Space))
                            {
                                StartCoroutine(UpgradeTower());
                            }
                        }
                    }
                    else
                    {
                        if (!isBuilding && !towerUpgraded)
                        {
                            player.transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>().text = "Press Right Shift to upgrade food machine";
                            if (Input.GetKeyDown(KeyCode.RightShift))
                            {
                                StartCoroutine(UpgradeTower());
                            }
                        }
                    }
                }
                else
                {
                    player.transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>().text = "You need " + upgradeCost.ToString() + " to upgrade food machine";
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!towerBuilt || !towerUpgraded)
        {
            if (other.CompareTag("Player"))
            {
                if (other.GetComponent<CharMovement>().player == towerNumber)
                {
                    player = other.gameObject;
                    isPlayerInRange = true;
                    popupUI.SetActive(true);
                    popupUI.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Press Space to Build";
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<CharMovement>().player == towerNumber)
            {
                popupUI.SetActive(false);
                isPlayerInRange = false;
                popupUI.SetActive(false);
            }
        }
    }

    private IEnumerator BuildTower()
    {
        if (isPlayerInRange)
        {
            isBuilding = true;
            popupUI.SetActive(false);
            buildProgressSlider.gameObject.SetActive(true);
            buildProgressSlider.fillAmount = 0;

            while (elapsedTime < buildTime && isPlayerInRange)
            {
                elapsedTime += Time.deltaTime;
                buildProgressSlider.fillAmount = elapsedTime / buildTime;
                yield return null;
            }
            if (isPlayerInRange)
            {
                buildProgressSlider.gameObject.SetActive(false);
                towerObject = Instantiate(towerPrefab, buildSpot.position, Quaternion.identity);
                towerObject.transform.SetParent(gameObject.transform);
                towerBuilt = true;
                elapsedTime = 0;
                currency.money -= buildCost;
            }
            isBuilding = false;
        }
    }

    private IEnumerator UpgradeTower()
    {
        if (isPlayerInRange)
        {
            isBuilding = true;
            popupUI.SetActive(false);
            buildProgressSlider.gameObject.SetActive(true);
            buildProgressSlider.fillAmount = 0;

            while (elapsedTime < buildTime && isPlayerInRange)
            {
                elapsedTime += Time.deltaTime;
                buildProgressSlider.fillAmount = elapsedTime / buildTime;
                yield return null;
            }
            if (isPlayerInRange)
            {
                buildProgressSlider.gameObject.SetActive(false);
                Destroy(towerObject);
                Debug.Log("destroyed");
                towerObject = Instantiate(upgradePrefab, buildSpot.position, Quaternion.identity);
                towerObject.transform.SetParent(gameObject.transform);
                towerUpgraded = true;
                elapsedTime = 0;
                currency.money -= upgradeCost;
            }
            isBuilding = false;
        }
    }
}

