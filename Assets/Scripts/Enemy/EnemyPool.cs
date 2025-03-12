using System.Collections.Generic;
using Logic;
using StaticData;
using UnityEngine;

namespace Enemy
{
    public class EnemyPool : MonoBehaviour
    {
        public GameObject enemyPrefab;
        public int poolSize = 10;

        public List<GameObject> enemyPool;

        private GameObject _player;
        private EnemyStaticData _enemyStaticData;
        private bool _firstCheckList = false;

        public void Construct(GameObject player, EnemyStaticData enemyStaticData)
        {
            
            //_player = player;
            _enemyStaticData = enemyStaticData;
            //FillPool();
        }


        public void FillList(GameObject enemy)
        {
            if (_firstCheckList)
            {
                if (enemyPool == null)
                {
                    enemyPool = new List<GameObject>();
                }
                else
                {
                    enemyPool.Clear();
                }

                _firstCheckList = true;
            }
            enemy.SetActive(false);
            enemyPool.Add(enemy);

        }

      
        
        public int GetActiveEnemyCount()
        {
            int activeCount = 0;
            foreach (GameObject enemy in enemyPool)
            {
                if (enemy.activeInHierarchy)
                {
                    activeCount++;
                }
            }
            return activeCount;
        }

        public GameObject GetEnemy()
        {
            foreach (GameObject enemy in enemyPool)
            {
                if (!enemy.activeInHierarchy)
                {
                    IHealth enemyHealth = enemy.GetComponent<IHealth>();
                    enemyHealth.CurrentHP = _enemyStaticData.Hp;
                    enemy.SetActive(true);
                    enemy.GetComponent<DissolveEnemy>().ShowObject();
                    return enemy;
                }
            }

            // Если все врачи активны, создаем новый объект
            GameObject newEnemy = Instantiate(enemyPrefab);
            enemyPool.Add(newEnemy);
            return newEnemy;
        }
    }
}