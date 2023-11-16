using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Health Regen")]
public class HealthRegen : Ability
{
    [SerializeField] float health;
    [SerializeField] float duration;

    HealthComponet healthComponent;

    public override void ActivateAbility()
    {
        if (!CommitAbility()) return;

        healthComponent = OwningAbility.GetComponent<HealthComponet>();
        if (healthComponent == null || healthComponent.IsFull())
            return;

        StartCorutine(HealOverTime());
    }

    IEnumerator HealOverTime()
    {
        float timeLeft = duration;
        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            float deltaTime = Time.deltaTime;
            if(timeLeft < 0)
            {
                deltaTime += timeLeft;
            }
            float healthToRecover = (health * deltaTime) / duration;
            healthComponent.ChangeHealth(healthToRecover, OwningAbility.gameObject);
            yield return new WaitForEndOfFrame();
        }
    }
}
