using System.Collections;
using Logic;
using Services.PauseService;
using UI.Services.Windows;
using UnityEngine;

namespace Enemy
{
    public class WaveManager : MonoBehaviour
    {
        public EnemyPool enemyPool;
        public AnimationCurve spawnRateCurve; 
        public CameraBoundsVisualizer CameraBoundsVisualizer;
        public int initialEnemiesPerWave = 2; 
        public float timeBetweenWaves = 10f; 
        public float waveDifficultyMultiplier = 1.2f;
        public float spawnAreaPadding = 1f;

        private int _currentWave = 0;
        private int _enemiesRemainingInWave;
        private bool _isSpawningWave = false;
        private bool _isPaused = false;
        private IPauseService _pauseService;


        public void Construct(IPauseService pauseService)
        {
            _pauseService = pauseService;
        }
        
        public void StartAfterInitPlayer()
        {
            Invoke(nameof(StartingWavesAfterDelay), 3f); 
        }

        private void StartingWavesAfterDelay()
        {
            StartCoroutine(StartWaves());
        }

        IEnumerator StartWaves()
        {
            while (true) 
            {
            
                yield return new WaitWhile(() => _isPaused || _pauseService.IsPaused);
            
                _currentWave++;
                Debug.Log($"Wave {_currentWave} started!");

            
                int enemiesInWave = Mathf.RoundToInt(initialEnemiesPerWave * Mathf.Pow(waveDifficultyMultiplier, _currentWave - 1));
                _enemiesRemainingInWave = enemiesInWave;

            
                StartCoroutine(SpawnWave(enemiesInWave));

           
                float timer = 0f;
                while (timer < timeBetweenWaves)
                {
                    if (!_isPaused && !_pauseService.IsPaused)
                    {
                        timer += Time.deltaTime;
                    }
                    yield return null;
                }
            }
        }

        IEnumerator SpawnWave(int enemiesToSpawn)
        {
            _isSpawningWave = true;

            while (enemiesToSpawn > 0)
            {
                yield return new WaitWhile(() => _isPaused || _pauseService.IsPaused);
                
                if (enemyPool.GetActiveEnemyCount() < enemyPool.poolSize)
                {
                    SpawnEnemy();
                    enemiesToSpawn--;
                }

            
                float spawnDelay = spawnRateCurve.Evaluate(Time.time / 60f); 
                float timer = 0f;
                
                while (timer < spawnDelay)
                {
                    if (!_isPaused && !_pauseService.IsPaused)
                    {
                        timer += Time.deltaTime;
                    }
                    yield return null;
                }
            }

            _isSpawningWave = false;
        }
        void SpawnEnemy()
        {
            GameObject enemy = enemyPool.GetEnemy();
            if (enemy != null)
            {
                enemy.transform.position = CameraBoundsVisualizer.GetRandomPositionOutsideCamera();

                // Настройка сложности врага (например, увеличение здоровья или скорости)
           
            }
        }
    
        public void PauseWaves()
        {
            _isPaused = true;
        }
        public void ResumeAfterPause()
        {
            _isPaused = false;
        }
    
        public void ResumeAfterRefresh()
        {
            //isPaused = false;
            Invoke("ResumeAfterPause", 3f);
        }

        public void RefreshWaves()
        {
            _isPaused = true;
            HideAllEnemies();
            //StopCoroutine(StartWaves());
        
        
        }
        private void HideAllEnemies()
        {
            foreach (var enemy in enemyPool.enemyPool)
            {
                enemy.SetActive(false);
            }
        }
    
    }
}