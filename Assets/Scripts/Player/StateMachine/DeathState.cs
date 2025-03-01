using UnityEngine;

namespace Player.StateMachine
{
    public class DeathState: State
    {
        private PlayerDeath _playerDeath;
        public DeathState(PlayerController player, Animator animator, PlayerDeath playerDeath) : base(player, animator)
        {
            Player = player;
            _animator = animator;
            _playerDeath = playerDeath;
        }

        public override void Enter()
        {
            Player.Animator.CrossFade(_animationNames.IdleHash, 0.1f);
            _playerDeath.Die();
            Debug.LogError(("death entered"));
        }

        public override void Exit()
        {
            Player.Animator.StopPlayback();
        }
    
    }
}
