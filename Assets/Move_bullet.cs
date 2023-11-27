using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_bullet : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBullet(Vector3 direction)
    {
        GetComponent<Rigidbody2D>().velocity = direction;
    }
}
