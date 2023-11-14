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
    [SerializeField] float highlightedScale = 1.2f;
    [SerializeField] float scaleSpeed = 20f;
    [SerializeField] float highlightOffset = 100f;

    [SerializeField] RectTransform widgetRoot;

    Vector3 goalScale = Vector3.one;
    Vector3 goalOffset = Vector3.zero;

    //amt == 0 not scaled, amt == 1 means scaled to 1.5 or highlighted scale
    public void SetScaleAmount(float amt)
    {
        goalScale = Vector3.one * (1 + amt * (highlightedScale - 1));
        goalOffset = Vector3.left * amt * highlightOffset;
    }

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

    private void Update()
    {
        widgetRoot.transform.localPosition = Vector3.Lerp(widgetRoot.transform.localPosition, goalOffset, Time.deltaTime * scaleSpeed);
        widgetRoot.transform.localScale = Vector3.Lerp(widgetRoot.transform.localScale, goalScale, Time.deltaTime * scaleSpeed);
    }
}
