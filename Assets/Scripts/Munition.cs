using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class Munition : MonoBehaviour
{
    private Rigidbody2D _rb;
    [System.NonSerialized] public bool IsEnemyMunition;
    [SerializeField] private bool CanKillMunitions;
    [SerializeField] private float _speed;
    [SerializeField] private float _baseDamage;
    private float _lifeTime;
    [Header("should be zero for non-pennetrating munitions")]
    [SerializeField] private float _pennetrationLifeTime;
    [Header("set to zero to disable explosion")]
    [SerializeField] private float _explosionRadius;
    [SerializeField] private Sprite _explosionSprite;
    private bool _hasHitOnce;
    private bool _hasExploded;
    [SerializeField] private float _maxRange;
    [SerializeField] private ParticleSystem _burnParticlePrefab;
    private ParticleSystem _burnParticles;
    [SerializeField] private RocketMotor _rocketMotor = new RocketMotor();
    [System.Serializable] private class RocketMotor
    {   
        [SerializeField] private bool _rocketEnabled;
        [NonSerialized] public bool isBurning;
        [SerializeField] private float _burnTime;
        [Tooltip("The value at which _speed is increased each frame. Should be a pretty small number in the low tenths or hundredths.")]
        [SerializeField] private float _thrust;
        public bool GetRocketEnabled()
        {
            return this._rocketEnabled;
        }
        public float GetBurnTime()
        {
            return this._burnTime;
        }
        public void TickDownBurnTime()
        {
            this._burnTime -= Time.deltaTime;
            // Debug.Log("burn time: " + this._burnTime);
        }
        public float GetThrust()
        {
            return this._thrust;
        }

        public RocketMotor()
        {
            if (this._rocketEnabled == true && !(this._burnTime > 0 && this._thrust > 0))
            {
                this._rocketEnabled = false;
                Debug.LogError("RocketMotor disabled after attempted initialization with invalid properties");
            }
            // StartBurn not called in here in case i want a delay to ignition
        }
    }

    private void Burn()
    {
        if (_rocketMotor.GetBurnTime() > 0f && _rocketMotor.GetRocketEnabled() == true)
        {
            this._speed += this._rocketMotor.GetThrust() / (1/Time.smoothDeltaTime);
            this._rb.linearVelocity = transform.up * this._speed;
            _rocketMotor.TickDownBurnTime();
            if (_burnParticles == null && _burnParticlePrefab != null)
            {
                _burnParticles = Instantiate(_burnParticlePrefab, transform);
                _burnParticles.transform.localPosition = Vector3.zero;
            }

            if (_burnParticles != null && !_burnParticles.isPlaying)
            {
                _burnParticles.Play();
            }
        }
        else
        {
            _rocketMotor.isBurning = false;
            if (_burnParticles != null && _burnParticles.isEmitting)
            {
                _burnParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
            // Debug.Log("Stopped burning (this should only log one message)");
        }

    }

    private bool _damageEnabled;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float GetMaxRange()
    {
        return this._maxRange;
    }
    public float GetRemainingLifeTime()
    {
        return this._lifeTime;
    }
    public float GetBaseDamage()
    {
        return this._baseDamage;
    }
    public void DestroyOnHit()
    {
        if (this._hasHitOnce == false)
        {
            Explode();
            this._hasHitOnce = true;
            this._lifeTime = this._pennetrationLifeTime;
        }

    }

    public void Explode()
    {
        if (this._hasExploded || this._explosionRadius <= 0f)
        {
            return;
        }

        this._hasExploded = true;
        HashSet<Enemy> damagedEnemies = new HashSet<Enemy>();
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, this._explosionRadius);
        foreach (Collider2D hit in hits)
        {
            Enemy enemy = hit.GetComponentInParent<Enemy>();
            if (enemy != null && damagedEnemies.Add(enemy))
            {
                if (this.IsEnemyMunition == false)
                    {
                        enemy.TakeDamage(this._baseDamage);
                    }
            }
        }

        if (this._explosionSprite != null)
        {
            GameObject explosion = new GameObject("Munition Explosion");
            explosion.transform.position = transform.position;
            SpriteRenderer explosionRenderer = explosion.AddComponent<SpriteRenderer>();
            explosionRenderer.sprite = this._explosionSprite;

            SpriteRenderer munitionRenderer = GetComponent<SpriteRenderer>();
            if (munitionRenderer != null)
            {
                explosionRenderer.sortingLayerID = munitionRenderer.sortingLayerID;
                explosionRenderer.sortingOrder = munitionRenderer.sortingOrder + 1;
            }

            Vector2 spriteSize = this._explosionSprite.bounds.size;
            float maxSpriteDimension = Mathf.Max(spriteSize.x, spriteSize.y);
            if (maxSpriteDimension > 0f)
            {
                float diameter = this._explosionRadius * 2f;
                explosion.transform.localScale = Vector3.one * (diameter / maxSpriteDimension);
            }
            Destroy(explosion, 0.2f);
        }
    }

    void Start() // i guess this is the Munition constructor now
    {   
        // TO-DO: validate properties _maxRange and RocketMotor stuff
        this._damageEnabled = true;
        this._hasHitOnce = false;
        this._rb = GetComponent<Rigidbody2D>();


        if (this._rocketMotor.GetRocketEnabled() == true)
        {
            this._lifeTime = 999f;  // use a different method to calculate lifetime from range
            this._rocketMotor.isBurning = true;
        }
        else
        {
            this._lifeTime = calculateTimeToMaxRange();
        }
        this._rb.linearVelocity = transform.up * _speed;
    }

    // Update is called once per frame
    void Update()
    {
        // i should move all ts to a separate method sometime
        if (this._rocketMotor.isBurning == true)
        {
            Burn();
        }

        this._lifeTime -= Time.deltaTime;
        // Deleting munitions that move out of camera
        float cameraScale = 5f;     // this should be equal to the value inside Size in the camera component in the Main Camera object 
        float destructionOffset = 1f;

        // pls dont freak out over this unconventional formatting i dont know a cleaner way to check position ranges :cry face emoji:
        // disable munitions that move out of the camera frame - prevents sniping enemies that haven't entered yet
        if (transform.position.y >= cameraScale                
            || transform.position.y <= -(cameraScale)
            || transform.position.x >= 16f/9f * cameraScale
            || transform.position.x <= -(16f/9f * cameraScale))
        {this._damageEnabled = false;}      // prevent killing enemies outside of the frame (disabled functionality yet to be implemented)
        
        // delete munitions after they move a little further out of the camera frame
        // prevents munitions from dissapearing while partially visible on screen (more visually appealing)
        if (_lifeTime <= 0
            || transform.position.y >= cameraScale + destructionOffset
            || transform.position.y <= -(cameraScale + destructionOffset)
            || transform.position.x >= 16f/9f * cameraScale + destructionOffset
            || transform.position.x <= -(16f/9f * cameraScale + destructionOffset))
        {Destroy(gameObject);}
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!CanKillMunitions)
        {
            return;
        }

        Munition otherMunition = other.GetComponentInParent<Munition>();
        if (otherMunition != null && IsEnemyMunition != otherMunition.IsEnemyMunition)
        {
            Destroy(otherMunition.gameObject);
            DestroyOnHit();
        }
    }

    private float calculateTimeToMaxRange()
    {
        if (this._speed == 0)
        {
            Debug.Log("idk why unity doesn't throw divide by zero error :p");
            Debug.LogWarning("Munition object with zero speed destroyed. Self-propelled munitions should have _lifeTime manually set manually or with a different method.");
            Destroy(gameObject);
        }
        float timeToMaxRange = this._maxRange / this._speed;  // d/v = t basic grade 11 kinematics haha
        return timeToMaxRange;
    }

    // idk why i made this overloaded method maybe i'll move all my utility math methods into their own script
    private float calculateTimeToMaxRange(float targetRange, float speed)
    {
        if (speed == 0)
        {
            Debug.Log("idk why unity doesn't throw divide by zero error :p");
            Debug.LogWarning("Munition object with zero speed destroyed. Self-propelled munitions should have _lifeTime manually set manually or with a different method.");
            Destroy(gameObject);
        }
        float timeToMaxRange = targetRange / speed;
        return timeToMaxRange;
    }
}


