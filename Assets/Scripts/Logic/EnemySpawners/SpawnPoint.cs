using Enemy;
using Infrastructure.Factory;
using StaticData;
using UnityEngine;

namespace Logic.EnemySpawners
{
    public class SpawnPoint : MonoBehaviour
    {
        public EnemyTypeId EnemyTypeId;
        public string Id { get; set; }
        public EnemyPool EnemyPool;
        public WaveManager WaveManager;
        
        private IGameFactory _gameFactory;
        private EnemyDeath _enemyDeath;
        private EnemyStaticData _enemyStaticData;


        public void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            Spawn();
        }

        private void OnDestroy()
        {
            if(_enemyDeath != null)
            {
                _enemyDeath.Happened -= Slay;
            }
        }
        private async void Spawn()
        {
            
            for (int i = 0; i < EnemyPool.poolSize; i++)
            {
                GameObject enemy = await _gameFactory.CreateEnemy(EnemyTypeId);
                EnemyPool.FillList(enemy);
            }
            
            
           //GameObject enemy = await _gameFactory.CreateEnemy(EnemyTypeId, transform);
            //_enemyDeath = enemy.GetComponent<EnemyDeath>();
            //_enemyDeath.Happened += Slay;
        }

        private void Slay()
        {
            if(_enemyDeath != null)
            {
                _enemyDeath.Happened -= Slay;
            }
        }
    }

}