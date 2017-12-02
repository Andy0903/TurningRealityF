using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class PuzzlePieceHolder : MonoBehaviour
{
    bool inRange = false, done = false;
    public bool lockedOn { get; private set; }
    GameObject[] InteractiveObjects;
    GameObject myTarget;
    Vector3 targetPos;

    bool isActive()
    {
        if (Vector3.Dot(new Vector3(0, 1, 0), transform.up) >= 1)
            return true;
        return false;
    }

    private void OnTriggerEnter(Collider p)
    {
        if (isActive())
            if (!inRange && !lockedOn)
            {
                for (int i = 0; i < InteractiveObjects.Length; i++)
                {
                    if (p == InteractiveObjects[i].GetComponent<Collider>())
                    {
                        myTarget = InteractiveObjects[i];
                        inRange = true;
                        return;
                    }
                }

            }
    }

    private void OnTriggerExit(Collider other)
    {
        inRange = false;
    }

    // Use this for initialization
    void Start()
    {
        InteractiveObjects = GameObject.FindGameObjectsWithTag("PuzzlePiece");
        lockedOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!done)
        {
            if (inRange && !lockedOn)
            {
                float distance = Vector3.Distance(myTarget.transform.position, transform.position);
                if (distance < 2.8f)
                {
                    EnterLockOn();
                }
            }
            else if (lockedOn)
            {
                if (!RunLockOn())
                    ExitLockOn();
            }
        }
    }

    private void EnterLockOn()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<ObjectPusher>().ForceDropObject();
        player.GetComponent<ThirdPersonUserControl>().StopTranslation = true;
        targetPos = new Vector3(transform.position.x - (GetComponent<BoxCollider>().size.x / 2), myTarget.transform.position.y, transform.position.z - (GetComponent<BoxCollider>().size.z / 2));
        lockedOn = true;
    }

    private bool RunLockOn()
    {
        float distance = Vector3.Distance(transform.position, targetPos);
        if (distance > 2.3f)
        {
            myTarget.transform.position = Vector3.Lerp(myTarget.transform.position,
                targetPos, Time.deltaTime);
            return true;
        }
        return false;
    }

    private void ExitLockOn()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        myTarget.layer = 0;
        done = true;
        player.GetComponent<ThirdPersonUserControl>().StopTranslation = false;
        myTarget.transform.position = targetPos;
        myTarget.GetComponent<Rigidbody>().isKinematic = true;
        GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>().CheckActivateGoal();
    }
}
