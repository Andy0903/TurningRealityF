using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSelector : BTNode
{

    // Use this for initialization
    void Start()
    {

    }

    public override BTNode process()
    {
        for (int i = 0; i < children.Count; ++i)
        {
            currentNode = children[i].process();
            NodeState = currentNode.NodeState;
            if (NodeState == ReturnValue.RUNNING)
            {
                return currentNode;
            }
            else if (NodeState == ReturnValue.SUCCESS)
                return currentNode;
        }
        return currentNode;
    }
}
