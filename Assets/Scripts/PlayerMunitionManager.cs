using System.Collections.Generic;
using NUnit.Framework.Constraints;
using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.InputSystem;

public class PlayerMunitionManager : MonoBehaviour
{
    // list of different weapon types
    // each weapon can be added to the weapons list below for spawn reference
    [System.Serializable]
    public class Weapon
    {
        // pairs a name with a gameobject (intended to use with muntion prefabs) for easy access in scripts
        [SerializeField] private string _name;
        [SerializeField] private GameObject _munitionPrefab;
        public int FireRateRPM;
        
        
        public GameObject GetMunitionPrefab()
        {
            return this._munitionPrefab;
        }
        public string GetWeaponName()
        {
            return this._name;
        }
    }

    [Header("fire rate is WIP, currently does nothing")]
    public List<Weapon> Weapons = new List<Weapon>();
    private GameObject _spawnedMunition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    float shootTimer = 0f;
    // Update is called once per frame
    void Update()
    {
        // Test shoot snippet - player and enemy call SpawnMunition
        if (shootTimer >= 0f)
        {
            shootTimer -= Time.deltaTime;
        }
        if (shootTimer >= 0f && Keyboard.current.spaceKey.isPressed)
        {   
            // TEMP VALUES - should be player coords
            UnityEngine.Vector2 coords = new UnityEngine.Vector2(8,-2);
            UnityEngine.Vector2 targetCoords = new UnityEngine.Vector2(-8, -3);
            SpawnMunition(coords, GetDirectionFromCoords(coords, targetCoords), "test");
            shootTimer = 0.2f;
            shootTimer = 0.2f;
        }
        Debug.Log(shootTimer);
        Debug.Log(shootTimer);
    }
    public void SpawnMunition(Vector2 spawnCoords, Quaternion rotation, string weaponName)
    {
        // get the munition gameobject from the Weapons list based on the name
        // GameObject munitionToFire = Weapons[Random.Range(0, Weapons.Count)].munitionPrefab; // Test fire random different types
        GameObject munitionToSpawn = null;
        foreach (Weapon weapon in Weapons)
        {
            if (weapon.GetWeaponName() == weaponName)
            {
                munitionToSpawn = weapon.GetMunitionPrefab();
                break;
            }
        }
        if (munitionToSpawn != null)
        {
                _spawnedMunition = Instantiate(munitionToSpawn,position:spawnCoords, rotation:rotation);
        }
        else
        {
            Debug.LogError("Munition not spawned: Weapon name not found!");
        }
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
