using System;
using Gameplay.Player.States;
using Mirror;
using Player;
using Player.States;
using UnityEngine;
using Random = UnityEngine.Random;
using Utils = Common.Utils;

namespace Gameplay.Player
{
    public class Player : NetworkBehaviour
    {
        [Header("General properties")]
        [SerializeField] private Transform graphic;
        [SerializeField] private PlayerCamera playerCameraPrefab;
        
        [Header("Moving properties")]
        [SerializeField] private float speedMoving = 5f;
        [SerializeField] private float smoothAngleTime = 0.1f;
        
        [Header("Dash properties")]
        [SerializeField] private float dashSpeed = 4;

        [Header("Contusion properties")]
        public Material[] material;
        int curColOfThisObject;
        [SyncVar]
        private int curSyncVarOfThisObject;

        
        private IdleState _idleState;
        private RunState _runState;
        private DashState _dashState;
        private PlayerState _currentState;
        
        public GameActions GameActions { get; private set; }

        public Rigidbody Rigidbody { get; private set; }
        public MeshRenderer MeshRenderer { get; private set; }

        public float MoveX => (int) GameActions.Player.MoveHorizontal.ReadValue<float>(); 
        public float MoveZ => (int) GameActions.Player.MoveVertical.ReadValue<float>(); 
        public PlayerNetwork PlayerNetwork => GetComponent<PlayerNetwork>();

        private PlayerCamera _playerCamera;
        
        private void Start()
        {
            if (!isLocalPlayer)
            {
                Destroy(this);
                return;
            }

            GlobalCamera.Instance.gameObject.SetActive(false);
            _playerCamera = Instantiate(playerCameraPrefab);
            _playerCamera.Cinemachine.Follow = transform;
            _playerCamera.Cinemachine.LookAt = transform;
            
            GameActions = new GameActions();
            GameActions.Enable();
            GameActions.Player.Dash.performed += context =>
            {
                SetState(_dashState);
            };
            
            Rigidbody = GetComponent<Rigidbody>();
            MeshRenderer = graphic.GetComponent<MeshRenderer>();
            
            InitializeStates();
        }

        private void Update()
        {
            _currentState?.Process();
            CheckState();

            if (!isLocalPlayer)
            {
                return;
            }
            
        }

        [Client]
        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.GetComponent<PlayerNetwork>())
            {
                CmdNextColor(gameObject);
            }
        }

        private void InitializeStates()
        {
            _idleState = new IdleState(this);
            _runState = new RunState(this, speedMoving, smoothAngleTime, _playerCamera.Camera.transform);
            _dashState = new DashState(this, dashSpeed);

            _idleState.Initialize();
            _runState.Initialize();
            _dashState.Initialize();
            
            _currentState = _idleState;
        }

        private void CheckState()
        {
            if (_currentState == _dashState)
            {
                if (MoveX != 0 || MoveZ != 0)
                {
                    Rigidbody.velocity = Vector3.zero;
                    SetState(_runState);
                }
                return;
            }
            
            if (MoveX != 0 || MoveZ != 0)
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

        public override void OnStartServer()
        {
            base.OnStartServer();
        }
        
        [Server]
        private void SyncColorVar()
        {
            curSyncVarOfThisObject = curColOfThisObject;
        }
        
        [ClientRpc]
        private void RpcNextColor(int newValue)
        {
            if (!isClient)
                return;

            if (newValue >= material.Length)
            {
                return;
            }

            ActualChangeColorCode();
        }
        
        void ActualChangeColorCode ()
        {
            curColOfThisObject++;
            if (curColOfThisObject >= material.Length)
                curColOfThisObject = 0;
            
            Debug.Log("Change material");
            graphic.GetComponent<MeshRenderer>().material = material[curColOfThisObject];

        }

        private int GetCurColor()
        {
            return curColOfThisObject;
        }
        
        [Command]
        private void CmdNextColor(GameObject hitObject)
        {
            int curColor = GetCurColor();

            RpcNextColor(curColor);

            SyncColorVar();
        }




        
    }
}
