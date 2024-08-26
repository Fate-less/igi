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
    public Animator anim;

    private Vector3 movement;
    private void Start()
    {
        if (player == playerNumber.Player1)
        {
            anim.Play("idlebiru");
        }
        else
        {
            anim.Play("idlemerah");
        }
    }
    private void Update()
    {
        // Handle attack
        if (Input.GetKeyDown(KeyCode.Space))
        {
            movement.x = 0;
            movement.z = 0;
            anim.Play("biruattack");
        }
    }
    void FixedUpdate()
    {
        // Reset movement
        movement.x = 0;
        movement.z = 0;

        // Player 1 Controls
        if (player == playerNumber.Player1)
        {
            // Handle movement inputs
            if (Input.GetKey(KeyCode.A))
            {
                movement.x = -1;
                anim.Play("walkbirukiri");
            }
            else if (Input.GetKey(KeyCode.D))
            {
                movement.x = 1;
                anim.Play("walkbirukanan");
            }
            else if (Input.GetKey(KeyCode.W))
            {
                movement.z = 1;
                anim.Play("walkbiruatas");
            }
            else if (Input.GetKey(KeyCode.S))
            {
                movement.z = -1;
                anim.Play("walkbirubawah");
            }
            else if (movement.x == 0 && movement.z == 0 && !Input.GetKey(KeyCode.Space))
            {
                StartCoroutine(idleBiruCooldown());
            }
        }
        // Player 2 Controls
        else
        {
            // Handle movement inputs
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                movement.x = -1;
                anim.Play("walkmerahkiri");
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                movement.x = 1;
                anim.Play("walkmerahkanan");
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                movement.z = 1;
                anim.Play("walkmerahatas");
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                movement.z = -1;
                anim.Play("walkmerahbawah");
            }

            // Handle attack
            if (movement.x == 0 && movement.z == 0 && Input.GetKeyDown(KeyCode.RightShift))
            {
                anim.Play("merahattack");
            }
            else if (movement.x == 0 && movement.z == 0 && !Input.GetKey(KeyCode.RightShift))
            {
                StartCoroutine(idleMerahCooldown());
            }
        }

        // Apply force to the rigidbody
        Vector3 force = new Vector3(movement.x, 0, movement.z).normalized * moveSpeed;
        rb.AddForce(force, ForceMode.Force);
    }


    public IEnumerator idleBiruCooldown()
    {
        yield return new WaitForSeconds(2f);
        if (movement.x == 0 && movement.z == 0 && !Input.GetKey(KeyCode.Space)){
            anim.Play("idlebiru");
        }
    }

    public IEnumerator idleMerahCooldown()
    {
        yield return new WaitForSeconds(2f);
        if (movement.x == 0 && movement.z == 0 && !Input.GetKey(KeyCode.RightShift)){
            anim.Play("idlemerah");
        }
    }
}

