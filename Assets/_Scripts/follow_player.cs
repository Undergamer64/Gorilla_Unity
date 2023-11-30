using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow_player : MonoBehaviour
{
    public GameObject player;
    public GameObject follow;

    // Update is called once per frame
    void Update()
    {
        if (follow == null)
        {
            transform.position = new Vector3(player.transform.position.x, 8, -10);
        }
        else
        {
            transform.position = new Vector3(follow.transform.position.x, follow.transform.position.y, -10);
        }
    }
}
