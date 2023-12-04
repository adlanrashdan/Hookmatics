using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    // singelton
    public static ScoreManager instance;
    public TextMeshPro scoreDisplay;
    public int score = 0;
    public int pointsCorrect = 10;
    public int pointsWrong = -2;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            print("There is already an instance of ScoreManager");
        }
    }

    public void IncrementScore()
    {
        //LevelManager.instance.NextLevel();
        LevelManager.instance.CheckNextLevel();
        score += pointsCorrect;
        UpdateScore();
        TimeManager.instance.AddTime(1f);
        print("Score!");
    }
    public void DecrementScore()
    {

        score += pointsWrong;
        UpdateScore();
        print("Wrong!");
    }

    void UpdateScore()
    {
        // update score
        scoreDisplay.text = "Score: " + score;

    }

    // Update is called once per frame

}
