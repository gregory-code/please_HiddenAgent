using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="BehaviorTree/TestBehaviorTree")]
public class TestBehaviorTree : BehaviorTree
{
    protected override void Construct(BTNode_Root root)
    {
        BTTask_Wait waitTask = ScriptableObject.CreateInstance<BTTask_Wait>();
        waitTask.SetWaitTime(3);

        root.AddChild(waitTask);
    }
}
