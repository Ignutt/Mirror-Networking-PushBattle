using System;
using Mirror;
using TMPro;
using UnityEngine;

namespace Player
{
    public class PlayerNetwork : NetworkBehaviour
    {
        [SerializeField] private TextMeshPro nameText;

        [SyncVar]
        public string playerName;

        private void Update()
        {
            nameText.text = playerName;
        }
    }
}