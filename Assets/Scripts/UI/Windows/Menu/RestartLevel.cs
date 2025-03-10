using Infrastructure.State;
using Services;
using UI.Services.Windows;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Windows.Menu
{
    public class RestartLevel : WindowBase
    {
        public Button Button;


        public string ChoiseButton1 = "Cancel";
        public string ChoiseButton2 = "Confirm";
        public string ChoiseHeadding = "Do you want to restart current level?";
        public string ChoiseBody = " ";

        private string _mainMenu = "MainMenu";

        private IWindowService _windowService;

        private IGameStateMachine _gameStateMachine;


        public void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        private void Awake()
        {
            _windowService = AllServices.Container.Single<IWindowService>();

            Button.onClick.AddListener(ClickStart);
        }

      
        public void ClickStart()
        {
            CreateChoiseWindow();
        }

        public void CreateChoiseWindow()
        {
            _windowService.Open(WindowId.ChoiceWindow);
            _windowService.ChoiceWindow.Construct(ChoiseButton1, ChoiseButton2, ChoiseHeadding, ChoiseBody);
            _windowService.ChoiceWindow.Choice1 += Cancel;
            _windowService.ChoiceWindow.Choice2 += Confirm;
            _windowService.ChoiceWindow.DestroyWindow += DestroyWindow;
        }


        public void Cancel()
        {
            DestroyWindow();
        }

        public void Confirm()
        {
            
            SceneManager.LoadScene("Level1");
            
            _gameStateMachine.Enter<LoadLevelState, string>("Level1");
        }

        public void DestroyWindow()
        {
            _windowService.ChoiceWindow.Choice1 -= Cancel;
            _windowService.ChoiceWindow.Choice2 -= Confirm;
            _windowService.ChoiceWindow.DestroyWindow -= DestroyWindow;
        }
    }
}