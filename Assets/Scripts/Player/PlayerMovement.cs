using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigidbody2D;
    [SerializeField] private float moveSpeedDefault = 10f;
    [SerializeField] private float moveSpeedSlowDown = 4f;

    private float moveSpeed;
    private Vector2 movement;

    private void Start()
    {
        playerRigidbody2D.angularDrag = 5;
        moveSpeed = moveSpeedDefault;
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        //Precision Movement
        //playerRigidbody2D.MovePosition(playerRigidbody2D.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);

        //Floaty Movement
        playerRigidbody2D.AddForce(movement.normalized * moveSpeed * Time.deltaTime, ForceMode2D.Impulse);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("SlowDown"))
        {
            moveSpeed = moveSpeedSlowDown;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        moveSpeed = moveSpeedDefault;
    }
}
