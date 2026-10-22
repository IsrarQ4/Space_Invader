using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

// has All the numbers for one enemy type, use inspector to edit
[System.Serializable]
public class EnemyStats
{
    public float MaxHealth = 10f;
    public float Damage = 5f;      
    public float MoveSpeed = 2f;

    //default constructor
    public EnemyStats() 
    {
    }

    //constructor
    public EnemyStats(float maxHealth, float damage, float fireRate, float moveSpeed)
    {
        MaxHealth = maxHealth;
        // Damage = damage;
        MoveSpeed = moveSpeed;
    }

}

public class Enemy : MonoBehaviour
{
    [Header("stats (you can change here in inspector or with SetStats)")]
    [SerializeField] private EnemyStats _stats = new EnemyStats();

    [Header("Movement")]
    [SerializeField] private float _xLimit = 8f;       
    [SerializeField] private float _yLimit = 4f;
    [SerializeField] private float _minTravel = 2f;
    [SerializeField] private int _scoreValue = 1;

    // [Header("Shooting")]
    // [SerializeField] private GameObject _bulletPrefab; 

    [Header("shooty pew pew")]
    [SerializeField] private WeaponManager.Weapon _weapon;
    [SerializeField] private float _shootXDistance = 2f;

    private Player _player;
    private float _currentHealth;
    private float _targetX;
    private float _targetY;
    // private float _shootTimer;
    // private string ENEMY_TAG = "Enemy";
    // private string PLAYER_MUNITION_TAG = "PlayerMunition";

    void Start()
    {
        _player = FindFirstObjectByType<Player>();
        _currentHealth = _stats.MaxHealth;
        PickNewTargetX();
        // PickNewTargetY();
        _targetY = 5f;
        // the first shot is random bc if they will all shoot at once and that would be weird
        // _shootTimer = Random.Range(0f, GetShootDelay());
    }

    void Update()
    {
        Move();
        MoveY();
        float distanceDownFromTop = 5f - transform.position.y;
        bool inThirdQuarterFromTop = distanceDownFromTop >= _yLimit * 0.5f
            && distanceDownFromTop <= _yLimit * 0.75f;
        bool withinPlayerXDistance = _player != null
            && Mathf.Abs(transform.position.x - _player.transform.position.x) <= _shootXDistance;

        if (withinPlayerXDistance || inThirdQuarterFromTop)
        {
            _weapon.OpenFire(transform.position, 180f, true);
        }
        _weapon.TickDownTimeToNextShot(); // ticks down weapon reload timer regardless of firing or not
        
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Munition munition = other.GetComponent<Munition>();
        if(munition == null)
        {
            return;
        }
        float damage = munition.GetBaseDamage();

        if (munition.IsEnemyMunition == false)
        {
            TakeDamage(damage);
            munition.DestroyOnHit();    
        }
    }
    // {
    //     
    //     
    //     
    //     
    // }

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

    // moves towards target x once it reaches it goes to the new target x
    private void Move()
    {
        Vector2 pos = transform.position;       // no need to use vector3 for 2d position
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
    // moves towards target y once it reaches it goes to the new target y
    private void MoveY()
    {
        Vector2 pos = transform.position;       // no need to use vector3 for 2d position
        pos.y = Mathf.MoveTowards(pos.y, _targetY, _stats.MoveSpeed * Time.deltaTime);
        // pos.y = Mathf.Clamp(pos.y, 5f - _yLimit, 5f);
        transform.position = pos;

        if (Mathf.Approximately(pos.y, _targetY))
        {
            PickNewTargetY();
        }
    }

    //picks the new target y and makes sure its not too close so it doesnt look goofy
    private void PickNewTargetY()
    {
        float newY = transform.position.y;
        newY = Random.Range(5f -_yLimit, 5f);
        if (Mathf.Abs(newY - transform.position.y) >= _minTravel)
        {
            newY = transform.position.y;
        }

        _targetY = newY;
    }

    //use this for whne the player hits the enemy
    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0f)
        {
            Die();
        }
        else
        {
            AudioManager.Instance.PlayHit();
        }
    }

    //enemy dies
    private void Die()
    {
        Destroy(gameObject);
        ScoreManager.Instance.AddScore(_scoreValue);
        AudioManager.Instance.PlayEnemyDeath();
    }
}