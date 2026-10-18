using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{
    // list of different weapon types
    // each weapon can be added to the weapons list below for spawn reference
    public string PlayerSelectedWeaponName;
    public List<Weapon> WeaponsReference = new List<Weapon>();
    private Weapon testWeapon; 
    private Weapon testWeapon1;

    [System.Serializable]
    public class Weapon
    {
        // pairs a name with a gameobject (intended to use with muntion prefabs) for easy access in scripts
        [SerializeField] private string _name;
        [SerializeField] private GameObject _munitionPrefab;
        [SerializeField] private int _fireRateRPM;
        private float _timeToNextShot;
        
        public string GetWeaponName()
        {
            return this._name;
        }
        public GameObject GetMunitionPrefab()
        {
            return this._munitionPrefab;
        }
        public int GetFireRateRPM()
        {
            return this._fireRateRPM;
        }
        public void TickDownTimeToNextShot()
        {
            if (this._timeToNextShot > 0)
            {
                this._timeToNextShot -= Time.fixedDeltaTime;
                Debug.Log(this._timeToNextShot);
            }
        }
        private void SpawnMunition(Vector2 spawnCoords, Quaternion rotation, GameObject munitionToSpawn)
        {
            GameObject spawnedMunition = Instantiate(munitionToSpawn,position:spawnCoords, rotation:rotation);
        }
        public void OpenFire()
        {
            if (this._timeToNextShot <= 0)
            {
                SpawnMunition(Vector2.zero, Quaternion.Euler(0f, 0f, 0f), this._munitionPrefab); // insert player pos and turret rotation data here
                this._timeToNextShot = 60f / this._fireRateRPM;     // convert rpm to seconds
            }
            // fire rate stuff should be handled here
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        testWeapon = GetWeaponFromName("test");
        // testWeapon1 = WeaponsReference[1];
    }

    // float shootTimer = 0.2f;
    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.isPressed)
        {   
            testWeapon.OpenFire();
        }
        testWeapon.TickDownTimeToNextShot();
    }
        
    public Weapon GetWeaponFromName(string name)
    {
        Weapon weapon = null;
        foreach (Weapon w in WeaponsReference)
        {
            if (w.GetWeaponName() == name)
            {
                weapon = w;
            }
        }
        if (weapon == null)
        {
            Debug.LogError("GetWeaponFromName(): Weapon name not found!");
        }
        return weapon;
    }

    public Quaternion GetDirectionFromCoords(Vector2 startCoords, Vector2 targetCoords)
    {
        Vector2 direction = targetCoords - startCoords;
        if (direction.sqrMagnitude > 0f)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            return rotation;
        }
        else
        {
            return Quaternion.Euler(0f, 0f, 0f);
        }
    }
}
