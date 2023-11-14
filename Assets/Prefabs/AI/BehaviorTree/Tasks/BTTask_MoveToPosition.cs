using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.AI;

public class BTTask_MoveToPosition : BTNode
{
    [SerializeField] string positionKeyName;

    [SerializeField] float acceptableDistance = 2;

    NavMeshAgent agent;
    GameObject owner;
    Vector3 position;
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

        agent = owner.GetComponent<NavMeshAgent>();
        if (!agent) return BTNodeResult.Failure;

        if (!blackboard.GetBlackboardData(positionKeyName, out position))
            return BTNodeResult.Failure;

        agent.stoppingDistance = acceptableDistance;
        agent.SetDestination(position);
        agent.isStopped = false;

        return BTNodeResult.InProgress;
    }

    protected override BTNodeResult Update()
    {
        if (InAcceptableDistance())
        {
            return BTNodeResult.Success;
        }

        agent.SetDestination(position);

        return BTNodeResult.InProgress;
    }

    private bool InAcceptableDistance()
    {
        return Vector3.Distance(owner.transform.position, position) <= acceptableDistance;
    }

    public override void End()
    {
        if (agent)
        {
            agent.isStopped = true;

        }
        base.End();
    }
}
