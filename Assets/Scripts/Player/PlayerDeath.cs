using Data;
using Infrastructure.State;
using Services.PersistentProgressService;
using UI.Services.Windows;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerDeath))]
    public class PlayerDeath : MonoBehaviour
    {

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

        public void Construct(IGameStateMachine gameStateMachine, IWindowService windowService, IPersistentProgressService persistentProgressService)
        {
            _gameStateMachine = gameStateMachine;
            _windowService = windowService;
            _persistentProgressService = persistentProgressService;
        }

        public void Die()
        {
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