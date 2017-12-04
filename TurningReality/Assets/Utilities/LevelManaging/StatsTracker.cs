using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsTracker : MonoBehaviour {
    int score = 0;
    public Text txtScore;

	void Awake () {
        SetScoreText(0);
    }

    public void SetScoreText(int amount)
    {
        score += amount;
        txtScore.text = "Score: " + score.ToString();
    }

    public void Punish()
    {
        if (score >= 20)
            score -= 20;
        else
            score = 0;
    }
}
