using Infrastructure.State;
using UI.Windows.Menu;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements
{
    public class ChangeLevelButton : MonoBehaviour
    {
        public Button Button;
        public string TransferTo;
        private IGameStateMachine _gameStateMachine;


        public void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        private void Awake()
        {
            Button.onClick.AddListener(ChangeLevel);
        }

        public void ChangeLevel()
        {

            _gameStateMachine.Enter<LoadLevelState, string>(TransferTo);
        }
    }
}