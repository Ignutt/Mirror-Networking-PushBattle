using System.Collections;
using System.Collections.Generic;
using Mirror;
using Player;
using UnityEngine;

public class CollisionHandler : NetworkBehaviour
{
    [ServerCallback]
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Enter");
        //other.transform.GetComponent<PlayerNetwork>().currentColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
}
