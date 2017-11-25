using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSettings : MonoBehaviour
{
    public float moveSpeed = 0.35f, rotateSpeed = 0.75f, moveInterval = 0.5f;
    bool moveUp;
    double time;
    public int ScoreAmount;

    StatsTracker trace;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            GameObject.Find("ScoreManager").GetComponent<StatsTracker>().SetScoreText(ScoreAmount);
        }
    }

    // Use this for initialization
    void Start()
    {
        moveUp = true;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeMoveDirection();
        transform.Rotate(0, rotateSpeed, 0, Space.Self);
        transform.Translate(0, moveSpeed * Time.deltaTime, 0, Space.Self);
    }

    private void ChangeMoveDirection()
    {
        time += Time.deltaTime;
        if (time > moveInterval)
        {
            time = 0;
            moveSpeed *= -1;
        }
    }
}
