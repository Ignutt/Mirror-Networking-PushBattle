using Player;
using UnityEngine;

namespace Gameplay.Player.States
{
    public class DashState : PlayerState
    {
        private readonly Transform _transform;
        private readonly float _dashSpeed;
        
        public DashState(Player player, float dashSpeed) : base(player)
        {
            _transform = player.transform;
            _dashSpeed = dashSpeed;
        }

        public override void Process()
        {
        }

        public override void EnterState()
        {
            base.EnterState();
            Dashing();
        }

        private void Dashing()
        {
            player.Rigidbody.AddForce(_transform.forward * _dashSpeed);
        }
    }
}