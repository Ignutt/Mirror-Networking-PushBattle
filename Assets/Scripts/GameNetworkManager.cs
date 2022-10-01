using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using Player;
using Utils = Common.Utils;

public class GameNetworkManager : NetworkManager
{
    [SerializeField] private PlayerLobbyUI playerLobbyUI;
    [SerializeField] private Transform layout;
    
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        //base.OnServerAddPlayer(conn);
        PlayerLobbyUI newPlayerLobbyUI = Instantiate(playerLobbyUI, layout);
        LobbyController.Instance.AddPlayer(newPlayerLobbyUI);
        newPlayerLobbyUI.playerName = Utils.PlayerName;
        NetworkServer.Spawn(newPlayerLobbyUI.gameObject);
        
        NetworkServer.AddPlayerForConnection(conn, newPlayerLobbyUI.gameObject);
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();
    }
}
