﻿using System;
using UnityEngine;

namespace Player.States
{
    public class RunState : PlayerState
    {
        private readonly float _speedMoving;
        private readonly float _smoothAngleTime;
        private readonly Transform _playerCamera;
        
        private readonly Transform _transform;
        private readonly CharacterController _characterController;
        private float _smoothAngle;

        public RunState(Player player, float speedMoving, float smoothAngleTime, Transform playerCamera) : base(player)
        {
            _speedMoving = speedMoving;
            _smoothAngleTime = smoothAngleTime;
            _playerCamera = playerCamera;

            _transform = player.transform;
            _characterController = player.CharacterController;
        }

        public override void Process()
        {
            Move();
        }
        
        private void Move()
        {
            float moveX = player.GameActions.Player.MoveHorizontal.ReadValue<float>();
            float moveZ = player.GameActions.Player.MoveVertical.ReadValue<float>();
        
            Vector3 direction = new Vector3(moveX, 0, moveZ).normalized;

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _playerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(
                _transform.eulerAngles.y,
                targetAngle, 
                ref _smoothAngle, 
                _smoothAngleTime);
        
        
            _transform.rotation = Quaternion.Euler(0, angle, 0);
            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

            _characterController.Move(moveDirection * _speedMoving * Time.deltaTime);
    }
    }
}