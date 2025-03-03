using System.Collections.Generic;
using System.Threading.Tasks;
using CameraScripts;
using Data;
using Enemy;
using Infrastructure.Factory;
using Logic;
using Player;
using Scripts.UI.Elements;
using Services.PersistentProgressService;
using Services.StaticDataService;
using StaticData;
using UI.Elements;
using UI.Services.Factory;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniqueId = Logic.UniqueId;

namespace Infrastructure.State
{

    public class LoadLevelState : IPayLoadedState<string>
    {
        
        private const string MainMenu = "MainMenu";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IStaticDataService _staticDataService;
        private readonly IUIFactory _uiFactory;

        public LoadLevelState(GameStateMachine stateMachine, 
            SceneLoader sceneLoader, 
            LoadingCurtain curtain, 
            IGameFactory gameFactory, 
            IPersistentProgressService progressService,
            IStaticDataService staticDataService, 
            IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _persistentProgressService = progressService;
            _staticDataService = staticDataService;
            _uiFactory = uiFactory;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _gameFactory.Cleanup();
            _gameFactory.WarmUp();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _curtain.Hide();
        }
        private async void OnLoaded()
        {
            await InitUIRoot();
            await InitGameWorld();
            InformProgressReaders();

            _stateMachine.Enter<GameLoopState>();
        }
        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            {
                progressReader.LoadProgress(_persistentProgressService.Progress);
            }
        }

        private async Task InitUIRoot()
        {
           await _uiFactory.CreateUIRoot();
        }


        private async Task InitGameWorld()
        {
            LevelStaticData levelStaticData = LevelStaticData();
            if(levelStaticData.MenuLevel)
            {
                await InitGameManager();
                await InitHud(null, levelStaticData);
                
            }
            else
            {
                await InitGate(levelStaticData);
                await InitGameManager();
                await InitWorldManager(levelStaticData);
                //await InitLootPieces();
                GameObject hero = await InitPlayer(levelStaticData);
                await InitSpawners(levelStaticData);
                await InitHud(hero, levelStaticData);
                CameraFollow(hero);
            }

        }

        private async Task InitGameManager()
        {
            await _gameFactory.CreateGameManager();
        }

        private async Task InitWorldManager(LevelStaticData levelStaticData)
        {
            await _gameFactory.CreateWorldManager(levelStaticData.InitialHeroPosition);
        }

        private async Task InitGate(LevelStaticData levelStaticData)
        {
            if(levelStaticData.LevelGate != null)
            {
                await _gameFactory.CreateLevelGate(levelStaticData.LevelGate.Position, levelStaticData.LevelGate.Rotation, levelStaticData.LevelGate.GateTypeId);
            }
        }

        private async Task InitSpawners(LevelStaticData levelStaticData)
        {

            foreach(EnemySpawnerStaticData enemySpawnerData in levelStaticData.EnemySpawnerData)
            {
               await _gameFactory.CreateSpawner(enemySpawnerData.Id, enemySpawnerData.Position, enemySpawnerData.EnemyTypeId);
            }
        }

        private async Task InitLootPieces()
        {
            foreach(KeyValuePair <string, LootPieceData> item in _persistentProgressService.Progress.WorldData.LootData.LootPieceOnScene.Dictionary)
            {
                LootPiece lootPiece = await _gameFactory.CreateLoot();
                lootPiece.GetComponent<UniqueId>().Id = item.Key;
                lootPiece.Initialize(item.Value.Loot);
                lootPiece.transform.position = item.Value.Position.AsUnityVector();

            }
        }


        private async Task<GameObject> InitPlayer(LevelStaticData levelStaticData)
        {
            return await _gameFactory.CreatePlayer(levelStaticData.InitialHeroPosition);
        }

        private async Task InitHud(GameObject player, LevelStaticData levelStaticData)
        {
           
            if(levelStaticData.MenuLevel)
            {
                GameObject menu = await _gameFactory.CreateMenu();
            }
            else
            {
                
                GameObject hud = await _gameFactory.CreateHud();
                hud.GetComponentInChildren<ActorUI>()?.Construct(player.GetComponent<IHealth>());
                hud.GetComponentInChildren<AbilityUI>()?.Construct(player.GetComponent<AbilityHolder>());
            }
        }

        private LevelStaticData LevelStaticData()
        {
            return _staticDataService.ForLevel(SceneManager.GetActiveScene().name);
        }

        private void CameraFollow(GameObject hero)
        {
            Camera.main?.GetComponent<FollowPlayer>()!.FollowObject(hero);
        }



    }
}

