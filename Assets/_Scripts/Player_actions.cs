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
    public int dir = 1;

    [Header("Jumping")]
    public float jumpPower = 10f;

    [Header("Shoot")]
    public float angle = 90;
    private int dirangle = 0;
    public float power = 10;
    private bool powering = false;
    [SerializeField] GameObject camera;
    [SerializeField] GameObject Bullet;

    [Header("GroundCheck")]
    public Transform GroundCheckPos;
    public Vector2 GroundCheckSize = new Vector2(0.5f, 0.5f);
    public LayerMask GroundLayer;

    // Update is called once per frame
    void FixedUpdate()
    {
        player.velocity = new Vector2(horizontalmove*movespeed * Time.fixedDeltaTime*50, player.velocity.y);
        Change_angle();
        Force_up();
        if (!powering && power > 0)
        {
            Shoot();
        }
        Reset_angle();
    }

    public void Exit(InputAction.CallbackContext context)
    {
        Application.Quit();
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalmove = context.ReadValue<Vector2>().x;
        //if the direction changes and it's not 0, actualize it
        if (Mathf.Sign(context.ReadValue<Vector2>().x) != dir 
                && (int) context.ReadValue<Vector2>().x != 0) 
        {
            angle *= -1;
            dir *= -1;
            dirangle *= -1;
        }
    }

    private void Reset_angle()
    {
        switch (dir)
        {
            case 1:
                if (angle < 45)
                {
                    angle = 45;
                }
                else if (angle > 180)
                {
                    angle = 180;
                }
                break;
            case -1:
                if (angle > -45)
                {
                    angle = -45;
                }
                else if (angle < -180)
                {
                    angle = -180;
                }
                break;
            default:
                print("direction error");
                break;
        }
    }

    private void Change_angle()
    {
        switch(dirangle)
        {
            case 1:
                angle += dir * Time.fixedDeltaTime*50;
                break;
               
            case -1:
                angle -= dir * Time.fixedDeltaTime*50;
                break;

            default:
                break;
        }
    }

    private void Force_up()
    {
        if (powering)
        {
            power += 0.1f*Time.fixedDeltaTime*50;
        }
        if (power > 20)
        {
            powering = false;
            power = 20;
        }
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (IsGrounded())
        {
            if (context.performed)
            {
                player.velocity = new Vector2(player.velocity.x, jumpPower);
            }
        }
    }

    public void Powered(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            power = 0;
            powering = true;
        }
        if (context.canceled)
        {
            powering = false;
        }
    }

    private void Shoot()
    {
        float x = Mathf.Cos((angle - 90) * dir * Mathf.PI / 180);
        float y = Mathf.Sin((angle - 90) * Mathf.PI / 180);
        Vector3 direction = new Vector3(x * power, y * power, 0);
        Vector3 spawn = new Vector3(transform.position.x + x, transform.position.y + y, 0);
        GameObject proj = Instantiate(Bullet, spawn, transform.rotation);
        proj.GetComponent<Move_bullet>().SetBullet((Vector3)direction);
        powering = false;
        power = 0;
        camera.GetComponent<follow_player>().follow = proj;
    }

    public void Aim(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            dirangle += (int)(context.ReadValue<float>()*10)*-dir;
        }
        if (context.canceled)
        {
            dirangle = 0;
        }
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
