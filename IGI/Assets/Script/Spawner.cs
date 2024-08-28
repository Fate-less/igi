using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public playerNumber owner;
    public WalkingDirection spawnDirection;
    public GameObject entityPrefab;
    public float spawnInterval = 5f;
    private AudioManager audioManager;
    private bool isActive = false;
    private float timer = 0f;

    void Update()
    {
        if (isActive)
        {
            timer += Time.deltaTime;
            if (timer >= spawnInterval)
            {
                if(transform.childCount < 2)
                {
                    SpawnEntity();
                }
                timer = 0f;
            }
        }
    }

    public void ActivateSpawner()
    {
        isActive = true;
    }

    private void SpawnEntity()
    {
        GameObject customer = Instantiate(entityPrefab, transform.position, transform.rotation);
        customer.transform.SetParent(gameObject.transform);
        customer.GetComponent<Customer>().owner = owner;
        customer.GetComponent<Customer>().walkingDirection = spawnDirection;
    }
}
