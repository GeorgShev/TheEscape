using UnityEngine;

namespace Player.StateMachine
{
    public class RunState : State
    {
        public RunState(PlayerController player, Animator animator) : base(player, animator)
        {
            Player = player;
            _animator = animator;
        }
        public override void Enter()
        {
            Player.Animator.CrossFade(_animationNames.RunHash, 0.1f);
        }

        public override void Exit()
        {
            Player.Animator.StopPlayback();
        }
    }
}
