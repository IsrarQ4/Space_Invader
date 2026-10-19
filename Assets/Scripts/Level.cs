using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private EnemyWave wave1;
    private EnemyWave wave2;
    private EnemyWave wave3;
    private bool _isDone = false;
    private float _timer;
    private List<EnemyWave> _enemyWaves = new List<EnemyWave>();
    public Level(GameObject waveOneEnemyOne, GameObject waveOneEnemyTwo, GameObject waveTwoEnemyOne, GameObject waveTwoEnemyTwo,
        GameObject waveThreeEnemyOne, GameObject waveThreeEnemyTwo,
        float spawnIntervalMultiplier, float spawnIntervalBase,
        float height, float width, float spawn_interval, int enemyMax, float enemyOneWeight)
    {
        wave1 = new EnemyWave(waveOneEnemyOne, waveOneEnemyTwo, height, width, spawn_interval, enemyMax, enemyOneWeight);
        wave2 = new EnemyWave(waveTwoEnemyOne, waveTwoEnemyTwo, height, width, spawn_interval, enemyMax, enemyOneWeight);
        wave3 = new EnemyWave(waveThreeEnemyOne, waveThreeEnemyTwo, height, width, spawn_interval, enemyMax, enemyOneWeight);
    }

    // Update is called once per frame
 public void Update()
    {
        _timer += Time.deltaTime;
        if (wave1.GetEnemiesSpawned() < wave1.GetEnemyMax())
        {
            if (_timer >= wave1.GetSpawnInterval())
            {
                wave1.SpawnEnemy();
                _timer = 0;
            }
            else if (wave2.GetEnemiesSpawned() < wave2.GetEnemyMax())
            {
                if (_timer >= wave2.GetSpawnInterval())
                {
                    wave2.SpawnEnemy();
                    _timer = 0;
                }

            }
            else if (wave3.GetEnemiesSpawned() < wave3.GetEnemyMax())
            {
                if (_timer >= wave3.GetSpawnInterval())
                {
                    wave3.SpawnEnemy();
                    _timer = 0;
                }

            }
            else
            {
                _isDone = true;
            }
        }
    }
    public bool IsDone()
    {
       return _isDone;
    }
}
