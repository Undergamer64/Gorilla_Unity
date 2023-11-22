using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D player;

    [Header("Movement")]
    public float movespeed = 100;
    float horizontalmove;

    [Header("Jumping")]
    public float jumpPower = 10f;

    // Update is called once per frame
    void Update()
    {
        player.velocity = (new Vector2(horizontalmove*movespeed*Time.deltaTime*100, player.velocity.y));
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalmove = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            player.velocity = new Vector2(player.velocity.x, jumpPower);
        }
    }
}
