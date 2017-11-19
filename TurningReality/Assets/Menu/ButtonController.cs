using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    [SerializeField]
    GameObject controlScheme;

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (Input.GetButtonDown("Controls"))
        {
            controlScheme.SetActive(!controlScheme.activeSelf);
        }
        else if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
    }

}
