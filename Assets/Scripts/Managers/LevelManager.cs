using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static LevelManager instance;
    public TextMeshPro levelDisplay;

    public int currentLevel = 0;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            print("There is already an instance of LevelManager");
        }
    }
    private void Start()
    {
        NextLevel();
    }
    public void LoadLevel(int level)
    {
        if (GameManager.instance.mode == GameManager.Mode.Forge)
        {
            Forge_HookablesManager.instance.DestroyAllHookables();
            MathManager.instance.ClearEquation();
            print("FORGE");
        }
        else
        {
            HookablesManager.instance.DestroyAllHookables();
        }
        ObstacleManager.instance.DestroyAllObstacles();
        MathManager.instance.GenerateMath();
        if (GameManager.instance.mode == GameManager.Mode.Forge)
        {
            Forge_HookablesManager.instance.SpawnHookables();
        }
        else
        {
            HookablesManager.instance.SpawnHookables();
        }
        ObstacleManager.instance.SpawnObstacles(level);

        levelDisplay.text = "Level " + level;
        Player.instance.ResetPlayer();
    }
    public void NextLevel()
    {
        currentLevel++;
        TimeManager.instance.StopTimer();
        Invoke("StartTimer", 4f);
        LoadLevel(currentLevel);
        print("Level " + currentLevel);
    }
    public void CheckNextLevel()
    {
        bool foundHookable = false;
        if (GameManager.instance.mode == GameManager.Mode.Forge)
        {
            NextLevel();
        }
        else
        {
            foreach (GameObject hookable in HookablesManager.instance.hookables)
            {
                if (hookable.GetComponent<ScoreObject>().isCorrect)
                {
                    foundHookable = true;
                }
            }
            if (!foundHookable)
            {
                NextLevel();
            }
        }
    }
    private void StartTimer()
    {
        TimeManager.instance.StartTimer();
    }
}
