using System;
using Mirror;
using TMPro;
using UnityEngine;

namespace Player
{
    public class PlayerNetwork : NetworkBehaviour
    {
        [SerializeField] private TextMeshPro nameText;

        [SyncVar(hook = nameof(SetPlayerName))]
        private string _playerName;

        private void SetPlayerName(string oldValue, string newValue)
        {
            _playerName = newValue;
        }

        public string PlayerName
        {
            get => _playerName;
            set
            {
                nameText.text = value;
                _playerName = value;
            }
        }

        public void SetName(string value)
        {
            if (isServer)
            {
                ChangeName(value);
            }
            else
            {
                CmdChangeName(value);
            }
        }        

        [Server]
        public void ChangeName(string newValue)
        {
            PlayerName = newValue;
        }

        [Command]
        public void CmdChangeName(string newValue)
        {
            PlayerName = newValue;
        }
    }
}