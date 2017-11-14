using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalManager : MonoBehaviour
{
    public bool isUnlocked = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && isUnlocked)
        {
            if (PathManager.Instance != null)
            {
                PathManager.Instance.Save();
                PathManager.Instance.Clear();
            }

            if (SceneManager.GetActiveScene().buildIndex + 1 <= SceneManager.sceneCountInBuildSettings)
            {
                AudioManager.Instance.Play("Xylophone");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
