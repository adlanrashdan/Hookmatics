using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static ObstacleManager instance;
    public GameObject[] MissileSpawns;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            print("There is already an instance of ObstacleManager");
        }
    }
    void Start()
    {

    }
    public void SpawnObstacles(int amount)
    {

        SpawnMissiles(amount);
        SpawnWalls(amount);
    }
    public void SpawnMissiles(int amount)
    {
        InvokeRepeating("SpawnMissile", 4, 3 + 7 / amount);
        //Invoke("StopSpawning", 20f);
    }
    void StopSpawning()
    {
        CancelInvoke("SpawnMissile");
        CancelInvoke("SpawnWall");
    }
    void SpawnMissile()
    {
        GameObject projectile = Instantiate(Resources.Load("Prefabs/Missile")) as GameObject;
        //spawn the missile at a random spawn point
        Transform spawnPoint = MissileSpawns[Random.Range(0, MissileSpawns.Length)].transform;
        projectile.transform.position = spawnPoint.position;
        projectile.transform.rotation = spawnPoint.rotation;
    }

    public void SpawnWalls(int amount)
    {
        InvokeRepeating("SpawnWall", 3, 3 + 8 / amount);
        //Invoke("StopSpawning", 5f);
    }
    void SpawnWall()
    {
        GameObject projectile = Instantiate(Resources.Load("Prefabs/Wall")) as GameObject;
        projectile.transform.position = transform.GetChild(0).transform.position;
    }

    public void DestroyAllObstacles()
    {
        StopSpawning();
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject obstacle in obstacles)
        {
            Destroy(obstacle);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
