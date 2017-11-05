using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    GameObject target;
    Vector3 offset;

    [SerializeField]
    float damping = 10f;
    [SerializeField]
    float maxPitchAngle = 50;
    [SerializeField]
    float minPitchAngle = -50;

    private void Start()
    {
        offset = target.transform.position - transform.position;
    }

    void Update()
    {
        RotationInput();
    }

    void RotationInput()
    {
        float pitch = Input.GetAxis("Pitch");
        pitch = Mathf.Clamp(pitch, -1, 1);

        transform.Rotate(pitch, 0, 0);

        float angle = transform.localEulerAngles.x;
        angle = (angle > 180) ? angle - 360 : angle;

        if (angle < minPitchAngle)
        {
            transform.localEulerAngles = new Vector3(minPitchAngle, transform.rotation.y, transform.rotation.z);
        }
        if (angle > maxPitchAngle)
        {
            transform.localEulerAngles = new Vector3(maxPitchAngle, transform.rotation.y, transform.rotation.z);
        }
    }


    private void LateUpdate()
    {
        float currentY = transform.eulerAngles.y;
        float desiredY = target.transform.eulerAngles.y;
        float angleY = Mathf.LerpAngle(currentY, desiredY, Time.deltaTime * damping);

        Quaternion rotation = Quaternion.Euler(transform.eulerAngles.x, angleY, 0);

        transform.position = target.transform.position - (rotation * offset);
        transform.LookAt(target.transform);
    }
}
