using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum playerNumber
{
    Player1,
    Player2
}
public class CharMovement : MonoBehaviour
{
    public playerNumber player;
    public float moveSpeed = 5f;
    public Rigidbody rb;

    private Vector3 movement;

    void Update()
    {
        // Player 1 controls (WASD)
        if(player == playerNumber.Player1)
        {
            movement.x = Input.GetKey(KeyCode.A) ? -1 : Input.GetKey(KeyCode.D) ? 1 : 0;
            movement.z = Input.GetKey(KeyCode.W) ? 1 : Input.GetKey(KeyCode.S) ? -1 : 0;
        }
        else
        {
            movement.x = Input.GetKey(KeyCode.LeftArrow) ? -1 : Input.GetKey(KeyCode.RightArrow) ? 1 : 0;
            movement.z = Input.GetKey(KeyCode.UpArrow) ? 1 : Input.GetKey(KeyCode.DownArrow) ? -1 : 0;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}

