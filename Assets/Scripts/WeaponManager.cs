using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{
    
    // test list, player and enemies should have their own list
    // public List<Weapon> TestWeaponsReference = new List<Weapon>();

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
        public float GetTimeToNextShot()
        {
            return this._timeToNextShot;
        }
        public void TickDownTimeToNextShot()
        {
            if (this._timeToNextShot > 0)
            {
                this._timeToNextShot -= Time.deltaTime;
            }
        }
        private void SpawnMunition(Vector2 spawnCoords, Quaternion rotation, GameObject munitionToSpawn)
        {
            Instantiate(munitionToSpawn,position:spawnCoords, rotation:rotation);
        }
        public void OpenFire(Vector2 pos, float angle)
        // rotation of 0 is straight up
        {
            if (this._timeToNextShot <= 0)
            {
                SpawnMunition(pos, Quaternion.Euler(0f, 0f, angle), this._munitionPrefab);
                // Debug.Log("weapon is shooting (this message is supposed to be spammed)");
                this._timeToNextShot = 60f/this._fireRateRPM;     // convert rpm to interval in seconds
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // float shootTimer = 0.2f;
    // Update is called once per frame
    void Update()
    {
        // Debug.Log("time thingy");
        // if (Keyboard.current.spaceKey.isPressed)    // Move this to player!!
        // {   
        //     GetWeaponFromName("rocket").OpenFire(Vector2.zero, 0);
        // }
        // foreach (Weapon weapon in TestWeaponsReference) // this too
        // {
        //     weapon.TickDownTimeToNextShot();
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
