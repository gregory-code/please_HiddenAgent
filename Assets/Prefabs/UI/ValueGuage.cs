using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueGuage : MonoBehaviour
{
    [SerializeField] Slider slider;
    public void SetValue(float val, float maxVal)
    { 
        slider.value = val/maxVal;
    }
}
