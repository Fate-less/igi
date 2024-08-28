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
    public Sprite attackIcon;

    private AudioManager audioManager;
    private GameObject enemyObject;
    private float atkCooldown;
    private bool isInTerritory;

    private void Start()
    {
        audioManager = GameObject.Find("Audio Handler").GetComponent<AudioManager>();
    }

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
                    Color tmp = player.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
                    tmp.a = 1f;
                    player.transform.GetChild(0).GetComponent<SpriteRenderer>().color = tmp;
                    player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = attackIcon;
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        atkCooldown = maxCooldown + enemyObject.GetComponent<PlayerBlink>().blinkDuration;
                        StartCoroutine(EnemyGotHit());
                    }
                }
                else
                {
                    Color tmp = player.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
                    tmp.a = 1f;
                    player.transform.GetChild(0).GetComponent<SpriteRenderer>().color = tmp;
                    player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = attackIcon;
                    if (Input.GetKeyDown(KeyCode.RightShift))
                    {
                        atkCooldown = maxCooldown + enemyObject.GetComponent<PlayerBlink>().blinkDuration;
                        StartCoroutine(EnemyGotHit());
                    }
                }
            }
            else
            {
                Color tmp = player.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
                tmp.a = 0.5f;
                player.transform.GetChild(0).GetComponent<SpriteRenderer>().color = tmp;
                player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = attackIcon;
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
        CharMovement enemyMovement = enemyObject.GetComponent<CharMovement>();
        HammerAttack enemyAttack = enemyObject.transform.GetChild(2).GetComponent<HammerAttack>();
        Transform enemyPosition = enemyObject.transform;
        enemyObject.GetComponent<PlayerBlink>().GetHit();
        enemyMovement.enabled = false;
        enemyAttack.enabled = false;
        yield return new WaitForSeconds(enemyObject.GetComponent<PlayerBlink>().blinkDuration - 0.5f);
        enemyAttack.enabled = true;
        enemyMovement.enabled = true;
        enemyPosition.position = enemySpawnPoint.position;
    }
}
