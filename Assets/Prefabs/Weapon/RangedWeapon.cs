using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    private AimTargetingComponent aimTargetingComponent;
    private void Awake()
    {
        aimTargetingComponent = GetComponent<AimTargetingComponent>();
    }
    public override void Attack()
    {
        GameObject target = aimTargetingComponent.GetTarget();
        if (target != null)
        {
            Debug.Log($"attacking: {target.name}");
        }
    }
}
