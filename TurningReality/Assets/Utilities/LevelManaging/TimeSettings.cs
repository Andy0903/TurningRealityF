using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSettings : MonoBehaviour
{
    [SerializeField]
    public double TimeLimit { get; set; }
    public Text txtTime;

    double time;
    public bool isCounting;

    // Use this for initialization
    void Start()
    {
        SetTimeText();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCounting)
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
