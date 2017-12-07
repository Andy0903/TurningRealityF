using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;

public class ArrowScript : MonoBehaviour
{
    int index = 0;
    public int totalButtons = 3;
    public float yOffSet = 20;
    [SerializeField]
    GameObject controlScheme;
    // Use this for initialization
    bool joystickReadyToMove = true;
    bool selectLevel = false;

    void Update()
    {
        if (Input.GetAxis("Vertical") < 0.5f && -0.5f < Input.GetAxis("Vertical"))
        {
            joystickReadyToMove = true;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxis("Vertical") == -1 && joystickReadyToMove)
        {
            if (index < totalButtons - 1)
            {
                joystickReadyToMove = false;
                index++;
                Vector2 position = transform.position;
                position.y -= yOffSet;
                transform.position = position;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetAxis("Vertical") == 1 && joystickReadyToMove)
        {
            if (index > 0)
            {
                joystickReadyToMove = false;
                index--;
                Vector2 position = transform.position;
                position.y += yOffSet;
                transform.position = position;
            }
        }
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Return) && !selectLevel)
        {
            switch (index)
            {
                case 0:
                    SceneManager.LoadScene(1);
                    break;
                case 1:
                    controlScheme.SetActive(!controlScheme.activeSelf);
                    break;
                case 2:
                    //TODO: select level from dropdown menu
                    Debug.Log("case 2");
                    SceneManager.LoadScene(8);
                    //selectLevel = true;
                    break;
                case 3:
                    Application.Quit();
                    break;
            }
        }
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Return) && selectLevel)
        {
            if (selectLevel)
            {
                switch (index)
                {
                    case 0:
                        SceneManager.LoadScene(2);
                        break;
                    case 1:
                        SceneManager.LoadScene(3);
                        break;
                    case 2:
                        SceneManager.LoadScene(4);
                        break;
                    case 3:
                        SceneManager.LoadScene(5);
                        break;
                    case 4:
                        SceneManager.LoadScene(6);
                        break;
                    case 5:
                        SceneManager.LoadScene(0);
                        break;
                }
            }
        }
        
    }
    void OnLevelWasLoaded(int level)
    {
        if(level == 8)
        {
            index = 0;
            selectLevel = true;
        }
        if(level == 0)
        {
            selectLevel = false;
        }
    }
}

