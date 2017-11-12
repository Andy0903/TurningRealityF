using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public Color ActiveColor, TriggeredColor, DisabledColor;
    ButtonRotation currentTrig;
    Transform worldTrans;
    GameObject[] buttons;
    GameObject player;

    // Use this for initialization
    void Start()
    {
        worldTrans = GameObject.FindGameObjectWithTag("WorldOrigin").transform;
        buttons = GameObject.FindGameObjectsWithTag("Button");
        player = GameObject.FindGameObjectWithTag("Player");
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
            currentTrig.Running(worldTrans);

            if (currentTrig.Exit())
            {
                SetKinematic(false, player);
                player.transform.eulerAngles = new Vector3(0, player.transform.eulerAngles.y, 0);
                player.GetComponent<Movement>().WorldIsRotating = false;
                currentTrig = null;
            }
        }
        else
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                ButtonRotation temp = buttons[i].GetComponent<ButtonRotation>();
                if (temp.Active())
                {
                    currentTrig = temp;
                    currentTrig.Enter();
                    SetKinematic(true, player);
                    player.GetComponent<Movement>().WorldIsRotating = true;
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
