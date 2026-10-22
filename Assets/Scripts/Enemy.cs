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
    [SerializeField] private float _minTravel = 2f;
    [SerializeField] private int _scoreValue = 1;

    // [Header("Shooting")]
    // [SerializeField] private GameObject _bulletPrefab; 

    [Header("shooty pew pew")]
    [SerializeField] private WeaponManager.Weapon _weapon;

    private float _currentHealth;
    private float _targetX;
    // private float _shootTimer;
    // private string ENEMY_TAG = "Enemy";
    // private string PLAYER_MUNITION_TAG = "PlayerMunition";

    void Start()
    {
        _currentHealth = _stats.MaxHealth;
        PickNewTargetX();
        // the first shot is random bc if they will all shoot at once and that would be weird
        // _shootTimer = Random.Range(0f, GetShootDelay());
    }

    void Update()
    {
        Move();
        if (true)   //add more shooting conditions to make shots less monotonouos, eg if x pos matches player x pos, or maybe bursts of suppresive fire to cut off movement range. 
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