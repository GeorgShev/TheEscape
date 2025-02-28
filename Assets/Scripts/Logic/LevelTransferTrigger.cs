using Data;
using Infrastructure.State;
using Services;
using Services.PersistentProgressService;
using Services.SaveLoad;
using UnityEngine;

namespace Logic
{
    public class LevelTransferTrigger : MonoBehaviour, ISavedProgress
    {
        public string TransferTo;

        private const string PlayerTag = "Player";
        private const string ShieldTag = "PlayerShield";
        private IGameStateMachine _gameStateMachine;
        private bool _isTransfering = false;
        private ISaveLoadService _saveLoadService;
        private ISavedProgress _isavedProgress; 
               

        public void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        private void Awake()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }

        private void OnTriggerEnter(Collider other)
        {
            
            if (_isTransfering)
            {
                return;
            }

            if (other.CompareTag(PlayerTag) || (other.CompareTag(ShieldTag)))
            {
                //Debug.Log("Transfer");

                _saveLoadService.SaveProgress();

                
                TransferToNextLevel();
            }

        }

        public void TransferToNextLevel()
        {
            _gameStateMachine.Enter<LoadLevelState, string>(TransferTo);
            _isTransfering = true;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.PositionOnLevel = new PositionOnLevel(TransferTo);
        }

        public void LoadProgress(PlayerProgress progress)
        {
        }
    }
}
