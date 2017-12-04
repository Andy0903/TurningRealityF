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
    void Start()
    {

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetButtonDown("Vertical"))
        {
            if (index < totalButtons - 1)
            {
                index++;
                Debug.Log(index);
                Vector2 position = transform.position;
                position.y -= yOffSet;
                transform.position = position;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetButtonDown("Vertical") )
        {
            
            if (index > 0)
            {
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

