using System.Collections.Generic;
using System.Threading.Tasks;
using Assets.Scripts.UI.Elements;
using Enemy;
using Infrastructure.AssetManagement;
using Infrastructure.State;
using Logic;
using Logic.EnemySpawners;
using Logic.Gates;
using Logic.Scene;
using Player;
using Services.InputService;
using Services.PersistentProgressService;
using Services.Randomizer;
using Services.StaticDataService;
using StaticData;
using UI.Elements;
using UI.Menu;
using UI.Services.Windows;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();



        private readonly IAssetProvider _assetsProvider;
        private readonly IInputService _inputService;
        private readonly IStaticDataService _staticDataService;
        private readonly IRandomService _randomService;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IWindowService _windowService;
        private readonly IGameStateMachine _gameStateMachine;

        private GameObject _playerGameObject;
        private GameObject _gameManager;
        private GameObject _worldManager;
        private GameObject _gate;
        private GameObject _hud;
        

        public GameFactory(IAssetProvider assetsProvider,
            IInputService inputService,
            IStaticDataService staticDataService,
            IRandomService randomService,
            IPersistentProgressService persistentProgressService,
            IWindowService windowService,
            IGameStateMachine gameStateMachine)
        {
            _assetsProvider = assetsProvider;
            _inputService = inputService;
            _staticDataService = staticDataService;
            _randomService = randomService;
            _persistentProgressService = persistentProgressService;
            _windowService = windowService;
            _gameStateMachine = gameStateMachine;
        }

        public async Task WarmUp()
        {
            await _assetsProvider.Load<GameObject>(AssetsAddress.Loot);
            await _assetsProvider.Load<GameObject>(AssetsAddress.Spawner);
        }


        public async Task<GameObject> CreateGameManager()
        {
            GameObject prefab = await _assetsProvider.Load<GameObject>(AssetsAddress.GameManager);
            GameObject gameManager = Object.Instantiate(prefab, new Vector3(0,0,0), Quaternion.identity);   
            _gameManager = gameManager;
            return _gameManager;
        }
        
        public async Task CreateWorldManager(Vector3 playerInitialPoint)
        {
            GameObject prefab = await _assetsProvider.Load<GameObject>(AssetsAddress.WorldManager);
            GameObject worldManer = Object.Instantiate(prefab, playerInitialPoint, Quaternion.identity);   
            _worldManager = worldManer;
            //_worldManager.GetComponent<InfiniteWorld>().InitPlayer();
            
        }

     

        public async Task<GameObject> CreatePlayer(Vector3 playerInitialPoint)
        {
            PlayerStaticData playerStaticData = _staticDataService.ForPlayer(PlayerTypeId.Player1);

            _playerGameObject = await InstantiateRegisteredAsync(AssetsAddress.HeroPath, playerInitialPoint);

            PlayerHealth health = _playerGameObject.GetComponent<PlayerHealth>();
            health.Construct(playerStaticData);
            /*health.MaxHP = playerStaticData.MaxHP;
            health.CurrentHP = playerStaticData.CurrentHP;*/
            health.TextPrefab = await _assetsProvider.Load<GameObject>(AssetsAddress.HpText);
            PlayerDeath heroDeath = _playerGameObject.GetComponent<PlayerDeath>();
            heroDeath.Construct(_gameStateMachine, _windowService, _persistentProgressService);
            
            PlayerController playerController = _playerGameObject.GetComponent<PlayerController>();
            playerController.Construct(health);
            
            _gameManager.GetComponent<GameManager>().InitPlayer(_playerGameObject);
            _worldManager.GetComponent<InfiniteWorld>().InitPlayer(_playerGameObject.transform);

            return _playerGameObject;
        }

        public async Task<GameObject> CreateHud()
        {

            _hud = await InstantiateRegisteredAsync(AssetsAddress.HudPath);

           // _hud.GetComponentInChildren<LootCounter>().Construct(_persistentProgressService.Progress.WorldData);

            foreach(OpenWindowButton openWindowButton in _hud.GetComponentsInChildren<OpenWindowButton>())
            {
                openWindowButton.Construct(_windowService);
            }
            _gameManager.GetComponent<GameManager>().InitHud(_hud.GetComponent<ActorUI>());

            return _hud;
        }
        public async Task<GameObject> CreateMenu()
        {

            GameObject menu = await InstantiateRegisteredAsync(AssetsAddress.MenuPath);

           menu.GetComponentInChildren<MainMenu>().Construct(_gameStateMachine, _windowService);

            foreach(OpenWindowButton openWindowButton in menu.GetComponentsInChildren<OpenWindowButton>())
            {
                openWindowButton.Construct(_windowService);
            }

            return menu;
        }

        public async Task CreateLevelGate(Vector3 position, Quaternion rotation, GateTypeId gateTypeId)
        {
            GateStaticData gateStaticData = _staticDataService.ForGate(gateTypeId);
            if(gateStaticData != null)
            {
                GameObject prefab = await _assetsProvider.Load<GameObject>(gateStaticData.PrefabGateReference);
                GameObject gate = Object.Instantiate(prefab, position, rotation);

                IGatesStatus gateStatus = prefab.GetComponent<IGatesStatus>();
                _gate = gate;
            }

        }

        public async Task<LootPiece> CreateLoot()
        {
            GameObject prefab = await _assetsProvider.Load<GameObject>(AssetsAddress.Loot);
            LootPiece lootPiece = InstantiateRegistered(prefab).GetComponent<LootPiece>();
            return lootPiece;
        }
        
        public async Task<GameObject> CreateEnemy(EnemyTypeId enemyTypeId)
        {
            EnemyStaticData monsterStaticData = _staticDataService.ForEnemy(enemyTypeId);

            GameObject prefab = await _assetsProvider.Load<GameObject>(monsterStaticData.PrefabEnemyReference);

            GameObject enemy = Object.Instantiate(prefab);

            ChasePlayer chasePlayer = enemy.GetComponent<ChasePlayer>();
            chasePlayer.Construct(_playerGameObject);
            IHealth health = enemy.GetComponent<IHealth>();
            health.CurrentHP = monsterStaticData.Hp;
            health.MaxHP = monsterStaticData.Hp;
            health.TextPrefab = await _assetsProvider.Load<GameObject>(AssetsAddress.HpText);

            /*LootSpawner lootSpawner = enemy.GetComponentInChildren<LootSpawner>();
            lootSpawner.SetLoot(monsterStaticData.MinLoot, monsterStaticData.MaxLoot);
            lootSpawner.Construct(this, _randomService);*/


            return enemy;
        }
      

        public async Task  CreateSpawner(string spawnerId, Vector3 position, EnemyTypeId enemyTypeId, GameManager gameManager)
        {
            GameObject prefab = await _assetsProvider.Load<GameObject>(AssetsAddress.Spawner);

            GameObject spawner = InstantiateRegistered(prefab, position);
            SpawnPoint spawnPoint = spawner.GetComponent<SpawnPoint>();
            
            spawnPoint.Construct(this, gameManager);
            spawnPoint.Id = spawnerId;
            spawnPoint.EnemyTypeId = enemyTypeId;
            EnemyStaticData enemyStaticData = _staticDataService.ForEnemy(enemyTypeId);
            EnemyPool enemyPool = spawner.GetComponent<EnemyPool>();
            enemyPool.Construct(_playerGameObject,enemyStaticData);
            
            _gameManager.GetComponent<GameManager>().InitSpawner(spawner);
        }
        
        



        public void Register(ISavedProgressReader progressReader)
        {
            if(progressReader is ISavedProgress progressWriter)
            {
                ProgressWriters.Add(progressWriter);
            }

            ProgressReaders.Add(progressReader);
        }


        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }


        private GameObject InstantiateRegistered(GameObject prefab, Vector3 position)
        {
            GameObject gameObject = Object.Instantiate(prefab, position, Quaternion.identity);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private GameObject InstantiateRegistered(GameObject prefab)
        {
            GameObject gameObject = Object.Instantiate(prefab);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private async Task<GameObject> InstantiateRegisteredAsync(string prefabPath, Vector3 position)
        {
            GameObject gameObject = await _assetsProvider.Instantiate(prefabPath, spawnPosition: position);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        } 
        private async Task<GameObject> InstantiateRegisteredAsync(string prefabPath)
        {
            GameObject gameObject = await _assetsProvider.Instantiate(prefabPath);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(progressReader);
            }
        }


    }
}