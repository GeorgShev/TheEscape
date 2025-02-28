using System;
using System.Collections;
using Enemy;
using Logic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    public EnemyPool enemyPool;
    public AnimationCurve spawnRateCurve; // Кривая пейсинга для спавна
    public CameraBoundsVisualizer CameraBoundsVisualizer;
    public int initialEnemiesPerWave = 2; // Количество врагов в первой волне
    public float timeBetweenWaves = 10f; // Время между волнами
    public float waveDifficultyMultiplier = 1.2f;
    public float spawnAreaPadding = 1f;// Множитель сложности волны

    private int currentWave = 0;
    private int enemiesRemainingInWave;
    private bool isSpawningWave = false;

    public void Construct()
    {
        StartCoroutine(StartWaves());
    }

    IEnumerator StartWaves()
    {
        while (true) // Бесконечный цикл волн
        {
            currentWave++;
            Debug.Log($"Wave {currentWave} started!");

            // Увеличиваем сложность волны
            int enemiesInWave = Mathf.RoundToInt(initialEnemiesPerWave * Mathf.Pow(waveDifficultyMultiplier, currentWave - 1));
            enemiesRemainingInWave = enemiesInWave;

            // Настраиваем спавнер для текущей волны
            StartCoroutine(SpawnWave(enemiesInWave));

            // Ждем фиксированное время перед началом следующей волны
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    IEnumerator SpawnWave(int enemiesToSpawn)
    {
        isSpawningWave = true;

        while (enemiesToSpawn > 0)
        {
            // Проверяем, есть ли свободные враги в пуле
            if (enemyPool.GetActiveEnemyCount() < enemyPool.poolSize)
            {
                SpawnEnemy();
                enemiesToSpawn--;
            }

            // Интервал спавна на основе кривой пейсинга
            float spawnDelay = spawnRateCurve.Evaluate(Time.time / 60f); 
            yield return new WaitForSeconds(spawnDelay);
        }

        isSpawningWave = false;
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
    
}