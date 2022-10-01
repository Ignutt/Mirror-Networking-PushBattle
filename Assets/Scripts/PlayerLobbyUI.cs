using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Utils = Common.Utils;

public class PlayerLobbyUI : NetworkBehaviour
{
    [SyncVar(hook = nameof(HandlePlayerNameUpdate))]
    public string playerName;
    
    public TextMeshProUGUI playerNameText;
    public GameObject isReadyCheckBox;
    
    public void HandlePlayerNameUpdate(string oldValue, string newValue)
    {
        playerNameText.text = playerName;
        playerName = newValue;
    }

    public override void OnStartAuthority()
    {
        Debug.Log("OnStartAuthority");
        CmdSetPlayerName(Utils.PlayerName);
        playerNameText.text = playerName;
    }

    public override void OnStartServer()
    {
        playerNameText.text = playerName;
    }

    public override void OnStartClient()
    {
        Debug.Log("OnStartClient");
        playerNameText.text = playerName;
    }

    [Command]
    private void CmdSetPlayerName(string value)
    {
        playerName = value;
    }
}
