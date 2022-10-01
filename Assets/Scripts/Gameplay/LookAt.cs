using System;
using UnityEngine;

namespace Gameplay
{
    public class LookAt : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private void Update()
        {
            if (!target) return;
            
            transform.LookAt(target);
        }
    }
}
