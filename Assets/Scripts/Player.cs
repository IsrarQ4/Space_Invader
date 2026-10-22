using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Spaceship ChosenSpaceship { get; private set; }
    private bool _shipSelected = false;
    private bool _isDead = false;
    [SerializeField] private float _deathScreenDelay = 1.5f;
    [SerializeField] private GameObject[] _shipVisuals;
    [Header("Israr please just use prefabs 😭😭 i hate having to write bum code on top of your bum code")]
    [Header("no offense meant, but if you keep coding like this you will continue to cause your teammates headaches")]
    [Tooltip("mr gallo pls spare me")]
    [SerializeField] private List<WeaponManager.Weapon> _ship1Weapons = new List<WeaponManager.Weapon>();
    [SerializeField] private List<WeaponManager.Weapon> _ship2Weapons = new List<WeaponManager.Weapon>();
    [SerializeField] private List<WeaponManager.Weapon> _ship3Weapons = new List<WeaponManager.Weapon>();
    [SerializeField] private List<WeaponManager.Weapon> _ship4Weapons = new List<WeaponManager.Weapon>();

    public List<WeaponManager.Weapon> GetShip1Weapons()
    {
        return this._ship1Weapons;
    }
    public List<WeaponManager.Weapon> GetShip2Weapons()
    {
        return this._ship2Weapons;
    }
    public List<WeaponManager.Weapon> GetShip3Weapons()
    {
        return this._ship3Weapons;
    }
    public List<WeaponManager.Weapon> GetShip4Weapons()
    {
        return this._ship4Weapons;
    }
    public class Spaceship
    {
        private int _movementSpeed;
        private float _playerHealth;
        private List<WeaponManager.Weapon> _weapons;

        public Spaceship(int movementSpeed, int playerHealth, List<WeaponManager.Weapon> weapons)
        {
            _movementSpeed = movementSpeed;
            _playerHealth = playerHealth;
            _weapons = weapons;
        }

        public int GetMovementSpeed()
        {
            return _movementSpeed;
        }

        public float GetPlayerHealth()
        {
            return _playerHealth;
        }
        public void TakeDamage(float damage)
        {
            this._playerHealth -= damage;
            if (this._playerHealth <= 0)
            {
                this._playerHealth = 0;
                Debug.Log("player has died, do the death screen");
                // death screen
            }
        }

        public float GetMaxHealth()
        {
            return _playerHealth;
        }
        public List<WeaponManager.Weapon> GetWeapons()
        {
            return this._weapons;
        }
        public int GetWeaponIndexFromName(string name)
        {
            int indexToReturn = -1;
            for (int i=0; i < this._weapons.Count; i++)
            {
                if (this._weapons[i].GetWeaponName() == name)
                {
                    indexToReturn = i;
                }
            }
            if (indexToReturn == -1)
            {
                Debug.LogError("GetWeaponIndexFromName(): Invalid weapon name!");
            }
            return indexToReturn;
        }
    }

    public bool IsShipSelected()
    {
        return _shipSelected;
    }

    void Start()
    {
        ChosenSpaceship = null;
        for (int i = 0; i < _shipVisuals.Length; i++)
        {
            _shipVisuals[i].SetActive(false);
        }
    }

    public void SelectSpaceship(int shipnumber)
    {
        if (shipnumber == 1)
        {
            ChosenSpaceship = new Spaceship(10, 120, GetShip1Weapons());
        }
        if (shipnumber == 2)
        {
            ChosenSpaceship = new Spaceship(20, 70, GetShip2Weapons());
        }
        if (shipnumber == 3)
        {
            ChosenSpaceship = new Spaceship(5, 180, GetShip3Weapons());
        }
        if (shipnumber == 4)
        {
            ChosenSpaceship = new Spaceship(15, 100, GetShip4Weapons());
        }
        _shipSelected = true;
        for (int i = 0; i < _shipVisuals.Length; i++)
        {
            _shipVisuals[i].SetActive(i == shipnumber - 1);
        }
    }

    void Update()
    {
        if (ChosenSpaceship == null || _isDead)
        {
            return;
        }

        int speed = ChosenSpaceship.GetMovementSpeed();

        // Move left
        if (transform.position.x > -8.2f && Keyboard.current.aKey.isPressed)
        {
           transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        // Move right
        if (transform.position.x < 8.2f && Keyboard.current.dKey.isPressed)
        {
           transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        // primary weapon open fire
        if (Keyboard.current.spaceKey.isPressed)
        {
            ChosenSpaceship.GetWeapons()[ChosenSpaceship.GetWeaponIndexFromName("primary")].OpenFire(transform.position, 0, false);
        }
        // secondary weapon open fire
        if (Keyboard.current.wKey.isPressed)
        {
            ChosenSpaceship.GetWeapons()[ChosenSpaceship.GetWeaponIndexFromName("secondary")].OpenFire(transform.position, 0, false);
            
        }
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {

            foreach (WeaponManager.Weapon weapon in ChosenSpaceship.GetWeapons())
            {
                if (weapon.GetWeaponName() == "rocket" && weapon.GetTimeToNextShot() <= 0)
                {
                    weapon.OpenFire(transform.position, 0, false);
                    break;
                }
                else
                {
                    continue;
                }
            }
        }

        foreach (WeaponManager.Weapon weapon in ChosenSpaceship.GetWeapons())
        {
            weapon.TickDownTimeToNextShot();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isDead)
        {
            return;
        }
        Munition munition = other.GetComponent<Munition>();
        if (munition == null)
        {
            return;
        }
        float damage = munition.GetBaseDamage();
        Debug.Log("player was hit");
        // only enemy bullets hurt the player
        if (munition.IsEnemyMunition == true)
        {
            this.ChosenSpaceship.TakeDamage(damage);
            AudioManager.Instance.PlayHit();
            munition.DestroyOnHit();    // the bullet is used up when it hits
            Debug.Log("player was hit by enemy");

            if (this.ChosenSpaceship.GetPlayerHealth() <= 0)
            {
                Die();
            }
        }
    }
    private void Die()
    {
        _isDead = true;
        AudioManager.Instance.PlayGameOver();
        Invoke(nameof(LoadDeathScreen), _deathScreenDelay);
    }
    public void DestroyOnHit()
    {
        Destroy(gameObject);
    }

    private void LoadDeathScreen()
    {
        SceneManager.LoadScene("Death");
    }

    public float GetPlayerHealth()
    {
        if (ChosenSpaceship == null)
        {
            return 0;
        }

        return ChosenSpaceship.GetPlayerHealth();
    }
}





