using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class AI_actions : MonoBehaviour
{
    public Rigidbody2D AI;
    public GameObject player;
    public GameObject camera;

    [Header("Movement")]
    public float movespeed = 100;
    float horizontalmove = 0;

    [Header("Shoot")]
    public float angle = 90;
    public float power = 10;
    int dir;
    bool detect = false;
    //private bool powering = false;
    [SerializeField] GameObject Bullet;


    private void Start()
    {
        StartCoroutine(delay());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AI.velocity = new Vector2 (horizontalmove, AI.velocity.y);
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(2f);
        //Search();
        Better_Search();
    }
    
    /*
    private void Search()
    {
        bool detect = false;
        dir = Math.Sign(player.GetComponent<Rigidbody2D>().position.x - transform.position.x);
        for (int i = 1; i < 10; i++) 
        {
            power = i*2;
            float angle_max = 180*dir;
            float angle_min = 45*dir;
            for (int j = 0; j < 20; j++)
            {
                float mid = (angle_max + angle_min)/2;
                float x = Mathf.Cos((mid - 90) * dir * Mathf.PI / 180);
                float y = Mathf.Sin((mid - 90) * Mathf.PI / 180);
                Vector2 velocity = new Vector2(x * power, y * power);
                Vector2 curpos = new Vector2(transform.position.x + x, transform.position.y + y);
                Vector2 lastpos = curpos;
                Vector2 gravity = new Vector2(0, -9.80665f);
                while (!detect) 
                {
                    curpos += velocity*Time.fixedDeltaTime; 
                    velocity += gravity * Time.fixedDeltaTime;
                    Debug.DrawLine(lastpos, curpos);
                    var raycast = Physics2D.CircleCast(curpos, 0.25f, Vector2.zero);
                    if (raycast.collider)
                    {
                        if (raycast.collider != null)
                        {
                            if (raycast.collider.transform.tag == "Map")
                            {
                                break;
                            }
                            else if (raycast.collider.transform.tag == "Player")
                            {
                                detect = true;
                                AI_Shoot(new Vector2(transform.position.x + x, transform.position.y + y), new Vector2(x * power, y * power));
                            }
                        }
                    }
                    lastpos = curpos;
                }
                if (dir == 1)
                {
                    if (player.GetComponent<Rigidbody2D>().position.x > curpos.x)
                    {
                        angle_min = mid;
                    }
                    else
                    {
                        angle_max = mid;
                    }
                }
                else if (dir == -1)
                {
                    if (player.GetComponent<Rigidbody2D>().position.x < curpos.x)
                    {
                        angle_min = mid;
                    }
                    else
                    {
                        angle_max = mid;
                    }
                }

                if (detect)
                {
                    break;
                }
            }
            if (detect)
            {
                break;
            }
        }
        StartCoroutine(delay());
    }
    */

    void Better_Search()
    {
        dir = Math.Sign(player.GetComponent<Rigidbody2D>().position.x - transform.position.x);
        switch (dir)
        {
            case 1:
                for (int i = 90; i < 180; i ++)
                {
                    angle = i;
                    StartCoroutine(Try_angle());
                    if (detect)
                    {
                        break;
                    }
                }
                break;
            case -1:
                for (int i = -90; i > -180; i --)
                {
                    angle = i;
                    StartCoroutine(Try_angle());
                    if (detect)
                    {
                        break;
                    }
                }
                break;

            default:
                break;
        }
        StartCoroutine(delay());
    }

    IEnumerator Try_angle()
    {
        detect = false;
        float power_max = 20;
        float power_min = 0;
        for (int j = 0; j < 5; j++)
        {
            float mid = (power_max + power_min) / 2;
            float x = Mathf.Cos((angle - 90) * dir * Mathf.PI / 180);
            float y = Mathf.Sin((angle - 90) * Mathf.PI / 180);
            Vector2 velocity = new Vector2(x * mid, y * mid);
            Vector2 curpos = new Vector2(transform.position.x + x, transform.position.y + y);
            Vector2 lastpos = curpos;
            Vector2 gravity = new Vector2(0, -9.8f);
            int bounces = 5;
            int temp = 0;
            while (!detect)
            {
                velocity += gravity * Time.fixedDeltaTime;
                curpos += velocity * Time.fixedDeltaTime;
                Debug.DrawLine(lastpos, curpos);
                var raycast = Physics2D.CircleCast(curpos, 0.25f, Vector2.zero);
                if (raycast.collider)
                {
                    if (raycast.collider != null)
                    {
                        if (raycast.collider.transform.tag == "Map")
                        {
                            velocity = Vector2.Reflect(velocity, raycast.normal);
                            velocity *= (-raycast.normal * 0.6f); 
                            if (bounces <= 0)
                            {
                                break;
                            }
                            bounces -= 1;
                        }
                        else if (raycast.collider.transform.tag == "Player")
                        {
                            detect = true;
                            print(mid);
                            AI_Shoot(new Vector2(transform.position.x + x, transform.position.y + y), new Vector2(x * mid, y * mid));
                        }
                    }
                }
                temp++;
                if (temp >= 2000)
                {
                    break;
                }
                lastpos = curpos;
            }
            if (dir == 1)
            {
                if (player.GetComponent<Rigidbody2D>().position.x > curpos.x)
                {
                    power_min = mid;
                }
                else if (curpos.x > player.GetComponent<Rigidbody2D>().position.x)
                {
                    power_max = mid;
                }
            }
            else if (dir == -1)
            {
                if (player.GetComponent<Rigidbody2D>().position.x < curpos.x)
                {
                    power_min = mid;
                }
                else if (curpos.x < player.GetComponent<Rigidbody2D>().position.x)
                {
                    power_max = mid;
                }
            }
            if (detect)
            {
                break;
            }
        }
        yield return null;
    }
    private void AI_Shoot(Vector3 spawn, Vector3 direction)
    {
        GameObject proj = Instantiate(Bullet, spawn, transform.rotation);
        proj.GetComponent<Move_bullet>().SetBullet((Vector3)direction);
        //powering = false;
        power = 0;
        //camera.GetComponent<follow_player>().follow = proj;
    }
}
