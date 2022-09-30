using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NetworkData : MonoBehaviour
{
    public static NetworkData Instance;

    [SerializeField] private TMP_InputField inputField;

    public Player.Player LocalPlayer { get; set; }
    
    
    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void SetPlayerName()
    {
        LocalPlayer.PlayerNetwork.SetName(inputField.text);
    }
}
