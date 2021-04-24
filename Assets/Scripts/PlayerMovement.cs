using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    [SerializeField] private Rigidbody2D playerRigidbody2D;

    private Vector2 movement;
    private void Start()
    {
        
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
        playerRigidbody2D.MovePosition(playerRigidbody2D.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}
