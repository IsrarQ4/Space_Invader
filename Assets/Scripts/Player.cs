using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Spaceship ChosenSpaceship { get; private set; }
    private bool _shipSelected = false;
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

        public float GetMaxHealth()
        {
            return _playerHealth;
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
            ChosenSpaceship = new Spaceship(5, 150, GetShip3Weapons());
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
    if (ChosenSpaceship == null)
        {
            return;
        }

    int speed = ChosenSpaceship.GetMovementSpeed();
        // Move left
if (transform.position.x > -8.2f){
    if (Keyboard.current.aKey.isPressed)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }   

        // Move right
if (transform.position.x < 8.2f){
    if (Keyboard.current.dKey.isPressed)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
   }

   public float GetPlayerHealth()
{
    if (ChosenSpaceship == null)
        return 0;

    return ChosenSpaceship.GetPlayerHealth();
}
}





