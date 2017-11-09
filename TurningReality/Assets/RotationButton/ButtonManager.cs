using System.Collections;
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
        for (int i = 0; i < buttons.Length; i++)
        {
            ButtonRotation temp = buttons[i].GetComponent<ButtonRotation>();
            temp.disabledColor = InActiveColor;
            temp.activeColor = ActiveColor;
            temp.idleColor = IdleColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTrig != null)
        {
            currentTrig.OnRun(worldTrans);
            
            if (currentTrig.ExitCurrentRotation())
            {
                if (currentTrig.AllRotationsTookPlace())
                {
                    SetKinematic(false, player);
                    player.transform.eulerAngles = new Vector3(0, player.transform.eulerAngles.y, 0);
                    player.GetComponent<Movement>().WorldIsRotating = false;
                    currentTrig = null;
                    for (int i = 0; i < buttons.Length; i++)
                    {
                        buttons[i].GetComponent<ButtonRotation>().Triggered = false;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].GetComponent<ButtonRotation>().Active())
                {
                    if (buttons[i].GetComponent<ButtonRotation>().Triggered)
                    {
                        currentTrig = buttons[i].GetComponent<ButtonRotation>();
                        currentTrig.Enter();
                        SetKinematic(true, player);
                        player.GetComponent<Movement>().WorldIsRotating = true;
                    }
                }
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
