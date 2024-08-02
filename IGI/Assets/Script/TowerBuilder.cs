using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TowerBuilder : MonoBehaviour
{
    public GameObject popupUI;
    public Slider buildProgressSlider;
    public GameObject towerPrefab;
    public Transform buildSpot;
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
        if (isPlayerInRange && !isBuilding && Input.GetKeyDown(KeyCode.B))
        {
            StartCoroutine(BuildTower());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            popupUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            popupUI.SetActive(false);
        }
    }

    private IEnumerator BuildTower()
    {
        isBuilding = true;
        popupUI.SetActive(false);
        buildProgressSlider.gameObject.SetActive(true);
        buildProgressSlider.value = 0;

        float elapsedTime = 0;
        while (elapsedTime < buildTime)
        {
            elapsedTime += Time.deltaTime;
            buildProgressSlider.value = elapsedTime / buildTime;
            yield return null;
        }

        buildProgressSlider.gameObject.SetActive(false);
        Instantiate(towerPrefab, buildSpot.position, Quaternion.identity);
        isBuilding = false;
    }
}

