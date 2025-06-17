using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public List<Wave> waves = new List<Wave>();

    [Space(0.5f)]
    [Header("References")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform enemyHolder;


    public float timeBetweenWaves = 5f;
    public float timeBetweenEachSpawn = 0.5f;
    private float countdown = 2f;

    private int waveIndex = 0;

    void Update()
    {
        if (waveIndex >= waves.Count)
        {
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
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation, enemyHolder);
    }
}
