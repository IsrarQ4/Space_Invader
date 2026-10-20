using System.Collections.Generic;
using UnityEngine;

public class Level
{
    private bool _isDone = false;
    private float _timer;
    private List<EnemyWave> _enemyWaves;
    private int index = 0;

    public Level(GameObject waveOneEnemyOne, GameObject waveOneEnemyTwo, GameObject waveTwoEnemyOne, GameObject waveTwoEnemyTwo,
        GameObject waveThreeEnemyOne, GameObject waveThreeEnemyTwo,
        
        float height, float width, float spawn_interval, int enemyMax, float enemyOneWeight)
    {
        _enemyWaves = new List<EnemyWave> {
            new EnemyWave(waveOneEnemyOne, waveOneEnemyTwo, height, width, spawn_interval, enemyMax, enemyOneWeight),
            new EnemyWave(waveTwoEnemyOne, waveTwoEnemyTwo, height, width, spawn_interval, enemyMax, enemyOneWeight),
            new EnemyWave(waveThreeEnemyOne, waveThreeEnemyTwo, height, width, spawn_interval, enemyMax, enemyOneWeight),
        };
    }

    // Update is called once per frame
 public void Update()
    {
        _timer += Time.deltaTime;
        if (_enemyWaves[index].GetEnemiesSpawned() < _enemyWaves[index].GetEnemyMax())
        {
            
            if (_timer >= _enemyWaves[index].GetSpawnInterval())
            {
                _enemyWaves[index].SpawnEnemy();
                _timer = 0;
            }
        }
        else
        {
            index += 1;
        }

        if (index >= _enemyWaves.Count)
        {
            _isDone = true;
        }
    }
    public bool IsDone()
    {
       return _isDone;
    }
}
