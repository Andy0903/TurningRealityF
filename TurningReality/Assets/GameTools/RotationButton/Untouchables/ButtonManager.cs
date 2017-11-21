﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public Color ActiveColor, TriggeredColor, DisabledColor;
    ButtonRotation currentTrig;
    Transform worldTrans;
    GameObject[] buttons;
    GameObject currObj;
    Vector3 LevitatePos;

    bool objInPosition = false;

    // Use this for initialization
    void Start()
    {
        worldTrans = GameObject.FindGameObjectWithTag("WorldOrigin").transform;
        buttons = GameObject.FindGameObjectsWithTag("Button");

        for (int i = 0; i < buttons.Length; i++)
        {
            ButtonRotation temp = buttons[i].GetComponent<ButtonRotation>();
            temp.disabledColor = DisabledColor;
            temp.triggeredColor = TriggeredColor;
            temp.activeColor = ActiveColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTrig != null)
        {
            if (objInPosition)
            {
                currentTrig.Running(worldTrans);

                if (currentTrig.Exit())
                {
                    SetKinematic(false, currObj);
                    currObj.transform.eulerAngles = new Vector3(0, currObj.transform.eulerAngles.y, 0);
                    currObj.GetComponent<Movement>().StopTranslation = false;
                    currentTrig = null;
                    objInPosition = false;
                }
            }
            else
                ObjectLevitates();
        }
        else
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                ButtonRotation temp = buttons[i].GetComponent<ButtonRotation>();
                if (temp.Active())
                {
                    if (temp.Triggered)
                    {
                        currentTrig = temp;
                        currentTrig.Enter();
                        currObj = currentTrig.interactedObj;
                        currObj.GetComponent<Movement>().StopTranslation = true;
                        currObj.GetComponent<ObjectPusher>().ForceDropObject();
                        LevitatePos = new Vector3(temp.transform.position.x, temp.transform.position.y + 2, temp.transform.position.z);

                        SetKinematic(true, currObj);
                    }
                }
            }
        }
    }

    private void ObjectLevitates()
    {
        currObj.transform.position = Vector3.Lerp(currObj.transform.position, LevitatePos, Time.deltaTime * 3);
        if (Vector3.Distance(currObj.transform.position, LevitatePos) < 0.1)
            objInPosition = true;
    }

    public void SetKinematic(bool on, GameObject obj)
    {
        if (obj.GetComponent<Rigidbody>())
        {
            obj.GetComponent<Rigidbody>().isKinematic = on;
        }
    }
}
