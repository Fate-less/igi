using UnityEngine;
using System.Collections;

public enum WalkingDirection
{
    Up,
    Down,
    Left,
    Right
}
public class Customer : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float detectionRange = 1f;
    public LayerMask obstacleLayer;
    public float waitTime = 20f;
    public int playerDamage = 1;
    public WalkingDirection walkingDirection = WalkingDirection.Right;

    public playerNumber owner;
    private bool isMoving = true;
    private bool isInWaitArea = false;
    private float waitTimer = 0f;
    private Rigidbody rb;
    private AudioManager audioManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioManager = GameObject.Find("Audio Handler").GetComponent<AudioManager>();
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            Move();
            CheckForObstacles();
        }
        else if (isInWaitArea)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                DamagePlayer();
                Destroy(gameObject);
            }
        }
    }

    private void Move()
    {
        Vector3 movement = Vector3.zero;
        switch (walkingDirection)
        {
            case WalkingDirection.Up:
                movement = new Vector3(0, 0, moveSpeed * Time.fixedDeltaTime);
                break;
            case WalkingDirection.Down:
                movement = new Vector3(0, 0, -moveSpeed * Time.fixedDeltaTime);
                break;
            case WalkingDirection.Left:
                movement = new Vector3(-moveSpeed * Time.fixedDeltaTime, 0, 0);
                break;
            case WalkingDirection.Right:
                movement = new Vector3(moveSpeed * Time.fixedDeltaTime, 0, 0);
                break;
        }
        rb.MovePosition(rb.position + movement);
    }

    private void CheckForObstacles()
    {
        RaycastHit hit;
        Vector3 direction = Vector3.zero;
        switch (walkingDirection)
        {
            case WalkingDirection.Up:
                direction = transform.forward;
                break;
            case WalkingDirection.Down:
                direction = -transform.forward;
                break;
            case WalkingDirection.Left:
                direction = -transform.right;
                break;
            case WalkingDirection.Right:
                direction = transform.right;
                break;
        }

        if (Physics.Raycast(transform.position, direction, out hit, detectionRange, obstacleLayer))
        {
            if (hit.collider != null)
            {
                isMoving = false;
                waitTimer = 0f; // Reset the wait timer when an obstacle is detected
            }
        }
        else
        {
            isMoving = true;
        }
    }

    private void DamagePlayer()
    {
        // Assume you have a PlayerHealth script that manages player health
        if (owner == playerNumber.Player1)
        {
            GameObject.FindGameObjectWithTag("PlayerHealthP1").GetComponent<PlayerHealth>().healthDecrease();
            AudioSource.PlayClipAtPoint(audioManager.orderFailed, gameObject.transform.position);
        }
        else
        {
            GameObject.FindGameObjectWithTag("PlayerHealthP2").GetComponent<PlayerHealth>().healthDecrease();
            AudioSource.PlayClipAtPoint(audioManager.orderFailed, gameObject.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WaitArea"))
        {
            isInWaitArea = true;
            isMoving = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("WaitArea"))
        {
            isInWaitArea = false;
            waitTimer = 0f; // Reset the wait timer when leaving the area
        }
    }
}
