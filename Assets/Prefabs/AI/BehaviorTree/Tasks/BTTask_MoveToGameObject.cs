using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class BTTask_MoveToGameObject : BTNode
{
    [SerializeField] string targetKeyName = "target";

    [SerializeField] float acceptableDistance = 2;

    NavMeshAgent agent;
    GameObject owner;
    GameObject target;
    Blackboard blackboard;

    protected override BTNodeResult Execute()
    {
        if (!GetBehaviorTree()) return BTNodeResult.Failure;

        blackboard = GetBehaviorTree().GetBlackBoard();
        if (blackboard == null) return BTNodeResult.Failure;

        if(!blackboard.GetBlackboardData("owner", out owner))
        {
            return BTNodeResult.Failure;
        }

        agent = owner.GetComponent<NavMeshAgent>();
        if (!agent) return BTNodeResult.Failure;

        if (!blackboard.GetBlackboardData(targetKeyName, out target))
            return BTNodeResult.Failure;

        blackboard.onBlackboardValueChanged -= BlackboardValuedChanged; // helps make sure that we don't have two at the same time. Since Execute will be played multiple times.
        blackboard.onBlackboardValueChanged += BlackboardValuedChanged;

        agent.stoppingDistance = acceptableDistance;
        agent.SetDestination(target.transform.position);
        agent.isStopped = false;

        return BTNodeResult.InProgress;

    }

    private void BlackboardValuedChanged(BlackboardEntry entry)
    {
        if(entry.GetKeyName() == targetKeyName)
        {
            entry.GetVal(out target);
        }
    }

    protected override BTNodeResult Update()
    {
        if (target == null) return BTNodeResult.Failure;

        if (InAcceptableDistance())
        {
            return BTNodeResult.Success;
        }

        agent.SetDestination(target.transform.position);

        return BTNodeResult.InProgress;
    }

    private bool InAcceptableDistance()
    {
        return Vector3.Distance(owner.transform.position, target.transform.position) <= acceptableDistance;
    }

    public override void End()
    {
        if(agent) agent.isStopped = true;
        if (blackboard)
        {
            blackboard.onBlackboardValueChanged -= BlackboardValuedChanged;
        }
        base.End();
    }
}
