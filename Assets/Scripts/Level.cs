using UnityEngine;

public class Level : MonoBehaviour
{
    private EnemyWave wave1;
    private EnemyWave wave2;
    private EnemyWave wave3;
    private bool _isDone = false;    
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
    void Update()
    {
        if (wave1.GetEnemiesSpawned() < wave1.GetEnemyMax())
        {
            wave1.SpawnEnemy();
        }
        else if (wave2.GetEnemiesSpawned() < wave2.GetEnemyMax())
        {
            wave2.SpawnEnemy();
        }
        else if (wave3.GetEnemiesSpawned() < wave3.GetEnemyMax())
        {
            wave3.SpawnEnemy();
        }
        else
        {
            _isDone = true;
        }
    }
    public bool IsDone()
    {
       return _isDone;
    }
}
