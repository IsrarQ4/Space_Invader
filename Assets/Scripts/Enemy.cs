using UnityEngine;

// has All the numbers for one enemy type, use inspector to edit
[System.Serializable]
public class EnemyStats
{
    public float MaxHealth = 10f;
    public float Damage = 5f;      
    public float FireRate = 1f;    
    public float MoveSpeed = 2f;

    //default constructor
    public EnemyStats() 
    {
    }

    //constructor
    public EnemyStats(float maxHealth, float damage, float fireRate, float moveSpeed)
    {
        MaxHealth = maxHealth;
        Damage = damage;
        FireRate = fireRate;
        MoveSpeed = moveSpeed;
    }

}

public class Enemy : MonoBehaviour
{
    [Header("stats (you can change here in inspector or with SetStats)")]
    [SerializeField] private EnemyStats _stats = new EnemyStats();

    [Header("Movement")]
    [SerializeField] private float _xLimit = 8f;       
    [SerializeField] private float _minTravel = 2f;     

    [Header("Shooting")]
    [SerializeField] private GameObject _bulletPrefab; 

    private float _currentHealth;
    private float _targetX;
    private float _shootTimer;

    void Start()
    {
        _currentHealth = _stats.MaxHealth;
        PickNewTargetX();
        // the first shot is random bc if they will all shoot at once and that would be weird
        _shootTimer = Random.Range(0f, GetShootDelay());
    }

    void Update()
    {
        Move();
        Shoot();
    }

    // use this after instantiate to give different stats basically the setter
    public void SetStats(EnemyStats newStats)
    {
        _stats = newStats;
        _currentHealth = _stats.MaxHealth;
    }

    //getters
    public float GetCurrentHealth()
    {
        return _currentHealth;
    }
    public EnemyStats GetStats()
    {
        return _stats;
    }

    //the fire rate if its 0 it means it doesnt shoot btw
    private float GetShootDelay()
    {
        if (_stats.FireRate <= 0f)
        {
            return float.MaxValue;  
        }
        return 1f / _stats.FireRate;
    }

    // moves towards target x once it reaches it goes to the new target x
    private void Move()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.MoveTowards(pos.x, _targetX, _stats.MoveSpeed * Time.deltaTime);
        transform.position = pos;

        if (Mathf.Approximately(pos.x, _targetX))
        {
            PickNewTargetX();
        }
    }

    //picks the new target x and makes sure its not too close so it doesnt look goofy
    private void PickNewTargetX()
    {
        float newX = transform.position.x;
        for (int i = 0; i < 10; i++)
        {
            newX = Random.Range(-_xLimit, _xLimit);
            if (Mathf.Abs(newX - transform.position.x) >= _minTravel)
            {
                break;
            }
        }
        _targetX = newX;
    }

    //shoots idk what else to say i needed the setter here btw keanu
    private void Shoot()
    {
        _shootTimer -= Time.deltaTime;
        if (_shootTimer <= 0f)
        {
            GameObject bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.Euler(0f, 0f, 180f));
            Munition munition = bullet.GetComponent<Munition>();
            if (munition != null)
            {
                munition.SetDamage(_stats.Damage);
            }
            _shootTimer = GetShootDelay();
        }
    }

    //use this for whne the player hits the enemy
    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0f)
        {
            Die();
        }
    }

    //enemy dies
    private void Die()
    {
        Destroy(gameObject);
    }
}