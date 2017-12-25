using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class ButtonManager : MonoBehaviour
{
    public Color ActiveColor, TriggeredColor, DisabledColor;
    ButtonRotation currentButton;
    Transform worldTrans;
    GameObject[] buttons;
    GameObject currObj, player;
    Vector3 LevitatePos;

    bool objInPosition = false;

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
        if (currentButton != null)
        {
            if (objInPosition)
            {
                currentButton.Running(worldTrans);

                if (currentButton.Exit())
                {
                    SetKinematic(false, currObj);
                    SetKinematic(false, player);
                    player.transform.eulerAngles = new Vector3(0, currObj.transform.eulerAngles.y, 0);
                    player.GetComponent<ThirdPersonUserControl>().StopTranslation = false;
                    currentButton = null;
                    objInPosition = false;
                }
            }
            else
                LevitateObject();
        }
        else
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                ButtonRotation tempButton = buttons[i].GetComponent<ButtonRotation>();
                if (tempButton.Active())
                {
                    if (tempButton.Triggered)
                    {
                        currObj = tempButton.interactedObj;
                        LevitatePos = new Vector3(tempButton.transform.position.x, tempButton.transform.position.y + (currObj.GetComponent<Collider>().bounds.size.y / 2), tempButton.transform.position.z);

                        if (currObj == player || PlayerBlocksObject(LevitatePos))
                        {
                            currentButton = tempButton;
                            currentButton.Enter();
                            SetKinematic(true, currObj);
                            player.GetComponent<ThirdPersonUserControl>().StopTranslation = true;
                            player.GetComponent<ThirdPersonCharacter>().HaltMovement();
                            player.GetComponent<ObjectPusher>().ForceDropObject();
                            SetKinematic(true, player);
                        }


                    }
                }
            }
        }
    }

    private bool PlayerBlocksObject(Vector3 other)
    {
        float dist = Vector3.Distance(player.transform.position, other);
        if (dist > 4)
            return true;
        return false;
    }

    private void LevitateObject()
    {
        currObj.transform.position = Vector3.Lerp(currObj.transform.position, LevitatePos, Time.deltaTime * 3);
        if (Vector3.Distance(currObj.transform.position, LevitatePos) <= Time.deltaTime * 3)
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
