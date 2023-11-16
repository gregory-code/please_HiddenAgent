using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaDisplay : MonoBehaviour
{
    PlayerValueGauge valueGauge;
    private void Start()
    {
        GameObject playerGameObject = GameStatics.GetPlayerGameObject();
        AbilityComponent abilityCom = playerGameObject.GetComponent<AbilityComponent>();
        abilityCom.onStaminaChanged += StaminaChange;
        valueGauge = GetComponent<PlayerValueGauge>();
        valueGauge.SetValue(abilityCom.GetStamina(), abilityCom.GetMaxStamina());
    }

    private void StaminaChange(float current, float max)
    {
        valueGauge.SetValue(current, max);   
    }
}
