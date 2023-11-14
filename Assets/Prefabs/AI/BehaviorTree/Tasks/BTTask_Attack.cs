using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_Attack : BTNode
{
    [SerializeField] string keyname;

    protected override BTNodeResult Execute()
    {
        IBTTaskInterface taskInterface = GetInterface();
        if (taskInterface == null)
            return BTNodeResult.Failure;

        Blackboard blackboard = GetBlackboard();
        if (!blackboard)
            return BTNodeResult.Failure;

        blackboard.GetBlackboardData(keyname, out GameObject target);

        taskInterface.AttackTarget(target);
        return BTNodeResult.Success;
    }
}
