using UnityEngine;

namespace Player.StateMachine
{
    public class RunState : State
    {
        private InputSystem_Actions _inputSystemActions;
        private Vector2 _inputAxis;
        private Vector2 _currentInputVector;
        private Vector2 _smoothInputVelocity;
        private float _smoothInputSpeed = .2f;
        private float _movementSpeed = 1.5f;
        private float _rotateSpeed = 15f;
        private CharacterController _characterController;
        private Vector3 _movementVector;

        
        
        public RunState(PlayerController player, Animator animator, InputSystem_Actions inputAxis) : base(player, animator)
        {
            Player = player;
            _animator = animator;
            _inputSystemActions = inputAxis;
            _characterController = player.characterController;
        }

        public override void Update()
        {
            _inputAxis = _inputSystemActions.Player.Move.ReadValue<Vector2>();
            _currentInputVector =  Vector2.SmoothDamp(_currentInputVector, _inputAxis, ref _smoothInputVelocity, _smoothInputSpeed);
            Vector3 movementvector = new Vector3(_currentInputVector.x, 0, _currentInputVector.y);

            Matrix4x4 isometricMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
            Vector3 multipliedMatrix = isometricMatrix.MultiplyPoint3x4(movementvector);
            
            UpdateMovementVector(multipliedMatrix);
                RotateTowards();

                _characterController.Move(multipliedMatrix * (_movementSpeed * Time.deltaTime));
        }

        public override void Enter()
        {
            Player.Animator.CrossFade(_animationNames.RunHash, 0.1f);
            Debug.LogError(("run entered"));
        }

        public override void Exit()
        {
            Player.Animator.StopPlayback();
        }
        
        public void UpdateMovementVector(Vector3 movementVector)
        {
            _movementVector = movementVector;
        }
        
        private void RotateTowards()
        {
            Player.gameObject.transform.rotation = SmoothedRotation(Player.gameObject.transform.rotation, _movementVector);
        }
        private Quaternion SmoothedRotation(Quaternion rotation, Vector3 positionToLook)
        {
            return Quaternion.Lerp(rotation, TargetRotation(positionToLook), SpeedFactor());
        }
        private float SpeedFactor()
        {
            return _rotateSpeed * Time.deltaTime;
        }
        private Quaternion TargetRotation(Vector3 positionToLook)
        {
            return Quaternion.LookRotation(positionToLook);
        }
    }
}
