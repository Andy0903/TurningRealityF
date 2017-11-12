using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTree : MonoBehaviour
{
    public BTNode currentNode;
    public BTSequence root;
    public BlackBox data;

    BTApproach approach;
    BTEvade evade;
    BTIdle idle;

    // Use this for initialization
    void Start()
    {
        data = new BlackBox();
        root = new BTSequence();
        evade = new BTEvade();
        approach = new BTApproach();
        idle = new BTIdle();
        root.AddChild(evade);
        root.AddChild(approach);
        root.AddChild(idle);
        currentNode = root;
    }

    // Update is called once per frame
    void Update()
    {
        //data.processGameState(game);
        currentNode = currentNode.process();

        switch (currentNode.NodeState)
        {
            case BTNode.ReturnValue.FAILURE:
                Reset();
                break;
            case BTNode.ReturnValue.RUNNING:
                //DO NOTHING
                break;
            case BTNode.ReturnValue.SUCCESS:
                Reset();
                break;
        }
    }

    void Reset()
    {
        currentNode = root;
    }
}
