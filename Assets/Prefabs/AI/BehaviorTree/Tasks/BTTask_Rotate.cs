using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTTask_Rotate : BTNode
{
    [SerializeField] float angleDegrees;
    [SerializeField] float acceptableOffset;
    [SerializeField] float turnSpeed;

    Quaternion goalRotation;

    GameObject owner;
    Blackboard blackboard;

    protected override BTNodeResult Execute()
    {
        if (!GetBehaviorTree()) return BTNodeResult.Failure;

        blackboard = GetBehaviorTree().GetBlackBoard();
        if (blackboard == null) return BTNodeResult.Failure;

        if (!blackboard.GetBlackboardData("owner", out owner))
        {
            return BTNodeResult.Failure;
        }

        goalRotation = Quaternion.AngleAxis(angleDegrees, Vector3.up)
            * owner.transform.rotation; // Gives the final rotation of a given degrees

        if(IsInAcceptableAngle())
        {
            return BTNodeResult.Success;
        }

        return BTNodeResult.InProgress;
    }

    private bool IsInAcceptableAngle()
    {
        return Quaternion.Angle(goalRotation, owner.transform.rotation) < acceptableOffset;
    }

    protected override BTNodeResult Update()
    {
        owner.transform.rotation = Quaternion.Slerp(owner.transform.rotation, goalRotation, turnSpeed * Time.deltaTime);
        if(IsInAcceptableAngle())
        {
            return BTNodeResult.Success;
        }
        return BTNodeResult.InProgress;
    }

    public override void End()
    {
        base.End();
    }
}
