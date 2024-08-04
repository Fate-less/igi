using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HammerAttack : MonoBehaviour
{
    public playerNumber owner;
    public GameObject player;
    public Transform enemySpawnPoint;
    public Animator anim;
    public bool enemyIsInRange;
    public float maxCooldown = 1;

    private GameObject enemyObject;
    private float atkCooldown;
    private bool isInTerritory;

    // Update is called once per frame
    void Update()
    {
        atkCooldown -= Time.deltaTime;
        if (enemyIsInRange & atkCooldown <= 0)
        {
            if (isInTerritory)
            {
                if (owner == playerNumber.Player1)
                {
                    player.transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>().text = "Press Space to hit your opponent in front";
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        atkCooldown = maxCooldown + enemyObject.GetComponent<PlayerBlink>().blinkDuration;
                        StartCoroutine(EnemyGotHit());
                    }
                }
                else
                {
                    player.transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>().text = "Press Right Shift to hit your opponent in front";
                    if (Input.GetKeyDown(KeyCode.RightShift))
                    {
                        atkCooldown = maxCooldown + enemyObject.GetComponent<PlayerBlink>().blinkDuration;
                        StartCoroutine(EnemyGotHit());
                    }
                }
            }
            else
            {
                player.transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>().text = "You cant attack opponent in their territory";
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(owner != other.GetComponent<CharMovement>().player)
            {
                enemyObject = other.gameObject;
                enemyIsInRange = true;
                player.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (owner != other.GetComponent<CharMovement>().player)
            {
                enemyObject = null;
                enemyIsInRange = false;
                player.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (owner == playerNumber.Player1)
        {
            if (other.CompareTag("TerritoryP1"))
            {
                isInTerritory = true;
            }
            else if (other.CompareTag("TerritoryP2"))
            {
                isInTerritory = false;
            }
        }
        else
        {
            if (other.CompareTag("TerritoryP2"))
            {
                isInTerritory = true;
            }
            else if (other.CompareTag("TerritoryP1"))
            {
                isInTerritory = false;
            }
        }
    }

    public IEnumerator EnemyGotHit()
    {
        //nyerang
        enemyObject.GetComponent<PlayerBlink>().GetHit();
        enemyObject.GetComponent<CharMovement>().enabled = false;
        yield return new WaitForSeconds(enemyObject.GetComponent<PlayerBlink>().blinkDuration);
        enemyObject.GetComponent<CharMovement>().enabled = true;
        enemyObject.transform.position = enemySpawnPoint.position;
    }
}
