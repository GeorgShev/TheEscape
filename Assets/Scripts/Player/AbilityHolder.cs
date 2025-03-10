using Services;
using Services.InputService;
using Services.PauseService;
using UI.Elements;
using UnityEngine;

namespace Player
{
    public class AbilityHolder : MonoBehaviour
    {
        public float cooldownTime;
        public float activeTime;
        public AbilityState state;

        public bool IsAbilityUse;
        private bool _dashInput;


        //public List<Ability> passiveAbilities;

        private float _pausedCooldownTime;
        private float _pausedActiveTime;
        private int _pausedIndexState;
        private AbilityState _pauseState;
        private AbilityUI _abilityUi;
        private IInputService _inputService;
        private IPauseService _pauseService;
        public float _cooldownTime;

        public  enum AbilityState
        {
            ready,
            active,
            cooldown
        }
        
        public void Construct(IPauseService pauseService)
        {
            _pauseService = pauseService;
        }

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
        }
        

        public void InitHUD(AbilityUI abilityUI)
        {
            _abilityUi = abilityUI;
            //_abilityUi.AbilityButton.gameObject.SetActive(true);
        }


        private void Update()
        {

            if (_pauseService != null && _pauseService.IsPaused)
            {
                return; 
            }
            
            _dashInput = _inputService.IsDashButtonUp();

            state = UpdateAbilityState(state, Time.deltaTime);
            
        }

        private AbilityState UpdateAbilityState(AbilityState currentState, float deltaTime)
        {
            switch (state)
            {
                case AbilityState.ready:
                    if ((IsAbilityUse || _dashInput))
                    {
                        currentState = AbilityState.active;
                        activeTime = activeTime;
                        _cooldownTime = cooldownTime;
                        IsAbilityUse = false;
                    }

                    break;
                case AbilityState.active:
                    if (activeTime > 0)
                    {
                        activeTime -= Time.deltaTime;
                        _abilityUi?.AbilityButton.ButtonActive(activeTime);
                        IsAbilityUse = false;
                    }
                    else
                    {
                        currentState = AbilityState.cooldown;
                        _cooldownTime = cooldownTime;
                        _abilityUi?.AbilityButton.ButtoonCooldown(_cooldownTime, cooldownTime);
                        IsAbilityUse = false;
                    }

                    break;
                case AbilityState.cooldown:
                    if (_cooldownTime > 0)
                    {
                        _cooldownTime -= Time.deltaTime;
                        _abilityUi?.AbilityButton.ButtoonCooldown(_cooldownTime, cooldownTime);
                        IsAbilityUse = false;
                    }
                    else
                    {
                        currentState = AbilityState.ready;
                        _abilityUi?.AbilityButton.ButtonReady();
                    }

                    break;
            }

            return currentState;
        }

        public void RefreshAbility()
        {
            activeTime = 0f;
            _cooldownTime = 0;
            _pausedCooldownTime = 0;
            _pausedActiveTime = 0;
            //_pauseState = 0;

            state = AbilityState.ready;

            if (_abilityUi?.AbilityButton)
            {
                _abilityUi.AbilityButton.ButtonReady();
            }
        }

        
    }
}