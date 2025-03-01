using System;
using Logic;
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
        public PlayerDeath PlayerDeath;
        public bool isKnockback;
        public Action KnockbackAction;
        public Action RecoverAction;
        

        [SerializeField] private float smoothInputSpeed = .2f;


        private Vector2 _axis;
        private Vector2 _currentInputVector;
        private Vector2 _smoothInputVelocity;
        private Vector2 _inputAxis;

        private InputSystem_Actions _inputSystemActions;
        private StateMachine.StateMachine _stateMachine;
        private IHealth _health;
        private RunState _runState;
        private IdleState _idleState;
        private DashState _dashState;
        private KnockoutState _knockoutState;
        private DeathState _deathState;
        
        private bool _isDead;

        public void Construct(IHealth health)
        {
            _health = health;
            HealthChangedSubscribe();
            
        }


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
            _knockoutState = new KnockoutState(this, Animator);
            _deathState = new DeathState(this, Animator,PlayerDeath);
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
            if (CanRun())
            {
                _stateMachine.ChangeState(_runState);
            }
            else if(CanDash())
            {
                _stateMachine.ChangeState(_dashState);
            }
            else if(KnockedBack())
            {
                _stateMachine.ChangeState(_knockoutState);
            }
            else if (Die())
            {
                _stateMachine.ChangeState(_deathState);
            }
            else
            {
                _stateMachine.ChangeState(_idleState);
            }
        }

        private bool CanRun()
        {
            bool canRun = (_inputAxis.sqrMagnitude > Constants.Elipson 
                           && AbilityHolder.state != AbilityHolder.AbilityState.active
                           && !Die() 
                           && !KnockedBack());
            
            return canRun;
        }

        private bool CanDash()
        {
            bool canDash = AbilityHolder.state == AbilityHolder.AbilityState.active;
            return canDash;
        }

        private bool KnockedBack()
        {
            bool canKnockback = isKnockback && !_isDead;
            return canKnockback;
        }
        
        private bool Die()
        {
            bool die = _isDead;
            return die;
        }
        

        private void GatherInput()
        {
            _inputAxis = _inputSystemActions.Player.Move.ReadValue<Vector2>();
        }
        
        private void HealthChangedSubscribe()
        {
            _health.HealthChanged += HealthChanged;
        }
        
        
        private void HealthChanged()
        {
            if(!_isDead && _health.CurrentHP < 0)
            {
                _isDead = true;
            }
            else if (!_isDead && _health.CurrentHP >= 0)
            {
                isKnockback = true;
            }
        }
        
    }
}