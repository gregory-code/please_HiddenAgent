using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScanner : MonoBehaviour
{
     float range;
     float scanSpeed;
    float duration;
     [SerializeField] Transform scanPivot;
    public event Action<GameObject> onNewTargetFound;

    private void Awake()
    {
        scanPivot.transform.localScale = Vector3.zero;
    }

    public void Init(float range, float duration, GameObject visual)
    {
        this.range = range;
        this.duration = duration;
        if (visual == null) return;
        visual.transform.parent = scanPivot;
        visual.transform.localPosition = Vector3.zero;
    }

    public void StartScan()
    {
        StartCoroutine(ScanCoroutine());
    }

    IEnumerator ScanCoroutine()
    {
        float timeElapsed = 0;
        float scaleRate = range / duration;
        while(timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            scanPivot.transform.localScale += Vector3.one * scaleRate * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }

    public void SetupAttachment(Transform parent)
    {
        transform.parent = parent;
        transform.localPosition = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        onNewTargetFound?.Invoke(other.gameObject);
    }
}
