using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Aim_sprite : MonoBehaviour
{
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        Quaternion target = Quaternion.Euler(0f, 0f, player.GetComponent<Movement>().angle-90);
        transform.rotation = target;
    }
}
