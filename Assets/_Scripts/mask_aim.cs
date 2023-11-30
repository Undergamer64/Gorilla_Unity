using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class mask_aim : MonoBehaviour
{
    public GameObject Player;
    public RectMask2D mask;

    // Update is called once per frame
    void Update()
    {
        mask.padding = new Vector4(0,0, 1.6f - Player.GetComponent<Movement>().power/20*1.6f,0);
    }
}
