using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRotation : MonoBehaviour
{
    private Vector3 accumulateAngle, angleStep, targetAngle;
    private float coolDown = 0, startTimer = 0.4f;
    private int cdInterval = 30;

    public int TotalNumberOfRotations = 1; // Set in unity

    public int CurrentNrOfRotations { get; set; } // used in manager

    // Give in Units of 1 on a specified axis
    public Vector3[] Axes;

    // The degrees of rotation - 90 tends to work well
    public float[] TiltDegrees;

    // Dont mess with it - used in manager
    public bool Triggered { get; set; }

    // Add objects that can trigger a rotation here
    public GameObject[] InteractiveObjects;

    public void Start()
    {
        accumulateAngle = Vector3.zero;
        targetAngle = accumulateAngle;
        angleStep = accumulateAngle;
        CurrentNrOfRotations = 0;
    }

    public bool Active()
    {
        if (Vector3.Dot(transform.up, new Vector3(0, 1, 0)) == 1)
        {
            return true;
        }
        return false;
    }

    public bool Exit()
    {
        if (accumulateAngle.magnitude >= TiltDegrees[CurrentNrOfRotations - 1])
        {
            accumulateAngle = Vector3.zero;
            coolDown = 0;
            return true;
        }

        return false;
    }

    private void OnTriggerEnter(Collider p)
    {
        if (!Triggered && Active())
        {
            for (int i = 0; i < InteractiveObjects.Length; i++)
            {
                if (p == InteractiveObjects[i].GetComponent<Collider>())
                {
                    Triggered = true;
                    return;
                }
            }
        }
    }

    public void Enter(Color color)
    {
        GetComponent<Renderer>().material.color = color;
        angleStep = Axes[CurrentNrOfRotations];
        targetAngle = angleStep * TiltDegrees[CurrentNrOfRotations];
        CurrentNrOfRotations++;
    }

    public void OnRun(Transform worldTrans)
    {
        if (!MustCoolDown())
        {
            accumulateAngle += angleStep;
            worldTrans.Rotate(angleStep, Space.World);
        }
    }

    private bool MustCoolDown()
    {
        if (accumulateAngle.magnitude % cdInterval == 0)
        {
            coolDown -= Time.deltaTime;
            if (coolDown > 0)
                return true;
        }
        coolDown = startTimer;
        return false;
    }
}