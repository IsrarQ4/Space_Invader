using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefabOne;
    [SerializeField] private GameObject _enemyPrefabTwo;
    [SerializeField] private float _height;
    [SerializeField] private float _width;
    [SerializeField] private float _spawn_interval;
    [SerializeField] private int _enemyMax;
    [SerializeField] private float _enemyOneWeight;
    private int _enemiesSpawned = 0;
    private float _timer;
    public int GetEnemiesSpawned()
    {
        return _enemiesSpawned;
    }
    public int GetEnemyMax()
    {
        return _enemyMax;
    }   
    public EnemyWave(GameObject enemyPrefabOne, GameObject enemyPrefabTwo, float height, float width, float spawn_interval, int enemyMax, float enemyOneWeight)
    {
        _enemyPrefabOne = enemyPrefabOne;
        _enemyPrefabTwo = enemyPrefabTwo;
        _height = height;
        _width = width;
        if (spawn_interval < 0)
        {
            spawn_interval = 0;
        }
        else
        {
            _spawn_interval = spawn_interval;
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        SpawnEnemy();
        
    }

    // Update is called once per frame
    public void SpawnEnemy()
    {
        _timer += Time.deltaTime;
        if (_enemiesSpawned < _enemyMax)
        {
            
            if (_timer >= _spawn_interval)
            {
                float randomX = Random.Range(-_width, _width);
                float randomY = Random.Range(0, _height);
                Vector2 spawnPosition = new Vector2(randomX, randomY);
                int randomEnemy = Random.Range(0, 100);
                if (randomEnemy < _enemyOneWeight * 100)
                {
                    Instantiate(_enemyPrefabOne, spawnPosition, Quaternion.identity);
                }
                else
                {
                    Instantiate(_enemyPrefabTwo, spawnPosition, Quaternion.identity);
                }
                _timer = 0;
                _enemiesSpawned++;
            }
        }
    }
}
