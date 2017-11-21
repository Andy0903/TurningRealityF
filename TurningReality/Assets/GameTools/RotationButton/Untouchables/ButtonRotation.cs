using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRotation : MonoBehaviour
{
    private float coolDown = 0, startTimer = 0.4f;
    private Vector3 accumulateAngle, angleStep;
    private int CurrentNrOfRotations;
    private int cdInterval = 30;
    public bool Triggered { get; set; }

    public Color triggeredColor { get; set; }
    public Color disabledColor { get; set; }
    public Color activeColor { get; set; }
    public GameObject interactedObj { get; private set; }

    //Unity accessibles
    public int TotalNumberOfRotations = 1; // Set in unity
    // Add objects that can trigger a rotation here
    public GameObject[] InteractiveObjects;
    // The degrees of rotation - 90 tends to work well
    public float[] TiltDegrees;
    // Give in Units of 1 on a specified axis
    public Vector3[] Axes;

    bool cogTickSoundNeedsToPlay = false;

    public void Start()
    {
        Triggered = false;
        accumulateAngle = Vector3.zero;
        angleStep = Vector3.zero;
        CurrentNrOfRotations = 0;
    }

    private void OnTriggerEnter(Collider p)
    {
        if (!Triggered && Active())
        {
            for (int i = 0; i < InteractiveObjects.Length; i++)
            {
                if (p == InteractiveObjects[i].GetComponent<Collider>())
                {
                    AudioManager.Instance.Play("ButtonPress");
                    interactedObj = InteractiveObjects[i];
                    Triggered = true;
                    return;
                }
            }
        }
    }

    public bool Active()
    {
        if (Vector3.Dot(new Vector3(0, 1, 0), transform.up) >= 1)
        {
            ChangeColor(activeColor);
            return true;
        }
        else
            ChangeColor(disabledColor);
        return false;
    }

    public void Enter()
    {
        AudioManager.Instance.Play("WorldRotationMetalMoan");
        AudioManager.Instance.Play("WorldRotationMetalThunder");
        AudioManager.Instance.Play("WorldRotationFastChugging");
        ChangeColor(triggeredColor);
        angleStep = Axes[CurrentNrOfRotations];
    }

    public void Running(Transform worldTrans)
    {
        if (!MustCoolDown())
        {
            accumulateAngle += angleStep * 0.5f;
            worldTrans.Rotate(angleStep * 0.5f, Space.World);
        }
    }

    public bool Exit()
    {
        if (accumulateAngle.magnitude >= TiltDegrees[CurrentNrOfRotations])
        {
            accumulateAngle = Vector3.zero;
            CurrentNrOfRotations++;
            coolDown = 0;

            if (CurrentNrOfRotations == TotalNumberOfRotations)
            {
                CurrentNrOfRotations = 0;
                Triggered = false;
                AudioManager.Instance.Play("WorldRotationCogTick", true);
                AudioManager.Instance.Stop("WorldRotationFastChugging", 1f);
                return true;
            }
            else // There are more rotations in the cycle
                Enter();
        }
        return false;
    }

    private bool MustCoolDown()
    {
        if (accumulateAngle.magnitude % cdInterval == 0)
        {
            if (coolDown == startTimer)
            {
                AudioManager.Instance.Play("WorldRotationCogTick", true);
            }

            coolDown -= Time.deltaTime;
            if (coolDown > 0)
            {
                return true;
            }
        }
        coolDown = startTimer;
        return false;
    }

    private void ChangeColor(Color color)
    {
        GetComponent<Renderer>().material.color = color;
    }
}