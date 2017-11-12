using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTInverter : BTNode
{

    BTNode nodeToInvert;

    // Use this for initialization
    void Start()
    {

    }

    public override BTNode process()
    {
        nodeToInvert = children[0];
        nodeToInvert.process();
        if (nodeToInvert.NodeState == ReturnValue.RUNNING)
        {
            NodeState = ReturnValue.RUNNING;
            return nodeToInvert;
        }
        else if (nodeToInvert.NodeState == ReturnValue.SUCCESS)
        {
            NodeState = ReturnValue.FAILURE;
            return nodeToInvert;
        }
        else
        {
            NodeState = ReturnValue.SUCCESS;
            return nodeToInvert;
        }
    }
}
