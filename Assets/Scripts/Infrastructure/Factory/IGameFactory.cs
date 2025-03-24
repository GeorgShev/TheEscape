using System.Collections.Generic;
using System.Threading.Tasks;
using Enemy;
using Logic;
using Services;
using Services.PersistentProgressService;
using StaticData;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }


        Task<GameObject> CreatePlayer(Vector3 playerInitialPoint);
        Task<GameObject> CreateAudioPlayer(bool isMenu);
        Task<GameObject> CreateHud();
        Task<GameObject> CreateMenu();
        Task<LootPiece> CreateLoot();
        Task CreateSpawner(string spawnerId,Vector3 position, EnemyTypeId enemyTypeId, GameManager gameManager);
        void Cleanup();
        Task WarmUp();
        Task CreateLevelGate(Vector3 position, Quaternion rotation, GateTypeId gateTypeId);
        Task<GameObject> CreateGameManager();
        Task CreateWorldManager(Vector3 playerInitialPoint);
        Task<GameObject> CreateEnemy(EnemyTypeId enemyTypeId);
    }
}