using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    [SerializeField] float staminaCost;
    [SerializeField] float cooldownDuration;

    bool onCooldown = false;
    public AbilityComponent OwningAbility { get; private set; }
    internal void Init(AbilityComponent abilityComponent)
    {
        OwningAbility = abilityComponent;
    }

    // return true if the ability is able to be casted
    // if returned true also commit it, meaning cost is consumed and cooldown will start
    public bool CommitAbility()
    {
        if (onCooldown)
        {
            return false;
        }

        if (!OwningAbility.TryConsumeStamina(staminaCost))
        {
            return false;
        }

        StartCooldown();
        return true;
    }

    void StartCooldown()
    {
        StartCorutine(CooldownCoroutine());
    }

    public Coroutine StartCorutine(IEnumerator enumerator)
    {
        return OwningAbility.StartCoroutine(enumerator);
    }

    IEnumerator CooldownCoroutine()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldownDuration);
        onCooldown = false;
    }

    public abstract void ActivateAbility();
}
