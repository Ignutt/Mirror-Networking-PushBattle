using System;

namespace Player.States
{
    public class IdleState : PlayerState
    {
        public override void Process()
        {
            
        }

        public IdleState(Gameplay.Player.Player player) : base(player)
        {
        }
    }
}