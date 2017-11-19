using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject pauseMenu;

    void LateUpdate()
    {
        if (Input.GetButtonDown("StartButton"))
        {
            if (Time.timeScale != 0)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }

            pauseMenu.SetActive(!pauseMenu.activeSelf);
        }
    }
}
