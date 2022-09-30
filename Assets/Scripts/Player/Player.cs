using System;
using Mirror;
using Player.States;
using UnityEngine;

namespace Player
{
    public class Player : NetworkBehaviour
    {
        [Header("Spawn properties")]
        [SerializeField] private PlayerCamera playerCameraPrefab;
        
        [Header("Moving properties")]
        [SerializeField] private float speedMoving = 5f;
        [SerializeField] private float smoothAngleTime = 0.1f;
        
        [Header("Dash properties")]
        [SerializeField] private float dashSpeed = 4;
        [SerializeField] private float dashDistance = 2;
        
        private IdleState _idleState;
        private RunState _runState;
        private DashState _dashState;
        private PlayerState _currentState;
        
        public CharacterController CharacterController { get; private set; }
        public GameActions GameActions { get; private set; }

        public PlayerNetwork PlayerNetwork => GetComponent<PlayerNetwork>();

        private PlayerCamera _playerCamera;
        
        private void Start()
        {
            if (!isLocalPlayer)
            {
                Destroy(this);
                return;
            }

            NetworkData.Instance.LocalPlayer = this;

            GlobalCamera.Instance.gameObject.SetActive(false);
            _playerCamera = Instantiate(playerCameraPrefab);
            _playerCamera.Cinemachine.Follow = transform;
            _playerCamera.Cinemachine.LookAt = transform;
            
            CharacterController = GetComponent<CharacterController>();
            
            GameActions = new GameActions();
            GameActions.Enable();
            GameActions.Player.Dash.performed += context =>
            {
                SetState(_dashState);
            };

            _idleState = new IdleState(this);
            _runState = new RunState(this, speedMoving, smoothAngleTime, _playerCamera.Camera.transform);
            _dashState = new DashState(this, dashSpeed, dashDistance);

            _idleState.Initialize();
            _runState.Initialize();
            _dashState.Initialize();
            
            _currentState = _idleState;
        }

        /*private void Start()
        {
            _currentState = _idleState;
        }*/

        private void Update()
        {
            _currentState?.Process();
            CheckState();
        }

        private void CheckState()
        {
            //if (_currentState == _dashState) return;
            
            if (GameActions.Player.MoveHorizontal.ReadValue<float>()!= 0 || GameActions.Player.MoveVertical.ReadValue<float>() != 0)
            {
                if (_currentState != _runState) SetState(_runState);
            }
            else
            {
                if (_currentState != _idleState) SetState(_idleState);
            }
        }

        public void SetState(PlayerState playerState)
        {
            _currentState?.FinishState();
            _currentState = playerState;
            _currentState.EnterState();
        }
    }
}
