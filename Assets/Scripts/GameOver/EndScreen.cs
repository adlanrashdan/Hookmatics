using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    public GameObject scoreText;
    public GameObject levelText;
    private int scoreCounter;
    private bool stopCount = false;
    // Start is called before the first frame update
    void Start()
    {
        levelText.GetComponent<TextMeshProUGUI>().text = "Level Completed: " + LevelManager.instance.currentLevel;
        scoreText.GetComponent<TextMeshProUGUI>().text = "Your Score: " + ScoreManager.instance.score;
    }

    // Update is called once per frame
    void Update()
    {
        if(stopCount){
            return;
        }
        //count every second frame to make the score count up slower
        if (Time.frameCount % 2 == 0)
        {
            if (scoreCounter < ScoreManager.instance.score)
            {
                scoreCounter++;
                scoreText.GetComponent<TextMeshProUGUI>().text = "Your Score: " + scoreCounter;
                
            } else {
                scoreText.GetComponent<Animator>().Play("TextPulse");
                stopCount = true;
            }
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("Menu");
    }
}
