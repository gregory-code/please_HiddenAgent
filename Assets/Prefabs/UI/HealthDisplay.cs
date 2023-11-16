using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    HealthComponet healthComponent;
    PlayerValueGauge valueGauge;
    private void Start()
    {
        GameObject playerGameObject = GameStatics.GetPlayerGameObject();
        healthComponent = playerGameObject.GetComponent<HealthComponet>();

        valueGauge = GetComponent<PlayerValueGauge>();
        valueGauge.SetValue(healthComponent.GetHealth(), healthComponent.GetMaxHealth());
        healthComponent.onHealthChanged += UpdateValue;
    }

    private void UpdateValue(float currentHealth, float delta, float maxHealth)
    {
        valueGauge.SetValue(currentHealth, maxHealth);
    }
}
