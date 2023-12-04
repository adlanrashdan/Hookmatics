using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public static Boss instance;
    public int maxHealth = 100;
    public int currentHealth;
    private int previousScore = 0;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (ScoreManager.instance.score > previousScore)
        {
            TakeDamage(10);
            previousScore = ScoreManager.instance.score;
            if (currentHealth <= 0)
            {
                GameObject effect = Instantiate(Resources.Load<GameObject>("Prefabs/FX/CFXR2 Broken Heart"), transform.position, Quaternion.identity);
                effect.transform.localScale = new Vector3(2, 2, 2);
                SoundEffectManager.instance.PlaySoundEffect("BossComplete");
                Instantiate(Resources.Load<GameObject>("Prefabs/FX/CFXR2 WW Explosion"), transform.position, Quaternion.identity);
                GetComponent<Animator>().Play("die");
                Invoke("Win", 2f);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Instantiate(Resources.Load<GameObject>("Prefabs/FX/CFXR2 Broken Heart"), transform.position, Quaternion.identity);
        healthBar.SetHealth(currentHealth);

    }
    void Win()
    {
        GameManager.instance.BossWin();
    }
}
