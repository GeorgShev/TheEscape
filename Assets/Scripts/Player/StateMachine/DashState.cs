using Services;
using Services.InputService;
using UnityEngine;

namespace Player.StateMachine
{
    public class DashState : State
    {
        private Vector2 _inputAxis;
        private Vector2 _currentInputVector;
        private Vector2 _smoothInputVelocity;
        private float _smoothInputSpeed = .2f;
        private float _dashRange = 1.5f;
        private CharacterController _characterController;
        private Vector3 _movementVector;
        private Vector3 _multipliedMatrix;
        //private bool _canDash = false;


        public DashState(PlayerController player, Animator animator) : base(player, animator)
        {
            Player = player;
            _animator = animator;
            _characterController = player.characterController;
            //_canDash = true;
        }

        public override void Enter()
        {
            Player.Animator.CrossFade(_animationNames.RunHash, 0.1f);
            
            
            _characterController.Move(Player.transform.forward * _dashRange);
            
            Debug.LogError(("dash entered"));
        }

        public override void Exit()
        {
            Player.Animator.StopPlayback();
        }

        
    }
}