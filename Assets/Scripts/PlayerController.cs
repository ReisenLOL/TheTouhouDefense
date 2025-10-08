using System;
using UnityEngine;

public class PlayerController : Unit
{
    public Vector2 moveDirection;
    
    void Update()
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * speed;
    }
}
