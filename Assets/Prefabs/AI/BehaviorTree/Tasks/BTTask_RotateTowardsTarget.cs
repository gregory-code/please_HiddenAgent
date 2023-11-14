using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_RotateTowardsTarget : BTNode
{
    [SerializeField] string targetKeyName;
    [SerializeField] float acceptableAngle;
    IBTTaskInterface movementInterface;

    GameObject target;
    Blackboard blackboard;
    GameObject owner;

    protected override BTNodeResult Execute()
    {
        if (!GetBehaviorTree()) return BTNodeResult.Failure;

        blackboard = GetBehaviorTree().GetBlackBoard();
        if (blackboard == null) return BTNodeResult.Failure;

        if (!blackboard.GetBlackboardData("owner", out owner))
        {
            return BTNodeResult.Failure;
        }

        if(!blackboard.GetBlackboardData(targetKeyName, out target))
        {
            return BTNodeResult.Failure;
        }

        movementInterface = owner.GetComponent<IBTTaskInterface>();
        if(movementInterface == null)
        {
            return BTNodeResult.Failure;
        }

        if (InAcceptableAngle())
        {
            return BTNodeResult.Success;
        }

        return BTNodeResult.InProgress;
    }

    protected override BTNodeResult Update()
    {
        if(InAcceptableAngle())
        {
            return BTNodeResult.Success;
        }

        movementInterface.RotateTowards(target);
        return BTNodeResult.InProgress;
    }

    private bool InAcceptableAngle()
    {
        return Vector3.Angle(owner.transform.forward, (target.transform.position - owner.transform.position)) < acceptableAngle;
    }
}
