using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    public playerNumber owner;
    public float maxHealth = 100f;
    public GameObject destroyPopupUI;
    public Image destroyProgressSlider;
    public float destroyTime = 5f;

    private float currentHealth;
    private bool isPlayerInRange = false;
    private bool isBeingDestroyed = false;
    private playerNumber playerInRange;

    void Start()
    {
        currentHealth = maxHealth;
        destroyProgressSlider.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isPlayerInRange && !isBeingDestroyed && Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(DestroyTower());
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
        isBeingDestroyed = true;
        destroyPopupUI.SetActive(false);
        destroyProgressSlider.gameObject.SetActive(true);
        destroyProgressSlider.fillAmount = 0;

        float elapsedTime = 0;
        while (elapsedTime < destroyTime)
        {
            elapsedTime += Time.deltaTime;
            destroyProgressSlider.fillAmount = elapsedTime / destroyTime;
            yield return null;
        }

        destroyProgressSlider.gameObject.SetActive(false);
        currentHealth -= maxHealth;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        isBeingDestroyed = false;
    }
}
