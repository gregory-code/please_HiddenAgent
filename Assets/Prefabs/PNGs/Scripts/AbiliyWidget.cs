using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbiliyWidget : MonoBehaviour
{

    Ability ability;
    [SerializeField] Image icon;
    [SerializeField] Image cooldownImage;

    internal void Init(Ability ability)
    {
        this.ability = ability;
        icon.sprite = ability.GeIcon();
        ability.onCooldownStarted += cooldownStarted;
    }

    private void cooldownStarted(float cooldownDuration)
    {
        cooldownImage.fillAmount = 1;
        StartCoroutine(cooldownWait(cooldownDuration));
    }

    IEnumerator cooldownWait(float duration)
    {
        float cooldownTimeLeft = duration;
        while(cooldownTimeLeft > 0)
        {
            cooldownTimeLeft -= Time.deltaTime;
            cooldownImage.fillAmount = cooldownTimeLeft / duration;
            yield return new WaitForEndOfFrame();
        }

    }

    public void ActivateAbility()
    {
        ability.ActivateAbility();
    }
}
