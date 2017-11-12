using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTNode : MonoBehaviour
{
    public enum ReturnValue
    {
        ERROR,
        SUCCESS,
        FAILURE,
        RUNNING,
    }
    public ReturnValue NodeState { get; set; }

    protected List<BTNode> children;

    protected BTNode currentNode;
    protected BlackBox data;


    // Use this for initialization
    void Start()
    {
        NodeState = ReturnValue.ERROR;
        children = new List<BTNode>();
    }

    public virtual BTNode process()
    {
        return null;
    }

    public void AddChild(BTNode child)
    {
        children.Add(child);
    }
}
