using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this is a singleton
// this class is responsible for spawning powerups and keeping track of them
public class PowerUpManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int spawnChance = 500;
    public static PowerUpManager instance;
    public enum PowerUpType
    {
        Shield,
        Freeze,
        TimeFreeze,
    }
    void Start()
    {

    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            print("There is already an instance of PowerUpManager");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if the timer is running, spawn powerups
        // if the timer is not running, stop spawning powerups
        if (TimeManager.instance.timerRunning)
        {
            // there is a change that a powerup will spawn
            // 1 in 100 chance
            int randomChance = Random.Range(0, spawnChance);
            if (randomChance == 0)
            {
                SpawnPowerUp();
            }

        }
    }

    void SpawnPowerUp()
    {
        // spawn powerup
        // in a random location spawn the powerup close to this transform

        Instantiate(Resources.Load("Prefabs/PowerUp"), new Vector3(transform.position.x + Random.Range(-2, 2), transform.position.y, transform.position.z + Random.Range(-2, 2)), Quaternion.identity);

    }
}
