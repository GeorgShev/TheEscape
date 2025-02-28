using Data;
using Services.PersistentProgressService;
using Services.SaveLoad;
using Services.StaticDataService;
using StaticData;

namespace Infrastructure.State
{
    public class LoadProgressState : IState
    {
        public string InitialLevel = "MainMenu";
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IStaticDataService _staticDataService;

        public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadService, IStaticDataService staticDataService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _staticDataService = staticDataService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();


            _gameStateMachine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.PositionOnLevel.Level);
        }


        public void Exit()
        {
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();
            
        }

        private PlayerProgress NewProgress()
        {
            var progress = new PlayerProgress(initialLevel : InitialLevel);

            PlayerStaticData playerStaticData = _staticDataService.ForPlayer(0);
            
            progress.PlayerState.MaxHP = playerStaticData.MaxHP;
            progress.PlayerState.CurrentHP = playerStaticData.CurrentHP;



            return progress;
        }
    }
}
