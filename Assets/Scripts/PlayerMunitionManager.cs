using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMunitionManager : MonoBehaviour
{
    [SerializeField] private GameObject[] munitionReference;        // list of different munition types
    private GameObject spawnedMunition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {                                                   // TEMP VALUE - index should be Player's selected weapon
            spawnedMunition = Instantiate(munitionReference[Random.Range(0,munitionReference.Length)]);     // spawns test munition
            spawnedMunition.transform.position = Vector3.zero;
        }
    }
}
