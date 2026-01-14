using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;

public class WaveManager : Singleton<WaveManager>
{
    public event Action OnInvasionEnd;

    public Waypoints waypoints;

    [SerializeField] private List<Wave> waves = new List<Wave>();
    [SerializeField] private int enemyNum;

    [Space(0.5f)]
    [Header("References")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform enemyHolder;

    [Header("Timing")]
    [SerializeField] private float timeBetweenEachSpawn = 0.75f;
    [SerializeField] private float countdown = 2f;

    private int waveIndex = 0;

    void Start()
    {
        waveIndex = 0;
        enemyNum = 0;
    }

    void Update()
    {
        if (waveIndex >= waves.Count)
        {
            if (enemyNum <= 0 && !LevelManager.Ins.IsWinLevel)
            {
                LevelManager.Ins.IsWinLevel = true;
                OnInvasionEnd?.Invoke();
            }

            return;
        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = waves[waveIndex].delayTime;
        }

        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        Enemy[] enemies = waves[waveIndex].enemies;

        for (int i = 0; i < enemies.Length; i++) 
        {
            SpawnEnemy(enemies[i]);
            yield return new WaitForSeconds(timeBetweenEachSpawn);
        }

        waveIndex++;
    }

    private void SpawnEnemy(Enemy enemy)
    {
        Enemy spawnedEnemy = Instantiate(enemy, spawnPoint.position, spawnPoint.rotation, enemyHolder);
        enemyNum++;
    }

    public void ReduceEnemyNum()
    {
        enemyNum--;
    }
}
