using UnityEngine;

namespace Player.StateMachine
{
    public class IdleState : State
    {
        public IdleState(PlayerController player, Animator animator) : base(player, animator)
        {
            Player = player;
            _animator = animator;
        }

        public override void Enter()
        {
            Player.Animator.CrossFade(_animationNames.IdleHash, 0.1f);
        }

        public override void Exit()
        {
            Player.Animator.StopPlayback();
        }
    }
}
