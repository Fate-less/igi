using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tower : MonoBehaviour
{
    public playerNumber owner;
    public float maxHealth = 100f;
    public GameObject destroyPopupUI;
    public Image destroyProgressSlider;
    public float destroyTime = 5f;
    public TowerBuilder buildArea;
    public Sprite destroyTowerIcon;

    private float currentHealth;
    private bool isPlayerInRange = false;
    private bool isBeingDestroyed = false;
    private playerNumber playerInRange;
    private float elapsedTime = 0;
    private AudioManager audioManager;

    void Start()
    {
        currentHealth = maxHealth;
        destroyProgressSlider.gameObject.SetActive(false);
        buildArea = transform.GetComponentInParent<TowerBuilder>();
        owner = transform.parent.GetComponent<TowerBuilder>().towerNumber;
        audioManager = GameObject.Find("Audio Handler").GetComponent<AudioManager>();
    }

    void Update()
    {
        if(playerInRange == playerNumber.Player1)
        {
            if (isPlayerInRange && !isBeingDestroyed && Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(DestroyTower());
            }
        }
        else
        {
            if (isPlayerInRange && !isBeingDestroyed && Input.GetKeyDown(KeyCode.RightShift))
            {
                StartCoroutine(DestroyTower());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharMovement playerMovement = other.GetComponent<CharMovement>();
            if (playerMovement != null && playerMovement.player != owner)
            {
                Debug.Log("enemy on range");
                isPlayerInRange = true;
                playerInRange = playerMovement.player;
                destroyPopupUI = other.transform.GetChild(0).gameObject;
                if (playerInRange == playerNumber.Player1)
                {
                    Color tmp = destroyPopupUI.transform.GetComponent<SpriteRenderer>().color;
                    tmp.a = 1f;
                    destroyPopupUI.transform.GetComponent<SpriteRenderer>().color = tmp;
                    destroyPopupUI.transform.GetComponent<SpriteRenderer>().sprite = destroyTowerIcon;
                }
                else
                {
                    Color tmp = destroyPopupUI.transform.GetComponent<SpriteRenderer>().color;
                    tmp.a = 1f;
                    destroyPopupUI.transform.GetComponent<SpriteRenderer>().color = tmp;
                    destroyPopupUI.transform.GetComponent<SpriteRenderer>().sprite = destroyTowerIcon;
                }
                destroyPopupUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharMovement playerMovement = other.GetComponent<CharMovement>();
            if (playerMovement != null && playerMovement.player != owner)
            {
                isPlayerInRange = false;
                destroyPopupUI.SetActive(false);
            }
        }
    }

    private IEnumerator DestroyTower()
    {
        if (isPlayerInRange)
        {
            isBeingDestroyed = true;
            destroyPopupUI.SetActive(false);
            destroyProgressSlider.gameObject.SetActive(true);

            while (elapsedTime < destroyTime && isPlayerInRange)
            {
                elapsedTime += Time.deltaTime;
                destroyProgressSlider.fillAmount = elapsedTime / destroyTime;
                yield return null;
            }
            if (isPlayerInRange)
            {
                destroyProgressSlider.gameObject.SetActive(false);
                currentHealth -= maxHealth;
                if (currentHealth <= 0)
                {
                    audioManager.audioSource.PlayOneShot(audioManager.destroyMachine);
                    Destroy(gameObject);
                    buildArea.towerBuilt = false;
                }
            }
            isBeingDestroyed = false;
        }
    }
}
