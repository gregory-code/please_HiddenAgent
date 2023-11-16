using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurationDamage : MonoBehaviour
{
    float duration;
    float damage;

    public void Init(float duration, float damage, HealthComponet healthcomp, GameObject instigator, GameObject damageVFXPrefab)
    {
        this.duration = duration;
        this.damage = damage;

        GameObject damageVFX = Instantiate(damageVFXPrefab, healthcomp.transform);
        damageVFX.transform.position = Vector3.zero;
        damageVFX.transform.localPosition = Vector3.zero;

        StartCoroutine(DamageCoroutine(healthcomp, instigator, damageVFX));
    }

    IEnumerator DamageCoroutine(HealthComponet healthcomp, GameObject instigator, GameObject damageVFX)
    {
        float timeElapsed = 0;
        float damageRate = damage / duration;
        while(timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            healthcomp.ChangeHealth(-damageRate * Time.deltaTime, instigator);
            yield return new WaitForEndOfFrame();
        }

        Destroy(damageVFX);
        Destroy(this); // this means component not the gameobject
    }


}
