using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public class TextManager : MonoBehaviour
{
    public GameObject textBox;
    public TextAsset textFile;
    public Text displayText;
    public bool isActive;

    GameObject player;
    string[] textLines;
    int currentIndex;

    // Use this for initialization
    void Start()
    {
        ResetText();
        if (isActive)
            EnableBox(true);
        else
            EnableBox(false);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            displayText.text = textLines[currentIndex];
            if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Interact"))
                currentIndex++;
            OnTextExit();
        }
    }

    public void OnTextExit()
    {
        if (currentIndex > textLines.Length - 1)
        {
            isActive = false;
            EnableBox(false);
            currentIndex = 0;
            //GameObject player = GameObject.FindGameObjectWithTag("Player");
            //ThirdPersonUserControl control = player.GetComponent<ThirdPersonUserControl>();
            //control.StopTranslation = false;
        }
    }

    public void EnableBox(bool enable)
    {
        textBox.SetActive(enable);
    }

    public void ResetText()
    {
        textLines = (textFile.text.Split('\n'));
        currentIndex = 0;
    }

    public void SetTextFile(TextAsset file)
    {
        textFile = file;
    }

    public void StartNewDialogue(TextAsset file)
    {
        if (file != null)
        {
            //GameObject player = GameObject.FindGameObjectWithTag("Player");
            //ThirdPersonUserControl control = player.GetComponent<ThirdPersonUserControl>();
            //control.StopTranslation = true;
            textFile = file;
            isActive = true;
            EnableBox(true);
            ResetText();
        }
    }
}
