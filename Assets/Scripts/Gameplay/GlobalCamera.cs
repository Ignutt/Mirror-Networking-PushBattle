using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalCamera : MonoBehaviour
{
    public static GlobalCamera Instance;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
}
