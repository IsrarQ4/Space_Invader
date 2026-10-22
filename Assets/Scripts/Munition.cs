using System;
using UnityEngine;

public class Munition : MonoBehaviour
{
    private Rigidbody2D _rb;
    [System.NonSerialized] public bool IsEnemyMunition;
    [SerializeField] private float _speed;
    [SerializeField] private float _baseDamage;
    [SerializeField] private RocketMotor _rocketMotor = new RocketMotor();
    [System.Serializable]
    private class RocketMotor
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
            this._speed += this._rocketMotor.GetThrust() / (1 / Time.smoothDeltaTime);
            this._rb.linearVelocity = transform.up * this._speed;
            _rocketMotor.TickDownBurnTime();
            Debug.Log(this._speed);
            // rocket graphic effects
        }
        else
        {
            _rocketMotor.isBurning = false;
            // Debug.Log("Stopped burning (this should only log one message)");
        }

    }

    private bool _damageEnabled;

    public float GetBaseDamage()
    {
        return this._baseDamage;
    }

    void Start() // i guess this is the Munition constructor now
    {
        this._damageEnabled = true;
        this._rb = GetComponent<Rigidbody2D>();

        // there is no max range anymore. munitions fly until they leave the screen (see Update)
        if (this._rocketMotor.GetRocketEnabled() == true)
        {
            this._rocketMotor.isBurning = true;
        }
        else if (this._speed == 0)
        {
            Debug.LogWarning("Munition has zero speed and no rocket motor, it will never move!");
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

        // Deleting munitions that move out of camera
        float cameraScale = 5f;     // this should be equal to the value inside Size in the camera component in the Main Camera object 
        float destructionOffset = 1f;

        // pls dont freak out over this unconventional formatting i dont know a cleaner way to check position ranges :cry face emoji:
        // disable munitions that move out of the camera frame - prevents sniping enemies that haven't entered yet
        if (transform.position.y >= cameraScale
            || transform.position.y <= -(cameraScale)
            || transform.position.x >= 16f / 9f * cameraScale
            || transform.position.x <= -(16f / 9f * cameraScale))
        { this._damageEnabled = false; }      // prevent killing enemies outside of the frame (disabled functionality yet to be implemented)

        // delete munitions after they move a little further out of the camera frame
        // prevents munitions from dissapearing while partially visible on screen (more visually appealing)
        if (transform.position.y >= cameraScale + destructionOffset
            || transform.position.y <= -(cameraScale + destructionOffset)
            || transform.position.x >= 16f / 9f * cameraScale + destructionOffset
            || transform.position.x <= -(16f / 9f * cameraScale + destructionOffset))
        { Destroy(gameObject); }
    }

    // Enemy and Player call this when the munition hits something it can damage
    public void DestroyOnHit()
    {
        Destroy(gameObject);
    }

    //setter and getter for the dmg bc i need for enemys
    // No you dont, just make a new munition prefab and attach it inside the
    // muntionprefab field of the serialized Weapon object of the enemy

    // public void SetDamage(float damage)
    // {
    //     _baseDamage = damage;
    // }

    public float GetDamage()
    {
        return _baseDamage;
    }
    // hi kids remember to document the shit out of your code because people dont want spend hours to translating your sphagetti
    // also text retains info better than brain
}