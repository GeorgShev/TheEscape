using System;
using Infrastructure.State;
using Services;
using Services.PauseService;
using UI.Services.Windows;
using UnityEngine;

namespace UI.Windows.Menu
{

    public class PauseMenu : WindowBase
    {
        private IGameStateMachine _gameStateMachine;
        private IWindowService _windowService;

        private ReturnToMainMenu _returnMainMenu;
        private RestartLevel _restartLevel;

        private IPauseService _pauseService;


        public void Construct(IGameStateMachine gameStateMachine, IWindowService windowService, IPauseService pauseService)
        {
            _gameStateMachine = gameStateMachine;
            _windowService = windowService;
            _returnMainMenu = GetComponentInChildren<ReturnToMainMenu>();
            _restartLevel = GetComponentInChildren<RestartLevel>();
            //need refactoring
            _pauseService = pauseService;
            
            _pauseService.SetPause(true);


            SendLinks();

        }

        public void SendLinks()
        {
            if (_returnMainMenu)
            {
                _returnMainMenu.Construct(_gameStateMachine);
            }
            if (_restartLevel)
            {
                _restartLevel.Construct(_gameStateMachine);
            }
        }
        private void OnDestroy()
        {
            _pauseService.SetPause(false);
        }
    }
}
