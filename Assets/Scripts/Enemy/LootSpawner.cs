using Data;
using Infrastructure.Factory;
using Services.Randomizer;
using UnityEngine;

namespace Enemy
{
    public class LootSpawner : MonoBehaviour
    {
        public EnemyDeath EnemyDeath;
        public bool HaveLoot = true;
        private IGameFactory _gameFactory;
        private IRandomService _randomService;
        private int _lootMin;
        private int _lootMax;

        public void Construct(IGameFactory gameFactory, IRandomService randomService)
        {
            _gameFactory = gameFactory;
            _randomService = randomService;
        }

        private void Start()
        {
            EnemyDeath.Happened += SpawnLoot;
        }

        public void SetLoot(int min, int max)
        {
            _lootMin = min;
            _lootMax = max;
        }

        private async void SpawnLoot()
        {
            if (HaveLoot)
            {
                EnemyDeath.Happened -= SpawnLoot;

                LootPiece lootPiece = await _gameFactory.CreateLoot();
                lootPiece.transform.position = transform.position;

                Loot lootItem = GenerateLoot();

                lootPiece.Initialize(lootItem);
            }
        }

        private Loot GenerateLoot()
        {
            return new Loot
            {
                Value = _randomService.Next(_lootMin, _lootMax)
            };
        }

       
    }
}