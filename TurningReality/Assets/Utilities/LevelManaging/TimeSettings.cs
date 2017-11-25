using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSettings : MonoBehaviour {
    public double TimeLimit = 30;
    public bool TimedLevel;
    public Text txtTime;

    double time;

    // Use this for initialization
    void Start () {
        if (TimedLevel)
            SetTimeText();
        else
            txtTime.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (TimedLevel)
        {
            TimeLimit -= Time.deltaTime;
            UpdateGUI();
        }
    }

    public bool TimeOver()
    {
        if (TimeLimit > 0)
            return false;
        return true;
    }

    private void SetTimeText()
    {
        txtTime.text = "Time Left: " + ((int)TimeLimit).ToString();
    }

    private void UpdateGUI()
    {
        time += Time.deltaTime;
        if (time >= 0.5)
        {
            SetTimeText();
            time = 0;
        }
    }
}
