using System;
using Mirror;
using UnityEngine;
using Utils = Common.Utils;

namespace Gameplay.Player
{
    public class ContusionState : NetworkBehaviour
    {
        [Header("Player")] 
        [SerializeField] private Player player;
        
        [Header("Contusion properties")]
        [SerializeField] private Color contusionColor = Color.cyan;
        [SerializeField] private float contusionTime = 3f;
        
        private Color _defaultColor;
        
        [SyncVar]
        private Color _syncCurrentColor;
        private Color _currentColor;

        private bool _isContusion = false;

        private void Start()
        {
            _defaultColor = player.Graphic.GetComponent<MeshRenderer>().material.color;
        }


        [Client]
        private void OnCollisionEnter(Collision other)
        {
            if (!isLocalPlayer) return;
            if (!other.transform.GetComponent<PlayerNetwork>()) return;
            Player attackPlayer = other.transform.GetComponent<PlayerNetwork>().player;
            
            if (attackPlayer.CurrentState == attackPlayer.DashState && !_isContusion && player.CurrentState != player.DashState)
            {
                StartContusion();
            }
            
        }
        
        [Server]
        private void SyncColorVar()
        {
            _syncCurrentColor = _currentColor;
        }
        
        [ClientRpc]
        private void RpcSetContusionColor()
        {
            if (!isClient) return;

            _currentColor = contusionColor;
            player.Graphic.GetComponent<MeshRenderer>().material.color = _currentColor;
        }

        [ClientRpc]
        private void RpcSetDefaultColor()
        {
            if (!isClient) return;

            _currentColor = _defaultColor;
            player.Graphic.GetComponent<MeshRenderer>().material.color = _currentColor;
        }

        [Command]
        private void StartContusion()
        {
            RpcSetContusionColor();
            SyncColorVar();
            _isContusion = true;

            StartCoroutine(Utils.MakeActionDelay(delegate
            {
                RpcSetDefaultColor();
                SyncColorVar();
                _isContusion = false;
            }, contusionTime));
        }
    }
}
