using System;
using UnityEngine;

namespace Player
{
    public class PlayerPhysics : MonoBehaviour
    {
        [Header("GroundChecker")] 
        [SerializeField] private Transform groundCheckPoint;
        [SerializeField] private float checkRadius = .5f;
        [SerializeField] private LayerMask whatIsGround;

        [Header("Physics")] 
        [SerializeField] private float gravity = -9.81f;
        [SerializeField] private float frictionFactor = 5f;
        
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
            _velocity.x = Mathf.MoveTowards(_velocity.x, 0, Time.deltaTime * frictionFactor);
            _velocity.z = Mathf.MoveTowards(_velocity.z, 0, Time.deltaTime * frictionFactor);
            _characterController.Move(_velocity * Time.deltaTime);
        }

        public void AddForce(Vector3 force)
        {
            _velocity += force;
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