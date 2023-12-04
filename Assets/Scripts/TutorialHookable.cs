using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TutorialHookable : MonoBehaviour
{
    // Start is called before the first frame update
    //it triggers an event when it is destroyed
    // it lsitens to the event 
    public GameObject[] hookables;
    void Start()
    {
        hookables = GameObject.FindGameObjectsWithTag("Hookable");

    }

    // Update is called once per frame
    void Update()
    {
        //if all the hookables are destroyed, spawn end the Scene
        // find all hookable by tag
        hookables = GameObject.FindGameObjectsWithTag("Hookable");


    }
    void OnDestroy()
    {
        //play the explosion effect
        Instantiate(Resources.Load("Prefabs/FX/CFX_Explosion_B_Smoke+Text"), transform.position, Quaternion.identity);
        //play the explosion sound
        SoundEffectManager.instance.PlaySoundEffect("MuchSuccess");
        if (hookables.Length == 1)
        {
            //spawn the end of the scene
            //spawn the end of the scene
            print("end of scene");
            GameManager.instance.EndGame();
        }

    }
}
