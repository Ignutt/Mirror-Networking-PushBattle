using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;
using Utils = Common.Utils;

public class LobbyController : NetworkBehaviour
{
    public static LobbyController Instance;
    [SerializeField] private TMP_InputField nameInputField;
    
    private readonly List<PlayerLobbyUI> _players = new List<PlayerLobbyUI>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        nameInputField.text = Utils.PlayerName;
    }

    public void SavePlayerName()
    {
        if (string.IsNullOrEmpty(nameInputField.text)) return;
        
        Utils.PlayerName = nameInputField.text;
    }

    public void AddPlayer(PlayerLobbyUI playerLobbyUI)
    {
        _players.Add(playerLobbyUI);
    }
}
