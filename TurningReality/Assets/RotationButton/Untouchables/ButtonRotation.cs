using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRotation : MonoBehaviour
{
    private Vector3 accumulateAngle, angleStep;
    private float coolDown = 0, startTimer = 0.4f;
    private int cdInterval = 30;
    private int CurrentNrOfRotations; 

    public int TotalNumberOfRotations = 1; // Set in unity

    public Color triggeredColor { get; set; }
    public Color activeColor { get; set; }
    public Color disabledColor { get; set; }

    // Give in Units of 1 on a specified axis
    public Vector3[] Axes;

    // The degrees of rotation - 90 tends to work well
    public float[] TiltDegrees;

    // Dont mess with it - used in manager
    public bool Triggered { get; private set; }

    // Add objects that can trigger a rotation here
    public GameObject[] InteractiveObjects;

    public void Start()
    {
        Triggered = false;
        accumulateAngle = Vector3.zero;
        angleStep = Vector3.zero;
        CurrentNrOfRotations = 0;
    }

    public bool Active()
    {
        if (Vector3.Dot(new Vector3(0, 1, 0), transform.up) >= 1)
        {
            ChangeColor(activeColor);
            return true;
        }
        ChangeColor(disabledColor);
        return false;
    }

    public bool AllRotationsTookPlace()
    {
        if (CurrentNrOfRotations == TotalNumberOfRotations)
        {
            CurrentNrOfRotations = 0;
            Triggered = false;
            return true;
        }
        Enter();
        return false;
    }

    public bool ExitCurrentRotation()
    {
        if (accumulateAngle.magnitude >= TiltDegrees[CurrentNrOfRotations])
        {
            accumulateAngle = Vector3.zero;
            CurrentNrOfRotations++;
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

    public void Enter()
    {
        ChangeColor(triggeredColor);
        angleStep = Axes[CurrentNrOfRotations];
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

    private void ChangeColor(Color color)
    {
        GetComponent<Renderer>().material.color = color;
    }
}