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
    void Update()
    {
        if (player == playerNumber.Player1)
        {
            movement.x = 0;
            movement.z = 0;

            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            {
                movement.x = -1;
                anim.Play("walkbirukiri");
            }
            else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            {
                movement.x = 1;
                anim.Play("walkbirukanan");
            }
            else if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                movement.z = 1;
                anim.Play("walkbiruatas");
            }
            else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                movement.z = -1;
                anim.Play("walkbirubawah");
            }
            else if (movement.x == 0 && movement.z == 0 && Input.GetKeyDown(KeyCode.Space))
            {
                anim.Play("biruattack");
            }
            else if (movement.x == 0 && movement.z == 0 && !Input.GetKey(KeyCode.Space))
            {
                StartCoroutine(idleBiruCooldown());
            }
        }
        else
        {
            movement.x = 0;
            movement.z = 0;

            if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
            {
                movement.x = -1;
                anim.Play("walkmerahkiri");
            }
            else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
            {
                movement.x = 1;
                anim.Play("walkmerahkanan");
            }
            else if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                movement.z = 1;
                anim.Play("walkmerahatas");
            }
            else if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                movement.z = -1;
                anim.Play("walkmerahbawah");
            }
            else if (movement.x == 0 && movement.z == 0 && Input.GetKeyDown(KeyCode.RightShift))
            {
                anim.Play("merahattack");
            }
            else if (movement.x == 0 && movement.z == 0 && !Input.GetKey(KeyCode.RightShift))
            {
                StartCoroutine(idleMerahCooldown());
            }
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
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

