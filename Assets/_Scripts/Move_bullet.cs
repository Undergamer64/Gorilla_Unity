using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_bullet : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(SelfDestruct());
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(7f);
        Destroy(gameObject);
    }

    public void SetBullet(Vector3 direction)
    {
        GetComponent<Rigidbody2D>().velocity = direction;
    }
}
