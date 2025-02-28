using Data;
using Infrastructure.State;
using Services;
using Services.PersistentProgressService;
using UI.Services.Windows;
using UnityEngine;

namespace Logic.Gates
{
    public class FinishScreenGate : MonoBehaviour , IGatesStatus
    {


        public string Button1Text = "menu_mainMenu_button";
        public string ChoiseHeadding = "menu_finish_screen_heading";
        public string ChoiseBody = "menu_finish_screen_body";

        private string _mainMenu = "MainMenu";


        private IGameStateMachine _gameStateMachine;
        private IWindowService _windowService;
        private IPersistentProgressService _persistentProgressService;


        private void Awake()
        {
            _gameStateMachine = AllServices.Container.Single<IGameStateMachine>(); ;
            _windowService = AllServices.Container.Single<IWindowService>();
            _persistentProgressService = AllServices.Container.Single<IPersistentProgressService>();
        }

        public void UpdateStatus()
        {
            Invoke("CreateChoiseWindow", 0.5f);
        }

        public void CreateChoiseWindow()
        {
            _windowService.Open(WindowId.Confim);
            _windowService.ConfimWindow.Construct(Button1Text, ChoiseHeadding, ChoiseBody);
            _windowService.ConfimWindow.Choice1 += GameOver;
        }


        public void GameOver()
        {
            _persistentProgressService.Progress = NewProgress();
            _gameStateMachine.Enter<LoadLevelState, string>(_mainMenu);
        }

        public PlayerProgress NewProgress()
        {
            var progress = new PlayerProgress(initialLevel: _mainMenu);



            return progress;
        }

    }
}