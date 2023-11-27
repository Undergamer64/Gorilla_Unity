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
        //transform.Rotate(0, 0, transform.rotation.z - player.GetComponent<Movement>().angle);
        Quaternion target = Quaternion.Euler(0f, 0f, player.GetComponent<Movement>().angle-90);
        transform.rotation = target;
    }
}
