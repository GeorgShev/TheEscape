using Player.StateMachine;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public CharacterController characterController;
        public Animator Animator;
        public AbilityHolder AbilityHolder;

        [SerializeField] private float smoothInputSpeed = .2f;


        private Vector2 _axis;
        private Vector2 _currentInputVector;
        private Vector2 _smoothInputVelocity;

        private InputSystem_Actions _inputSystemActions;
        private StateMachine.StateMachine _stateMachine;
        private RunState _runState;
        private IdleState _idleState;
        private DashState _dashState;
        private Vector2 _inputAxis;


        private void Awake()
        {
            _inputSystemActions = new InputSystem_Actions();
        }

        private void OnEnable()
        {
            _inputSystemActions.Player.Enable();
            AbilityHolder.Construct(_inputSystemActions);
        }

        public void Start()
        {
            _stateMachine = new StateMachine.StateMachine();
            _idleState = new IdleState(this, Animator);
            _runState = new RunState(this, Animator,  _inputSystemActions);
            _dashState = new DashState(this, Animator,  _inputSystemActions);
            _stateMachine.Initialize(_idleState);
        }

        private void OnDisable()
        {
            _inputSystemActions.Player.Disable();
        }


        private void Update()
        {
            _stateMachine.CurrentState?.Update();
            GatherInput();
            ChangeState();
        }

        private void ChangeState()
        {
            if (_inputAxis.sqrMagnitude > Constants.Elipson && AbilityHolder.state != AbilityHolder.AbilityState.active)
            {
                _stateMachine.ChangeState(_runState);
            }
            else if(AbilityHolder.state == AbilityHolder.AbilityState.active)
            {
                _stateMachine.ChangeState(_dashState);
            }
            else
            {
                _stateMachine.ChangeState(_idleState);
            }
        }

        private void GatherInput()
        {
            _inputAxis = _inputSystemActions.Player.Move.ReadValue<Vector2>();
        }
        
    }
}