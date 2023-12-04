using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreObject : MonoBehaviour
{
    public bool isCorrect = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || (gameObject.name == "Missile" && other.gameObject.tag == "Hook"))
        {
            if (GameManager.instance.mode == GameManager.Mode.Forge)
            {
                if (gameObject.tag == "Obstacle")
                    ScoreManager.instance.DecrementScore();
                else
                    MathManager.instance.AddToEquation(gameObject.GetComponentInChildren<TextMeshPro>().text);
            }
            else
            {

                if (isCorrect)
                    ScoreManager.instance.IncrementScore();
                else
                {
                    if (Player.instance.hasShield)
                        return;
                    ScoreManager.instance.DecrementScore();
                }
                SpawnScoreEffect();
            }

            Destroy(gameObject);
        }
    }
    public void SpawnScoreEffect()
    {
        GameObject scoreEffect = Instantiate(Resources.Load("Prefabs/ScoreEffect"), transform.position, Quaternion.identity) as GameObject;
        scoreEffect.transform.GetChild(0).GetComponent<TextMeshPro>().text = isCorrect ? "+" + ScoreManager.instance.pointsCorrect.ToString() : ScoreManager.instance.pointsWrong.ToString();
        Destroy(scoreEffect, 0.85f);
    }
}
