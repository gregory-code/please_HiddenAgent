using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Fire")]
public class Fire : Ability
{
    [SerializeField] TargetScanner scannerPrefab;
    TargetScanner scanner;

    [SerializeField] float range = 3;
    [SerializeField] float scanDuration = 0.3f;
    [SerializeField] float damage = 40;
    [SerializeField] float damageDuration = 3;

    [SerializeField] GameObject scanVFX;
    [SerializeField] GameObject damageVFX;

    public override void ActivateAbility()
    {
        if (!CommitAbility())
            return;

        scanner = Instantiate(scannerPrefab);
        scanner.Init(range, scanDuration, scanVFX == null ? null : Instantiate(scanVFX));
        scanner.SetupAttachment(OwningAbility.gameObject.transform);
        scanner.StartScan();
        scanner.onNewTargetFound += ApplyDamage;
    }

    private void ApplyDamage(GameObject target)
    {
        HealthComponet targetHealth = target.GetComponent<HealthComponet>();
        if (targetHealth == null)
            return;

        if (targetHealth.GetComponent<ITeamInterface>().GetRelationTowards(OwningAbility.gameObject) != TeamRelation.Hostile)
            return;

        DurationDamage damager = targetHealth.gameObject.AddComponent<DurationDamage>();
        //Do the coroutine//

        damager.Init(damageDuration, damage, targetHealth, OwningAbility.gameObject, damageVFX);
    }
}
