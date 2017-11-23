using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    GameObject target;
    Vector3 offset;

   // readonly Vector3 zeroPos;
    Vector3 sineWavePos;

    //[SerializeField]
    float damping = 4f;
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

   // public bool IsWalking { get; set; }


    private void Start()
    {
        const int zoffset = 7;
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z - zoffset);
        offset = target.transform.position - transform.position;
        // IsWalking = false;
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

        float currentY = transform.eulerAngles.y;
        float desiredY = target.transform.eulerAngles.y;
        float angleY = Mathf.LerpAngle(currentY, desiredY, Time.deltaTime * damping);

        float currentX = transform.eulerAngles.x;
        float desiredX = target.transform.parent.eulerAngles.x;
        float angleX = Mathf.LerpAngle(currentX, desiredX, Time.deltaTime * damping / (damping-1));

        Debug.Log("1: " + angleX);
        Debug.Log("2: " + transform.eulerAngles.x);

        Quaternion rotation = Quaternion.Euler(angleX, angleY, 0);  //transform.eulerAngles.x

        transform.position = target.transform.position - (rotation * offset);
        transform.LookAt(target.transform);


        SineBobbing();
    }

    private Vector2 CalculateHoverPosition(Vector2 amplitude, Vector2 frequency)
    {
        float posX = sineWavePos.x;
        float posY = sineWavePos.y;

        posX += frequency.x * Time.deltaTime * 1000;
        posY += frequency.y * Time.deltaTime * 1000;

        // Subtracts all full cycles to avoid overflows.
        posX -= (float)(Mathf.Floor(posX / (Mathf.PI * 2)) * Mathf.PI * 2);
        posY -= (float)(Mathf.Floor(posY / (Mathf.PI * 2)) * Mathf.PI * 2);

        sineWavePos = new Vector2(posX, posY);

        return new Vector2(amplitude.x * (float)Mathf.Sin(posX), amplitude.y * (float)Mathf.Sin(posY));
    }

    public void SineBobbing()
    {
        Vector2 amplitude = new Vector2(Screen.width * 0.0002f, Screen.height * 0.0002f);
        Vector2 frequency = new Vector2(0.0005f, 0.001f);

        Vector2 newPosition = CalculateHoverPosition(amplitude, frequency);
        
        transform.position += new Vector3(0, newPosition.y, 0);
    }
}
