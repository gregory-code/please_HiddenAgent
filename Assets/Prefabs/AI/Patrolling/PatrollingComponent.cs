using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingComponent : MonoBehaviour
{
    [SerializeField] PatrollingWaypoint startPoint;
    [SerializeField]

    public bool GetNextPatrollingPos(out Vector3 pos)
    {
        pos = Vector3.zero;
        if (startPoint == null) return false;

        pos = startPoint.GetPosition();
        startPoint = startPoint.Next();
        return true;
    }
}
