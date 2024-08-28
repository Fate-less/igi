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
    public Transform upgradeSpot;
    public playerNumber towerNumber;
    public float buildTime = 5f;
    public float upgradeTime = 60f;
    public bool towerBuilt = false;
    public Currency currency;
    public float buildCost;
    public float upgradeCost;
    public GameObject upgradePrefab;
    public bool towerUpgraded;
    public Sprite craftIcon;
    public Sprite upgradeIcon;

    private bool isPlayerInRange = false;
    private bool isBuilding = false;
    private float elapsedTime = 0;
    private GameObject player;
    private GameObject towerObject;
    private AudioManager audioManager;

    void Start()
    {
        audioManager = GameObject.Find("Audio Handler").GetComponent<AudioManager>();
        if (towerNumber == playerNumber.Player1)
        {
            currency = GameObject.FindGameObjectWithTag("CurrencyP1").GetComponent<Currency>();
        }
        else
        {
            currency = GameObject.FindGameObjectWithTag("CurrencyP2").GetComponent<Currency>();
        }
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
                            Color tmp = player.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
                            tmp.a = 1f;
                            player.transform.GetChild(0).GetComponent<SpriteRenderer>().color = tmp;
                            player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = craftIcon;
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
                            Color tmp = player.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
                            tmp.a = 1f;
                            player.transform.GetChild(0).GetComponent<SpriteRenderer>().color = tmp;
                            player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = craftIcon;
                            if (Input.GetKeyDown(KeyCode.RightShift))
                            {
                                StartCoroutine(BuildTower());
                            }
                        }
                    }
                }
                else
                {
                    Color tmp = player.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
                    tmp.a = 0.5f;
                    player.transform.GetChild(0).GetComponent<SpriteRenderer>().color = tmp;
                    player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = craftIcon;
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
                            Color tmp = player.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
                            tmp.a = 1f;
                            player.transform.GetChild(0).GetComponent<SpriteRenderer>().color = tmp;
                            player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = upgradeIcon;
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
                            Color tmp = player.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
                            tmp.a = 1f;
                            player.transform.GetChild(0).GetComponent<SpriteRenderer>().color = tmp;
                            player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = upgradeIcon;
                            if (Input.GetKeyDown(KeyCode.RightShift))
                            {
                                StartCoroutine(UpgradeTower());
                            }
                        }
                    }
                }
                else
                {
                    Color tmp = player.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
                    tmp.a = 0.5f;
                    player.transform.GetChild(0).GetComponent<SpriteRenderer>().color = tmp;
                    player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = upgradeIcon;
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
            {
                while (elapsedTime < buildTime && isPlayerInRange)
                {
                    elapsedTime += Time.deltaTime;
                    buildProgressSlider.fillAmount = elapsedTime / buildTime;
                    yield return null;
                }
            }
            if (isPlayerInRange)
            {
                audioManager.audioSource.PlayOneShot(audioManager.craftMachine);
                audioManager.audioSource.PlayOneShot(audioManager.upgradeMachine);
                buildProgressSlider.gameObject.SetActive(false);
                towerObject = Instantiate(towerPrefab, buildSpot.position, towerPrefab.transform.rotation);
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

            while (elapsedTime < upgradeTime && isPlayerInRange)
            {
                elapsedTime += Time.deltaTime;
                buildProgressSlider.fillAmount = elapsedTime / upgradeTime;
                yield return null;
            }
            if (isPlayerInRange)
            {
                audioManager.audioSource.PlayOneShot(audioManager.craftMachine);
                audioManager.audioSource.PlayOneShot(audioManager.upgradeMachine);
                buildProgressSlider.gameObject.SetActive(false);
                float foodLeft = towerObject.GetComponent<TowerResource>().towerResource;
                Destroy(towerObject);
                Debug.Log("destroyed");
                towerObject = Instantiate(upgradePrefab, upgradeSpot.position, upgradePrefab.transform.rotation);
                towerObject.transform.SetParent(gameObject.transform);
                towerObject.LeanMoveY(0, 0.1f);
                towerObject.GetComponent<TowerResource>().towerResource = foodLeft;
                towerUpgraded = true;
                elapsedTime = 0;
                currency.money -= upgradeCost;
            }
            isBuilding = false;
        }
    }
}

