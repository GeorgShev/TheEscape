using System.Collections.Generic;
using Logic.Gates;
using UnityEngine;

namespace Logic.EnemySpawners
{
    public class EnemyCount : MonoBehaviour
    {
        public List<SpawnPoint> SpawnPoints = new List<SpawnPoint>();

        public GameObject _gate;

        public void Construct(GameObject gate)
        {
            _gate = gate;
        }

        public void UpdateEnemyList(SpawnPoint spawnPoint)
        {
            SpawnPoints.Remove(spawnPoint);

            KillingEnemy();
        }

        private void KillingEnemy()
        {
            if (SpawnPoints.Count <= 0)
            {
                _gate.GetComponent<IGatesStatus>().UpdateStatus();
            }
        }
    }
}