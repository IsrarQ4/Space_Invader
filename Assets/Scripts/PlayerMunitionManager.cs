using System.Collections.Generic;
using NUnit.Framework.Constraints;
using NUnit.Framework.Internal;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMunitionManager : MonoBehaviour
{
    // list of different weapon types
    // each weapon can be added to the weapons list below for spawn reference
    [System.Serializable]
    public class Weapon
    {
        // pairs a name with a gameobject (intended to use with muntion prefabs) for easy access in scripts
        public string name;
        public GameObject munitionPrefab;
        public int fireRateHz;
    }

    [Header("fire rate is WIP, currently does nothing")]
    public List<Weapon> weapons = new List<Weapon>();


    private GameObject _spawnedMunition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Test shoot snippet - player and enemy call SpawnMunition
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {   // TEMP VALUES - should be player coords
            UnityEngine.Vector2 coords = new UnityEngine.Vector2(8,-2);
            UnityEngine.Vector2 targetCoords = new UnityEngine.Vector2(-8, -3);
            SpawnMunition(coords, GetDirectionFromCoords(coords, targetCoords), "test");
        }
    }
    public void SpawnMunition(Vector2 spawnCoords, Quaternion rotation, string weaponName)
    {
        // get the munition gameobject from the Weapons list based on the name
        // GameObject munitionToFire = Weapons[Random.Range(0, Weapons.Count)].munitionPrefab; // Test fire random different types
        GameObject munitionToSpawn = null;
        foreach (Weapon weapon in weapons)
        {
            if (weapon.name == weaponName)
            {
                munitionToSpawn = weapon.munitionPrefab;
            }
            else
            {
                Debug.LogError("PlayerMunitionManager.cs/SpawnMunition(): Weapon name not found!",munitionToSpawn);
            }
        }
        _spawnedMunition = Instantiate(munitionToSpawn,position:spawnCoords, rotation:rotation);    // spawns test munition
    }
    private UnityEngine.Quaternion GetDirectionFromCoords(UnityEngine.Vector2 startCoords, UnityEngine.Vector2 targetCoords)
    {
        UnityEngine.Vector2 direction = (UnityEngine.Vector2)targetCoords - (UnityEngine.Vector2)startCoords;
        if (direction.sqrMagnitude > 0f)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            UnityEngine.Quaternion rotation = UnityEngine.Quaternion.Euler(0f, 0f, angle);
            return rotation;
        }
        else
        {
            return Quaternion.Euler(0f, 0f, 0f);
        }
    }
}
