using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerWeaponsReference : MonoBehaviour
{
    [SerializeField] private List<WeaponManager.Weapon> ship1Weapons;
    [SerializeField] private List<WeaponManager.Weapon> ship2Weapons;
    [SerializeField] private List<WeaponManager.Weapon> ship3Weapons;
    [SerializeField] private List<WeaponManager.Weapon> ship4Weapons;
    public List<WeaponManager.Weapon> GetShip1Weapons()
    {
        return ship1Weapons;
    }
    public List<WeaponManager.Weapon> GetShip2Weapons()
    {
        return ship2Weapons;
    }
    public List<WeaponManager.Weapon> GetShip3Weapons()
    {
        return ship3Weapons;
    }
    public List<WeaponManager.Weapon> GetShip4Weapons()
    {
        return ship4Weapons;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
