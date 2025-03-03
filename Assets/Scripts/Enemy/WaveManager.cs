using System;
using System.Collections;
using Enemy;
using Logic;
using UnityEngine;
using Random = UnityEngine.Random;

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

    public void StartAfterInitPlayer()
    {
       Invoke("StartingWavesAfterDelay", 3f); 
    }

    private void StartingWavesAfterDelay()
    {
        StartCoroutine(StartWaves());
    }

    IEnumerator StartWaves()
    {
        while (true) 
        {
            
            if (_isPaused) 
            {
                yield return null;
                continue;
            }
            
            _currentWave++;
            Debug.Log($"Wave {_currentWave} started!");

            
            int enemiesInWave = Mathf.RoundToInt(initialEnemiesPerWave * Mathf.Pow(waveDifficultyMultiplier, _currentWave - 1));
            _enemiesRemainingInWave = enemiesInWave;

            
            StartCoroutine(SpawnWave(enemiesInWave));

           
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    IEnumerator SpawnWave(int enemiesToSpawn)
    {
        _isSpawningWave = true;

        while (enemiesToSpawn > 0)
        {
            
            if (enemyPool.GetActiveEnemyCount() < enemyPool.poolSize)
            {
                SpawnEnemy();
                enemiesToSpawn--;
            }

            
            float spawnDelay = spawnRateCurve.Evaluate(Time.time / 60f); 
            yield return new WaitForSeconds(spawnDelay);
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