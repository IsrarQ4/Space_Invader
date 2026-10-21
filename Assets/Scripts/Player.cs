using System;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Spaceship _spaceship;
    [SerializeField] private PlayerWeaponsReference playerWeaponsReference;

    private void Start()
    {
        _spaceship = null;
    }

    public void SelectSpaceship(int shipnumber)
    {
        if (shipnumber == 1)
        {
            _spaceship = new Spaceship(10, 120, playerWeaponsReference.GetShip1Weapons());
            Debug.Log(_spaceship.GetWeapons()[0].GetWeaponName());
        }

        if (shipnumber == 2)
        {
            _spaceship = new Spaceship(20, 70, playerWeaponsReference.GetShip2Weapons());
        }

        if (shipnumber == 3)
        {
            _spaceship = new Spaceship(5, 150, playerWeaponsReference.GetShip3Weapons());
        }

        if (shipnumber == 4)
        {
            _spaceship = new Spaceship(15, 100, playerWeaponsReference.GetShip4Weapons());
        }
    }

    private void Update()
    {
        if (_spaceship == null)
        {
            return;
        }

        int speed = _spaceship.Get_movementSpeed();

        // Move left
        if (transform.position.x > -8.2f)
        {
            if (Keyboard.current.aKey.isPressed)
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
            }
        }

        // Move right
        if (transform.position.x < 8.2f)
        {
            if (Keyboard.current.dKey.isPressed)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
        }

        // Shoot
        if (Keyboard.current.spaceKey.isPressed)
        {   
            _spaceship.GetWeapons()[0].OpenFire(transform.position, 0f);
            
        }
        // not all spaceships will have secondary weapons
        if (Keyboard.current.wKey.isPressed)
        {
        }
        if (Keyboard.current.sKey.isPressed)
        {   
            // foreach Weapon rocket in _spaceship.GetWeapons()[]
            // {if (rocket.GetWeaponName() == rocket || rocket.GetTimeToNextShot() <= 0 {rocket.OpenFire; break;}
            // else {continue; or whatever the word was to go to the next item in the list}
            //
            // Explanation for the above comments: each ship will have multiple rockets with a long reload time.
            // The idea is that player can press one button to fire any rocket that is done reloading
        }
        foreach (WeaponManager.Weapon weapon in this._spaceship.GetWeapons())
        {
            weapon.TickDownTimeToNextShot();
        }
    }

    public float GetPlayerHealth()
    {
        if (_spaceship == null)
        {
            return 0;
        }

        return _spaceship.GetPlayerHealth();
    }

    public class Spaceship
    {
        private int _movementSpeed;
        private float PlayerHealth;
        private List<WeaponManager.Weapon> _weapons = new List<WeaponManager.Weapon>();

        public Spaceship(int movespeed, int playerhealth, List<WeaponManager.Weapon> Weapons)
        {
            _movementSpeed = movespeed;
            PlayerHealth = playerhealth;
            this._weapons = Weapons;
        }

        public int Get_movementSpeed()
        {
            return _movementSpeed;
        }

        public float GetPlayerHealth()
        {
            return PlayerHealth;
        }

        public float GetMaxHealth()
        {
            return PlayerHealth;
        }
        public List<WeaponManager.Weapon> GetWeapons()
        {
            return this._weapons;
        }
    }
}
