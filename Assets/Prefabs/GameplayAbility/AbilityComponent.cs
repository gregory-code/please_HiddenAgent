using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AbilityComponent : MonoBehaviour
{
    [SerializeField] Ability[] initialAbilities;
    List<Ability> abilities = new List<Ability>();

    public event Action<Ability> onNewAbilityAdded;

    private void Start()
    {
        GiveAbility();
    }

    private void GiveAbility()
    {
        foreach (Ability ability in initialAbilities)
        {
            Ability newAbility = Instantiate(ability);
            newAbility.Init(this);
            abilities.Add(newAbility);
            onNewAbilityAdded?.Invoke(newAbility);
        }
    }
}
