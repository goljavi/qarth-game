using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayers : MonoBehaviour
{
    Player _player;
    Rigidbody rb;
    public float speed;
    public bool player1;
    float moveH;
    float moveV;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _player = GetComponent<Player>();
    }

    void FixedUpdate()
    {
        if (player1)
        {
            moveH = Input.GetAxis("HorizontalPlayer1");
            moveV = Input.GetAxis("VerticalPlayer1");
            if (Input.GetKeyDown(KeyCode.Keypad0)) { _player.Connect(); Debug.Log("pressed"); }

            if (Input.GetKeyDown(KeyCode.Keypad1)) _player.Disconnect();
        }
        else
        {
            moveH = Input.GetAxis("HorizontalPlayer2");
            moveV = Input.GetAxis("VerticalPlayer2");
            if (Input.GetKeyDown(KeyCode.F)) _player.Connect();
            if (Input.GetKeyDown(KeyCode.G)) _player.Disconnect();
        }

        rb.velocity = new Vector3(moveH * speed, rb.velocity.y, moveV * speed);
    }
}
