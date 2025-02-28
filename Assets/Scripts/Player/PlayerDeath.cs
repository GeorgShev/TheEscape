using Data;
using Infrastructure.State;
using Services.PersistentProgressService;
using UI.Services.Windows;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    [RequireComponent(typeof(PlayerDeath))]
    public class PlayerDeath : MonoBehaviour
    {
        public PlayerController playerController;

        public GameObject DeathFx;

        public string Button1Text = "menu_mainMenu_button";
        public string ChoiseHeadding = "menu_death_choise_heading";
        public string ChoiseBody = "menu_death_choise_body";

        private string _mainMenu = "MainMenu";


        private bool _isDead =false;
        private IGameStateMachine _gameStateMachine;
        private IWindowService _windowService;
        private IPersistentProgressService _persistentProgressService;
        private PlayerHealth _playerHealth;

        public void Construct(IGameStateMachine gameStateMachine, IWindowService windowService, IPersistentProgressService persistentProgressService, PlayerHealth playerHealth)
        {
            _gameStateMachine = gameStateMachine;
            _windowService = windowService;
            _persistentProgressService = persistentProgressService;
            _playerHealth = playerHealth;
            HealthChangedSubscribe();
        }

        private void HealthChangedSubscribe()
        {
            _playerHealth.HealthChanged += HealthChanged;
        }

        private void OnDestroy()
        {
            _playerHealth.HealthChanged -= HealthChanged;
        }

        private void HealthChanged()
        {
            if(!_isDead && _playerHealth.CurrentHP <= 0)
            {
                Die();

            }
            //Debug.Log(heroHealth.CurrentHP);
        }

        private void Die()
        {
            _isDead = true;
            playerController.enabled = false;
            //Animator.Play(Die);

            //Destroy(gameObject);
            gameObject.SetActive(false);

            //Instantiate(DeathFx, transform.position, Quaternion.identity);

            Invoke("GameOverWindow", 1f);

            

            //Debug.Log(heroHealth.CurrentHP);

        }

        private void GameOverWindow()
        {
            CreateChoiseWindow();
            
        }

        private void CreateChoiseWindow()
        {
            _windowService.Open(WindowId.Confim);
            _windowService.ConfimWindow.Construct(Button1Text, ChoiseHeadding, ChoiseBody);
            _windowService.ConfimWindow.Choice1 += GameOver;
        }


        private void GameOver()
        {
            _persistentProgressService.Progress = null;
            _gameStateMachine.Enter<LoadLevelState, string>(_mainMenu);
        }

        private PlayerProgress NewProgress()
        {
            var progress = new PlayerProgress(initialLevel: _mainMenu);



            return progress;
        }

    }
}