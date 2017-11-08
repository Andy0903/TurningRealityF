﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    GameObject[] buttons;
    GameObject player;
    ButtonRotation currentTrig;
    Transform worldTrans;
    public Color IdleColor, ActiveColor, InActiveColor;

    // Use this for initialization
    void Start()
    {
        worldTrans = GameObject.FindGameObjectWithTag("WorldOrigin").transform;
        buttons = GameObject.FindGameObjectsWithTag("Button");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTrig != null)
        {
            currentTrig.OnRun(worldTrans);

            if (currentTrig.Exit())
            {
                if (currentTrig.CurrentNrOfRotations == currentTrig.TotalNumberOfRotations)
                {
                    currentTrig.CurrentNrOfRotations = 0;
                    SetKinematic(false, player);
                    player.transform.eulerAngles = new Vector3(0, player.transform.eulerAngles.y, 0);
                    currentTrig = null;
                    for (int i = 0; i < buttons.Length; i++)
                    {
                        buttons[i].GetComponent<ButtonRotation>().Triggered = false;
                    }
                }
                else
                    currentTrig.Enter(ActiveColor);
            }
        }
        else
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].GetComponent<ButtonRotation>().Active())
                {
                    buttons[i].GetComponent<Renderer>().material.color = IdleColor;
                    if (buttons[i].GetComponent<ButtonRotation>().Triggered)
                    {
                        currentTrig = buttons[i].GetComponent<ButtonRotation>();
                        currentTrig.Enter(ActiveColor);
                        SetKinematic(true, player);
                    }
                }
                else
                    buttons[i].GetComponent<Renderer>().material.color = InActiveColor;

            }
        }
    }

    public void SetKinematic(bool on, GameObject obj)
    {
        if (obj.GetComponent<Rigidbody>())
        {
            obj.GetComponent<Rigidbody>().isKinematic = on;
        }
    }
}