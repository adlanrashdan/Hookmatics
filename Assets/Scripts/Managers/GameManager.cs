using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;
    public AudioSource musicSource;
    public Mode mode;
    public enum Mode { Forge, Normal, Boss };
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            print("There is already an instance of GameManager");
        }
    }
    void Start()
    {
        musicSource.Play();
        if (SceneManager.GetActiveScene().name == "NumberForge")
        {
            mode = Mode.Forge;
        }
        else
        {
            mode = Mode.Normal;
        }
    }

    public void EndGame()
    {
        SceneManager.LoadScene("GameOver");
        print("Game Over");
    }

    public void BossRound()
    {
        SceneManager.LoadScene("BossScene");
    }
    public void BossWin()
    {
        SceneManager.LoadScene("BossWin");
    }

    public void NextRound()
    {
        SceneManager.LoadScene("NextRound");
    }


    // Update is called once per frame

}
