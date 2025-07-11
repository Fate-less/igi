using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cookFood : MonoBehaviour
{
    public playerNumber owner;
    public float maxCD = 5;
    public TowerResource towerResource;
    public GameObject customer;
    public Currency currency;
    private AudioManager audioManager;
    private bool isInRange = false;
    private float cookCD;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("Audio Handler").GetComponent<AudioManager>();
        owner = transform.parent.GetComponent<Tower>().owner;
        towerResource = transform.parent.GetComponent<TowerResource>();
        if(owner == playerNumber.Player1)
        {
            currency = GameObject.FindGameObjectWithTag("CurrencyP1").GetComponent<Currency>();
        }
        else
        {
            currency = GameObject.FindGameObjectWithTag("CurrencyP2").GetComponent<Currency>();
        }
        cookCD = maxCD;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange)
        {
            cookCD -= Time.deltaTime;
            if (towerResource.towerResource > 0)
            {
                if (cookCD <= 0)
                {
                    cookCD = maxCD;
                    currency.money += 30;
                    towerResource.towerResource -= 1;
                    try
                    {
                        Destroy(customer.gameObject);
                        audioManager.audioSource.PlayOneShot(audioManager.orderSuccess);
                        customer = null;
                    }
                    catch { }
                    Debug.Log("jalan");
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Customer"))
        {
            isInRange = true;
            customer = other.gameObject;
        }
    }
}
