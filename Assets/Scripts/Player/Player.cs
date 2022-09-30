using System;
using Player.States;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [Header("Moving properties")]
        [SerializeField] private float speedMoving = 5f;
        [SerializeField] private float smoothAngleTime = 0.1f;
        [SerializeField] private Transform playerCamera;
        
        [Header("Dash properties")]
        [SerializeField] private float dashSpeed = 4;
        [SerializeField] private float dashDistance = 2;
        
        private IdleState _idleState;
        private RunState _runState;
        private DashState _dashState;
        private PlayerState _currentState;
        
        public CharacterController CharacterController { get; private set; }
        public GameActions GameActions { get; private set; }
        
        private void Awake()
        {
            CharacterController = GetComponent<CharacterController>();
            
            GameActions = new GameActions();
            GameActions.Enable();
            GameActions.Player.Dash.performed += context =>
            {
                SetState(_dashState);
            };

            _idleState = new IdleState(this);
            _runState = new RunState(this, speedMoving, smoothAngleTime, playerCamera);
            _dashState = new DashState(this, dashSpeed, dashDistance);

            _idleState.Initialize();
            _runState.Initialize();
            _dashState.Initialize();
        }

        private void Start()
        {
            _currentState = _idleState;
        }

        private void Update()
        {
            _currentState.Process();
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
