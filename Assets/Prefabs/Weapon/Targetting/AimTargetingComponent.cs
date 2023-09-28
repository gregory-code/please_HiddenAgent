using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimTargetingComponent : MonoBehaviour
{
    [SerializeField] Transform aimTransform;
    [SerializeField] float aimDistance;
    [SerializeField] LayerMask aimLayerMask;

    public GameObject GetTarget()
    {
        if(Physics.Raycast(aimTransform.position, aimTransform.forward, out RaycastHit hitInfo, aimDistance, aimLayerMask))
        {
            return hitInfo.collider.gameObject;
        }

        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * aimDistance);
    }
}
