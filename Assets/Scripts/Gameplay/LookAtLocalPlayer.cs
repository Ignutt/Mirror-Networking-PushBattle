using System;
using Player;
using UnityEngine;

namespace Gameplay
{
    public class LookAtLocalPlayer : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private void Start()
        {
            PlayerCamera[] cameras = FindObjectsOfType<PlayerCamera>();
            foreach (var elem in cameras)
            {
                if (elem.Cinemachine.Follow.transform.GetComponent<Player.Player>().isLocalPlayer)
                {
                    target = elem.Cinemachine.transform;
                }
            }
        }

        private void Update()
        {
            if (!target) return;
            
            transform.LookAt(target);
        }
    }
}
