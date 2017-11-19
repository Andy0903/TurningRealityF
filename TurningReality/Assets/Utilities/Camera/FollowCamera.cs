using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    GameObject target;
    Vector3 offset;

   // [SerializeField]
   // float damping = 10f;
    [SerializeField]
    float maxPitchAngle = 50;
    [SerializeField]
    float minPitchAngle = -50;
    [SerializeField]
    float zoomSpeed = 10f;
    [SerializeField]
    float maxZoomOut = 15;
    [SerializeField]
    float minZoomOut = 3;
    

    private void Start()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
        offset = target.transform.position - transform.position;
    }

    private void ZoomInput()
    {
        float zoom = Input.GetAxis("Zoom");
        bool zoomingOut = (0 < zoom);
        bool zoomingIn = (zoom < 0);

        if (zoomingOut)
        {
            if (offset.z < maxZoomOut)
            {
                offset = new Vector3(offset.x, offset.y, offset.z + (zoomSpeed * Time.deltaTime));
            }
        }
        else if (zoomingIn)
        {
            if (offset.z > minZoomOut)
            {
                offset = new Vector3(offset.x, offset.y, offset.z - (zoomSpeed * Time.deltaTime));
            }
        }
    }

    private void RotationInput()
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
        else if (angle > maxPitchAngle)
        {
            transform.localEulerAngles = new Vector3(maxPitchAngle, transform.rotation.y, transform.rotation.z);
        }
    }

    private void LateUpdate()
    {
        RotationInput();
        ZoomInput();

      //  float currentY = transform.eulerAngles.y;
        float desiredY = target.transform.eulerAngles.y;
       // float angleY = Mathf.LerpAngle(currentY, desiredY, Time.deltaTime * damping);

        Quaternion rotation = Quaternion.Euler(transform.eulerAngles.x, desiredY, 0);

        transform.position = target.transform.position - (rotation * offset);
        transform.LookAt(target.transform);
    }
}
