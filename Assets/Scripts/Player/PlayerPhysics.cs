using System;
using UnityEngine;

namespace Player
{
    public class PlayerPhysics : MonoBehaviour
    {
        [Header("GroundChecker")] 
        public Transform groundCheckPoint;
        public float checkRadius = .5f;
        public LayerMask whatIsGround;

        [Header("Gravitation")] 
        public float gravity = -9.81f;
        private Vector3 _velocity;
        
        private bool _isGrounded = false;
        private CharacterController _characterController;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void FixedUpdate()
        {
            UpdateVelocity();
            CheckGround();
        }

        private void UpdateVelocity()
        {
            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = 0;
            }

            _velocity.y += gravity * Time.deltaTime;
            _characterController.Move(_velocity * Time.deltaTime);
        }

        private void CheckGround()
        {
            _isGrounded = false;
            Collider[] colliders = Physics.OverlapSphere(groundCheckPoint.position, checkRadius, whatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    _isGrounded = true;
                }
                
            }
        }
    }
}