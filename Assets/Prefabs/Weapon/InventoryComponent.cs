using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryComponent : MonoBehaviour
{
    [SerializeField] Weapon[] initialWeaponPrefabs;

    private List<Weapon> weapons = new List<Weapon>();

    int currentWeaponIndex = -1; // negative value means something do not exit.

    private void Awake()
    {
        InitializeWeapons();
    }

    private void InitializeWeapons()
    {
        foreach(Weapon weaponPrefab in initialWeaponPrefabs)
        {
            Weapon newWeapon = Instantiate<Weapon>(weaponPrefab);
            weapons.Add(newWeapon);
            newWeapon.UnEnquip();
        }

        NextWeapon();
    }

    private void NextWeapon()
    {
        int nextIndex = currentWeaponIndex + 1;
        if( nextIndex >= weapons.Count )
        {
            nextIndex = 0;
        }

        EquipWeapon(nextIndex);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
