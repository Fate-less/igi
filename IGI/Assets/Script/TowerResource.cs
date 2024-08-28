using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerResource : MonoBehaviour
{
    public Tower towerScript;
    public Resource resourceStorage;
    public Image resourceBar;
    public float towerResource = 0;
    public float maxResource;
    public float increment;
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("Audio Handler").GetComponent<AudioManager>();
        if(towerScript.owner == playerNumber.Player1)
        {
            resourceStorage = GameObject.FindGameObjectWithTag("Storage1").GetComponent<Resource>();
        }
        else
        {
            resourceStorage = GameObject.FindGameObjectWithTag("Storage2").GetComponent<Resource>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        resourceBar.fillAmount = towerResource / maxResource;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.transform.GetChild(1).gameObject.activeInHierarchy)
            {
                increment = maxResource - towerResource;
                if(increment > resourceStorage.foodResource)
                {
                    towerResource += resourceStorage.foodResource;
                    resourceStorage.foodResource = 0;
                }
                else
                {
                    resourceStorage.foodResource -= increment;
                    towerResource = maxResource;
                }
                other.transform.GetChild(1).gameObject.SetActive(false);
                audioManager.audioSource.PlayOneShot(audioManager.resupplyMachine);
            }
        }
    }
}
