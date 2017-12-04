using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrowScript : MonoBehaviour
{
    int index = 0;
    public int totalButtons = 3;
    public float yOffSet = 20;
    GameObject controlScheme;
    // Use this for initialization
    bool joystickReadyToMove = true;

    void Start()
    {

    }
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
                Debug.Log(index);
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
                Debug.Log(index);
                Vector2 position = transform.position;
                position.y += yOffSet;
                transform.position = position;
            }
        }
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Return))
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
                    Application.Quit();
                    break;
            }
        }
    }
}

