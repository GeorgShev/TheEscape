using System;
using Logic;
using Player.StateMachine;
using Services;
using Services.AudioService;
using Services.InputService;
using Services.PauseService;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public CharacterController characterController;
        public Animator Animator;
        public AbilityHolder AbilityHolder;
        public PlayerDeath PlayerDeath;
        public bool isKnockback;
        public bool isDashing;
        public Action KnockbackAction;
        public Action RecoverAction;
        public Material catMaterial;
        

        [SerializeField] private float smoothInputSpeed = .2f;


        private Vector2 _axis;
        private Vector2 _currentInputVector;
        private Vector2 _smoothInputVelocity;
        private Vector2 _inputAxis;

       
        private IInputService _inputService;
        private IPauseService _pauseService;
        private IAudioService _audioService;
        private StateMachine.StateMachine _stateMachine;
        private IHealth _health;
        private RunState _runState;
        private IdleState _idleState;
        private DashState _dashState;
        private KnockoutState _knockoutState;
        private DeathState _deathState;
        
        private bool _isDead;
        private bool _statesInitialized;

        public void Construct(IHealth health, IPauseService pauseService, IAudioService audioService)
        {
            _health = health;
            _pauseService = pauseService;
            _audioService = audioService;
            
            InitStates();
            HealthChangedSubscribe();
            
        }

        private void Awake()
        {
            //need refacrotinf
            _inputService = AllServices.Container.Single<IInputService>();
        }


        public void InitStates()
        {
            _stateMachine = new StateMachine.StateMachine();
            _idleState = new IdleState(this, Animator);
            _runState = new RunState(this, Animator);
            _dashState = new DashState(this, Animator, _audioService);
            _knockoutState = new KnockoutState(this, Animator, _audioService);
            _deathState = new DeathState(this, Animator,PlayerDeath, _audioService);
            _stateMachine.Initialize(_idleState);

            _statesInitialized = true;
        }


        private void Update()
        {
            if (_pauseService != null && _pauseService.IsPaused || !_statesInitialized)
            {
                return;
            }
            
            _stateMachine.CurrentState?.Update();
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
            else if(CanIdle())
            {
                _stateMachine.ChangeState(_idleState);
            }
        }

        private bool CanRun()
        {
            bool canRun = (_inputService.Axis.sqrMagnitude > Constants.Elipson 
                           && AbilityHolder.state != AbilityHolder.AbilityState.active
                           && !Die() 
                           && !Dashing() 
                           && !KnockedBack());
            
            return canRun;
        }

        private bool CanDash()
        {
            bool canDash = (AbilityHolder.state == AbilityHolder.AbilityState.active
                && !Die() 
                && !KnockedBack());
            return canDash;
        }

        private bool CanIdle()
        {
            bool canDash =( !Die() 
                && !Dashing() 
                && !KnockedBack());
            return canDash;
        }

        private bool KnockedBack()
        {
            bool canKnockback = isKnockback && !_isDead;
            return canKnockback;
        }

        private bool Dashing()
        {
            return isDashing;
        }
        
        private bool Die()
        {
            bool die = _isDead;
            return die;
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