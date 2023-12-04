using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject hook;
    public static Player instance;
    private Vector3 playerSpawnPoint;
    public bool hasShield;


    private void Awake()
    {
        instance = this;
        playerSpawnPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
    void Start()
    {
        //save the player spawn point

        Instantiate(Resources.Load("Prefabs/FX/CFX3_MagicAura_D_Runic"), transform.position, Quaternion.Euler(90, 0, 0));
    }

    // Update is called once per frame



    public void ResetPlayer()
    {
        //reset the player to the spawn point
        transform.position = playerSpawnPoint;
        transform.GetComponent<PlayerMovement>().BlockMovement();
    }
    public void AddSheild()
    {
        hasShield = true;
        Invoke("RemoveShield", 5f);
        transform.Find("Shield").gameObject.SetActive(true);
    }
    public void RemoveShield()
    {
        hasShield = false;
        transform.Find("Shield").gameObject.SetActive(false);
    }
}
