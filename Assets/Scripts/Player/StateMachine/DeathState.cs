using Services.AudioService;
using StaticData;
using UnityEngine;

namespace Player.StateMachine
{
    public class DeathState: State
    {
        private PlayerDeath _playerDeath;
        private IAudioService _audioService;

        public DeathState(PlayerController player, Animator animator, PlayerDeath playerDeath,
            IAudioService audioService) : base(player, animator)
        {
            Player = player;
            _animator = animator;
            _playerDeath = playerDeath;
            _audioService = audioService;
        }

        public override void Enter()
        {
            Player.Animator.CrossFade(_animationNames.IdleHash, 0.1f);
            _audioService.PlaySound(AudioTypeId.PlayerDie3);
            _playerDeath.Die();
            
            Debug.LogError(("death entered"));
        }

        public override void Exit()
        {
            Player.Animator.StopPlayback();
        }
    
    }
}
