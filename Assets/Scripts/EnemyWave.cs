using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    private GameObject _enemyPrefabOne;
    private GameObject _enemyPrefabTwo;
    private float _height;
    private float _width;
    private float _spawnInterval;
    private int _enemyMax;
    private float _enemyOneWeight;
    private int _enemiesSpawned = 0;
    
    public int GetEnemiesSpawned()
    {
        return _enemiesSpawned;
    }
    public int GetEnemyMax()
    {
        return _enemyMax;
    }   
    public float GetSpawnInterval()
    {
        return _spawnInterval;
    }
    public EnemyWave(GameObject enemyPrefabOne, GameObject enemyPrefabTwo, float height, float width, float spawnInterval, int enemyMax, float enemyOneWeight)
    {
        _enemyPrefabOne = enemyPrefabOne;
        _enemyPrefabTwo = enemyPrefabTwo;
        _height = height;
        _width = width;
        if (height < 0)
                    {
            _height = 0f;
        }
        else
        {
            _height = height;
        }
        if (width < 0)
        {
            _width = 0f;
        }
        else
        {
            _width = width;
        }
        if (_spawnInterval < 0)
        {
            _spawnInterval = 0f;
        }
        else
        {
            _spawnInterval = spawnInterval;
        }
        _enemyMax = enemyMax;
        if (enemyOneWeight < 0)
        {
            enemyOneWeight = 0;
        }
        else if (enemyOneWeight > 1)
        {
            enemyOneWeight = 1;
        }
        else
        {
            _enemyOneWeight = enemyOneWeight;
        }
            
    }   

    // Update is called once per frame
    public void SpawnEnemy()
    {
        
       

        float randomX = Random.Range(-_width, _width);
        float randomY = 4.5f - Random.Range(0f, _height);
        Vector2 spawnPosition = new Vector2(randomX, randomY);
        int randomEnemy = Random.Range(0, 100);
        if (randomEnemy <= _enemyOneWeight * 100)
            {
                Instantiate(_enemyPrefabOne, spawnPosition, Quaternion.identity);
            }
                else
            {
                Instantiate(_enemyPrefabTwo, spawnPosition, Quaternion.identity);
            }
                
         _enemiesSpawned++;
            
        
    }
}
