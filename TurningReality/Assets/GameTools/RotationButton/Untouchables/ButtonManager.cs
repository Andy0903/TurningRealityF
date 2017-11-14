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
    Vector3 LevitatePos;

    bool playerInPosition = false;

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
            if (playerInPosition)
            {
                currentTrig.Running(worldTrans);

                if (currentTrig.Exit())
                {
                    SetKinematic(false, player);
                    player.transform.eulerAngles = new Vector3(0, player.transform.eulerAngles.y, 0);
                    player.GetComponent<Movement>().WorldIsRotating = false;
                    currentTrig = null;
                    playerInPosition = false;
                }
            }
            else
                PlayerLevitates();
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
                        player.GetComponent<Movement>().WorldIsRotating = true;
                        player.GetComponent<ObjectPusher>().ForceDropObject();
                        currentTrig = temp;
                        LevitatePos = new Vector3(temp.transform.position.x, temp.transform.position.y + 2, temp.transform.position.z);

                        currentTrig.Enter();
                        SetKinematic(true, player);
                    }
                }
            }
        }
    }

    private void PlayerLevitates()
    {
        player.transform.position = Vector3.Lerp(player.transform.position, LevitatePos, Time.deltaTime * 3);
        if (Vector3.Distance(player.transform.position, LevitatePos) < 0.1)
            playerInPosition = true;
    }

    public void SetKinematic(bool on, GameObject obj)
    {
        if (obj.GetComponent<Rigidbody>())
        {
            obj.GetComponent<Rigidbody>().isKinematic = on;
        }
    }
}
