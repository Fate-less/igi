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

    private bool isPlayerInRange = false;
    private bool isBuilding = false;

    void Start()
    {
        popupUI.SetActive(false);
        buildProgressSlider.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isPlayerInRange && !isBuilding && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(BuildTower());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(other.GetComponent<CharMovement>().player == towerNumber)
            {
                isPlayerInRange = true;
                popupUI.SetActive(true);
                popupUI.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Press Space to Build";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            popupUI.SetActive(false);
            if (other.GetComponent<CharMovement>().player == towerNumber)
            {
                isPlayerInRange = false;
                popupUI.SetActive(false);
            }
        }
    }

    private IEnumerator BuildTower()
    {
        isBuilding = true;
        popupUI.SetActive(false);
        buildProgressSlider.gameObject.SetActive(true);
        buildProgressSlider.fillAmount = 0;

        float elapsedTime = 0;
        while (elapsedTime < buildTime)
        {
            elapsedTime += Time.deltaTime;
            buildProgressSlider.fillAmount = elapsedTime / buildTime;
            yield return null;
        }

        buildProgressSlider.gameObject.SetActive(false);
        GameObject towerObject = Instantiate(towerPrefab, buildSpot.position, Quaternion.identity);
        towerObject.transform.SetParent(gameObject.transform);
        isBuilding = false;
    }
}

