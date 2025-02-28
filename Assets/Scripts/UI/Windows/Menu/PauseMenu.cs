using Infrastructure.State;
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


        public void Construct(IGameStateMachine gameStateMachine, IWindowService windowService)
        {
            _gameStateMachine = gameStateMachine;
            _windowService = windowService;
            _returnMainMenu = GetComponentInChildren<ReturnToMainMenu>();
            _restartLevel = GetComponentInChildren<RestartLevel>();


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


        //Need custom pause service
        private void OnDestroy()
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
        }
    }
}
