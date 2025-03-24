using Services;
using Services.AudioService;
using Services.InputService;
using StaticData;
using UnityEngine;

namespace Player.StateMachine
{
    public class DashState : State
    {
        private Vector2 _inputAxis;
        private Vector2 _currentInputVector;
        private Vector2 _smoothInputVelocity;
        private float _smoothInputSpeed = .2f;
        private float _dashRange = 3f;
        private CharacterController _characterController;
        private IAudioService _audioService;
        private Vector3 _movementVector;
        private Vector3 _multipliedMatrix;

        private bool isDashing;
        private float dashTimer;
        private float dashDuration = .25f;
        
        private Vector3 dashStartPosition;
        private Vector3 dashTargetPosition; 


        public DashState(PlayerController player, Animator animator, IAudioService audioService) : base(player, animator)
        {
            Player = player;
            _animator = animator;
            _characterController = player.characterController;
            _audioService = audioService;
            //_canDash = true;
        }

        public override void Enter()
        {
            Player.Animator.CrossFade(_animationNames.RunHash, 0.1f);
            
            _audioService.PlaySound(AudioTypeId.PlayerDash);

            StartDash();
        }

        public override void Update()
        {
            if (Player.isDashing)
            {
                dashTimer += Time.deltaTime;

                float progress = dashTimer / dashDuration;
                Vector3 newPosition = Vector3.Lerp(dashStartPosition, dashTargetPosition, progress);

                
                _characterController.Move(newPosition - Player.transform.position);

                if (progress < 0.2f)
                {
                    Player.catMaterial.SetFloat("_NoisePower", Mathf.Lerp(0f, 0.1f, progress / .2f));
                }
                else if (progress < 0.8f)
                {
                    Player.catMaterial.SetFloat("_NoisePower",.1f);
                }
                else
                {
                    Player.catMaterial.SetFloat("_NoisePower",Mathf.Lerp(0.1f, 0f, (progress - 0.8f) / .2f));
                }

                if (dashTimer >= dashDuration)
                {
                    isDashing = false;
                    Player.isDashing = false;
                }
            }
            
        }

        public override void Exit()
        {
            Player.catMaterial.SetFloat("_NoisePower",0f);
            Player.isDashing = false;
            Player.Animator.StopPlayback();
        }
        
        private void StartDash()
        {
            Player.isDashing = true;
            isDashing = true;
            dashTimer = 0f;
            dashStartPosition = Player.transform.position;
            dashTargetPosition = dashStartPosition + Player.transform.forward * _dashRange;
        }

        
    }
}