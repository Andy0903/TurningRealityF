using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsTracker : MonoBehaviour {
    public static StatsTracker Instance { get; private set; }

    int score = 0;
    public Text txtScore;

	void Awake () {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        SetScoreText(0);
    }

    public void SetScoreText(int amount)
    {
        score += amount;
        txtScore.text = "Score: " + score.ToString();
    }

}
