using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersLayout : MonoBehaviour
{
    public static PlayersLayout Instance;

    private void Awake()
    {
        Instance = this;
    }
}
