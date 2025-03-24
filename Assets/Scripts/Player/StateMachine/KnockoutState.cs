using Services.AudioService;
using StaticData;
using UnityEngine;

namespace Player.StateMachine
{
    public class KnockoutState : State
    {
        
        private SkinnedMeshRenderer _skinnedMeshRenderer;
        private IAudioService _audioService;
        private float timer = 0f;
        private bool isFirstFunctionRunning = true;
        public KnockoutState(PlayerController player, Animator animator, IAudioService audioService) : base(player, animator)
        {
            Player = player;
            _animator = animator;
            _audioService = audioService;
        }

        public override void Enter()
        {
            Player.Animator.CrossFade(_animationNames.IdleHash, 0.1f);
            
            Player.GetComponent<PlayerController>().KnockbackAction?.Invoke();
            Player.GetComponent<PlayerHealth>().enabled = false;
            Player.GetComponent<AbilityHolder>().enabled = false;
            
            _audioService.PlaySound(AudioTypeId.PlayerKnock1);

            _skinnedMeshRenderer = Player.GetComponentInChildren<SkinnedMeshRenderer>();
        }

        public override void Update()
        {
            timer += Time.deltaTime;

            if (isFirstFunctionRunning)
            {
                if (timer < 1f) 
                {
                    
                }
                else
                {
                    
                    isFirstFunctionRunning = false;
                    timer = 0f;
                }
            }
            else
            {
                if (timer < 2f) 
                {
                    if (timer % 0.2f < 0.1f) 
                    {
                        _skinnedMeshRenderer.enabled = true;
                    }
                    else
                    {
                        _skinnedMeshRenderer.enabled = false;
                    }
                }
                else
                {
                    timer = 0f;
                    isFirstFunctionRunning = true;
                    _skinnedMeshRenderer.enabled = true; 
                    Player.RecoverAction?.Invoke();
                    Player.isKnockback = false;
                    Player.GetComponent<PlayerHealth>().enabled = true;
                    Player.GetComponent<AbilityHolder>().enabled = true;
                    Player.GetComponent<AbilityHolder>().RefreshAbility();
                    Exit();
                }
            }
        }

        public override void Exit()
        {
            Player.Animator.StopPlayback();
        }
    }
}
    

