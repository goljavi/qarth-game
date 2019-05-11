using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayers : MonoBehaviour
{
    Rigidbody rb;
    public float speed;
    public bool player1;
    float moveH;
    float moveV;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (player1)
        {
            moveH = Input.GetAxis("HorizontalPlayer1");
            moveV = Input.GetAxis("VerticalPlayer1");
        }
        else
        {
            moveH = Input.GetAxis("HorizontalPlayer2");
            moveV = Input.GetAxis("VerticalPlayer2");
        }

        rb.velocity = new Vector3(moveH * speed, rb.velocity.y, moveV * speed);
    }
}
