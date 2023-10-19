using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] BehaviorTree behaviorTreeAsset;

    BehaviorTree behaviorTree;

    // Start is called before the first frame update
    void Start()
    {
        behaviorTree = behaviorTreeAsset.CloneTree();
        behaviorTree?.PreConstruct();
    }

    // Update is called once per frame
    void Update()
    {
        behaviorTree?.Update();
    }
}
