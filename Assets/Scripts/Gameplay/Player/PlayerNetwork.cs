using Mirror;
using TMPro;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerNetwork : NetworkBehaviour
    {
        [SerializeField] private TextMeshPro nameText;
        public Player player;
        

        [SyncVar]
        public string playerName;

        private void Update()
        {
            nameText.text = playerName;
        }
    }
}