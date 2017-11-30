using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManage : MonoBehaviour
{
    public static LevelManage Instance { get; private set; }
    public int[] levelTimes;
    TimeSettings times;
    StatsTracker stats;
    TextManager texts;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        times = GetComponent<TimeSettings>();
        stats = GetComponent<StatsTracker>();
        texts = GetComponent<TextManager>();
    }

    private void OnLevelWasLoaded(int level)
    {
        switch (level)
        {
            case 0:
                times.TimeLimit = 10 * 10;
                break;
            case 1:
                times.TimeLimit = 60 * 10;
                break;
            case 2:
                times.TimeLimit = 80 * 10;
                break;
            case 3:
                times.TimeLimit = 80 * 10;
                break;
            case 4:
                times.TimeLimit = 80 * 10;
                break;
            case 5:
                times.TimeLimit = 80 * 10;
                break;
        }
        if (level > levelTimes.Length)
            times.TimeLimit = 900;
    }

    // Update is called once per frame
    void Update()
    {
        if (texts.isActive)
        {
            times.isCounting = false;
        }
        else
            times.isCounting = true;

        if (times.TimeOver())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            stats.Punish();
        }
    }
}
