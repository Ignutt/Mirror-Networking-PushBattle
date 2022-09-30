using UnityEngine;

namespace Player.States
{
    public class DashState : PlayerState
    {
        private readonly Transform _transform;
        private float _dashSpeed;
        private float _dashDistance;

        private Vector3 _target;
        
        public DashState(Player player, float dashSpeed, float dashDistance) : base(player)
        {
            _transform = player.transform;
            _dashSpeed = dashSpeed;
            _dashDistance = dashDistance;
        }

        public override void Process()
        {
        }

        public override void EnterState()
        {
            base.EnterState();
            Dashing();
            _target = _transform.forward * _dashDistance;
        }

        private void Dashing()
        {
            player.GetComponent<PlayerPhysics>().AddForce(_transform.forward * _dashSpeed);
        }
    }
}