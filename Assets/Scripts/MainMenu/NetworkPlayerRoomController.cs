using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;
using Utils = Common.Utils;

public class NetworkPlayerRoomController : NetworkRoomPlayer
{
    [SerializeField] private PlayerRoomUI roomPlayerPrefab;
    
    [SyncVar(hook = nameof(PlayerNameChanged))]
    public string playerName = "Player ";

    private PlayerRoomUI _playerRoomUI;

    private void Awake()
    {
        playerName = Utils.PlayerName;
        _playerRoomUI = Instantiate(roomPlayerPrefab, PlayersLayout.Instance.transform);
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!isLocalPlayer) return;
        RoomWindow.Instance.ReadyButton.onClick.AddListener(() =>
        {
            _playerRoomUI.isReadyCheckBox.SetActive(!_playerRoomUI.isReadyCheckBox.activeSelf);
            CmdChangeReadyState(_playerRoomUI.isReadyCheckBox.activeSelf);
        });
    }

    private void Update()
    {
        _playerRoomUI.playerName.text = playerName;
        
        if (!_playerRoomUI.isReadyCheckBox) return;
        _playerRoomUI.isReadyCheckBox.SetActive(readyToBegin);    
    }

    public virtual void PlayerNameChanged(string oldIndex, string newIndex) {}

}
