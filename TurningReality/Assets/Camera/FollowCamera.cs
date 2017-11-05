using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    GameObject target;
    Vector3 offset;

    [SerializeField]
    float damping = 1;

    private void Start()
    {
        offset = target.transform.position - transform.position;
    }

    private void LateUpdate()
    {
        float currentAngle = transform.eulerAngles.y;
        float desiredAngle = target.transform.eulerAngles.y;
        float angle = Mathf.LerpAngle(currentAngle, desiredAngle, Time.deltaTime * damping);

        Quaternion rotation = Quaternion.Euler(0, angle, 0);

        transform.position = target.transform.position - (rotation * offset);
        transform.LookAt(target.transform);
    }
}
