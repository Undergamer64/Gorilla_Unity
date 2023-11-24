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
    private bool grounded = true;

    [Header("Shoot")]
    public float angle = 0;
    public float power = 0;
    [SerializeField] GameObject Bullet;

    [Header("GroundCheck")]
    public Transform GroundCheckPos;
    public Vector2 GroundCheckSize = new Vector2(0.5f, 0.5f);
    public LayerMask GroundLayer;

    // Update is called once per frame
    void Update()
    {
        player.velocity = new Vector2(horizontalmove*movespeed, player.velocity.y);
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalmove = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (IsGrounded())
        {
            if (context.performed && grounded)
            {
                player.velocity = new Vector2(player.velocity.x, jumpPower);
            }
        }
    }

    public void Throw_ball(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
             GameObject proj = Instantiate(Bullet, transform.position, transform.rotation);
        }
    }

    public void Aim(InputAction.CallbackContext context)
    {
        angle += context.ReadValue<Vector2>().x;
        power += context.ReadValue<Vector2>().y;
    }
    private bool IsGrounded() 
    {
        if (Physics2D.OverlapBox(GroundCheckPos.position, GroundCheckSize, 0, GroundLayer))
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireCube(GroundCheckPos.position, GroundCheckSize);
    }
}
