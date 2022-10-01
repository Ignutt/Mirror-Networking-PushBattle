using Cinemachine;
using UnityEngine;

namespace Player
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private Camera playerCamera;
        [SerializeField] private CinemachineFreeLook cinemachine;

        public Camera Camera => playerCamera;
        public CinemachineFreeLook Cinemachine => cinemachine;
    }
}
