using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_actions : MonoBehaviour
{
    public Rigidbody2D AI;

    [Header("Movement")]
    public float movespeed = 100;
    float horizontalmove = 0;
    public int dir = 1;

    [Header("Shoot")]
    public float angle = 90;
    //private int dirangle = 0;
    public float power = 10;
    private bool powering = false;
    [SerializeField] GameObject Bullet;

    // Update is called once per frame
    void Update()
    {
        AI.velocity = new Vector2 (horizontalmove, 0);
        Search();
        Shoot();
    }

    private void Search()
    {
        bool detect = false;
        for (int i = 1; i < 3; i++) 
        {
            power = i*10;
            float angle_max = 180;
            float angle_min = 45;
            for (int j = 0; j < 10; j++)
            {
                float mid = (angle_max + angle_min)/2;


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
    }

    private void Shoot()
    {

    }
}
