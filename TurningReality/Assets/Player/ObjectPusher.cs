using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPusher : MonoBehaviour
{
    Transform target;
    Transform targetParent;
    Vector3 originalAngles;

    [SerializeField]
    Vector3 rayOffset = new Vector3(0, 0, 0);
    [SerializeField]
    float pickupRange = 2f;

    float minDistance = 0;
    float maxDistance = 50f;

    bool oldKinimaticState;

    public void ForceDropObject()
    {
        if (target != null)
            DropObject();
    }

    private void CastRay()
    {
        Ray hitRay = new Ray(transform.position - rayOffset, transform.forward);

        RaycastHit hit;
        if (Physics.Raycast(hitRay, out hit, pickupRange, 1 << 8))
        {
            Debug.Log("Finds target!");
            target = hit.transform;
            targetParent = target.parent;

            originalAngles = transform.eulerAngles;
            oldKinimaticState = target.gameObject.GetComponent<Rigidbody>().isKinematic;
            target.gameObject.GetComponent<Rigidbody>().isKinematic = false;

            target.SetParent(transform);
            FixedJoint joint = target.gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = gameObject.GetComponent<Rigidbody>();
            joint.breakForce = Mathf.Infinity;
            joint.breakTorque = Mathf.Infinity;
        }
        Debug.Log("Misses target!");
    }

    private void DropObject()
    {
        Debug.Log("Drops object!");
        target.SetParent(targetParent);
        targetParent = null;
        target.gameObject.GetComponent<Rigidbody>().isKinematic = oldKinimaticState;
        Destroy(target.gameObject.GetComponent<FixedJoint>());
        target = null;
    }

    private void Update()
    {
        ProcessInput();

        if (target)
        {
            transform.eulerAngles = originalAngles;

            Vector3 fromTargetToPlayer = transform.position - target.position;
            float distance = fromTargetToPlayer.magnitude;
            Vector3 direction = fromTargetToPlayer.normalized;

            if (distance < minDistance || maxDistance < distance)
            {
                DropObject();
            }
        }
    }

    private void ProcessInput()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (target == null)
            {
                Debug.Log("Casts ray!");
                CastRay();
            }
            else
            {
                DropObject();
            }
        }
    }
}
