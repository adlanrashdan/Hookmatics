using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    public float timeToCompleteLevel = 60f;
    public GameObject timerText;
    public bool timerRunning = true;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            print("There is already an instance of TimeManager");
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!timerRunning)
        {
            return;
        }

        timeToCompleteLevel -= Time.deltaTime;

        timerText.GetComponent<TextMeshPro>().text = "Time: " + Mathf.Round(timeToCompleteLevel * 10) / 10;
        if (timeToCompleteLevel <= 0)
        {
            if (GameManager.instance.mode == GameManager.Mode.Forge)
            {
                GameManager.instance.EndGame();
            }
            else
            {
                GameManager.instance.NextRound();
            }

        }

    }

    public void StopTimer()
    {
        timerRunning = false;
    }
    public void StartTimer()
    {
        timerRunning = true;
    }
    public void AddTime(float time)
    {
        timeToCompleteLevel += time;
    }

    public void TimeFreezeEvent()
    {
        StopTimer();
        Invoke("StartTimer", 5f);
    }
}
