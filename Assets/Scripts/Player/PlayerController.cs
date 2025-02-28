using Player.StateMachine;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public CharacterController CharacterController;
        public float MovementSpeed;
        public PlayerRotate PlayerRotate;
        public Animator Animator;   

        [SerializeField] private float smoothInputSpeed = .2f;

    
        private Vector2 _axis;
        private Vector2 _currentInputVector;
        private Vector2 _smoothInputVelocity;
    
        private InputSystem_Actions _inputSystemActions;
        private StateMachine.StateMachine _stateMachine;
        private RunState _runState;
        private IdleState _idleState;


        private void Awake()
        {
            _inputSystemActions = new InputSystem_Actions();
        }

        private void OnEnable()
        {
            _inputSystemActions.Player.Enable();
        }

        public void Start()
        {
            _stateMachine = new StateMachine.StateMachine();
            _idleState = new IdleState(this, Animator);
            _runState = new RunState(this, Animator);
            _stateMachine.Initialize(_idleState);
        }

        private void OnDisable()
        {
            _inputSystemActions.Player.Disable();
        }


        private void Update()
        {
            _stateMachine.CurrentState?.Update();
            CalculateMovementVector();
            ChangeState();
        }

        private void ChangeState()
        {
            if (CharacterController.velocity.sqrMagnitude < Constants.Elipson)
            {
                _stateMachine.ChangeState(_idleState);
            }
            else
            {
                _stateMachine.ChangeState(_runState);
            }
        }

        private void CalculateMovementVector()
        {
            Vector2 axis = _inputSystemActions.Player.Move.ReadValue<Vector2>();
            _currentInputVector = Vector2.SmoothDamp(_currentInputVector, axis, ref _smoothInputVelocity, smoothInputSpeed);
            Vector3 movementvector = new Vector3(_currentInputVector.x, 0, _currentInputVector.y);
         
            Matrix4x4 isometricMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
            Vector3 multipliedMatrix = isometricMatrix.MultiplyPoint3x4(movementvector);

            if (axis.sqrMagnitude > Constants.Elipson)
            {
                PlayerRotate.UpdateMovementVector(multipliedMatrix);
            }
            
            CharacterController.Move(multipliedMatrix * (MovementSpeed * Time.deltaTime));
         

        }
    
    }
}
