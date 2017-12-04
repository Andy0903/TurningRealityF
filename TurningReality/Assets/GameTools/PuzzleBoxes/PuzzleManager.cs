using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    GameObject[] puzzlePieceHolders;
    GameObject[] puzzlePieces;
    GoalManager goal;

    // Use this for initialization
    void Start()
    {
        puzzlePieceHolders = GameObject.FindGameObjectsWithTag("PuzzlePieceHolder");
        puzzlePieces = GameObject.FindGameObjectsWithTag("PuzzlePiece");
        goal = GameObject.Find("Goal").GetComponent<GoalManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckActivateGoal()
    {
        for (int i = 0; i < puzzlePieceHolders.Length; i++)
        {
            if (!puzzlePieceHolders[i].GetComponent<PuzzlePieceHolder>().lockedOn)
                return;
            else if (i == puzzlePieceHolders.Length - 1)
            {
                goal.isUnlocked = true;
            }
        }
    }
}
