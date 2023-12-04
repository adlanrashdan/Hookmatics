using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class HookableObject : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        SoundEffectManager.instance.PlaySoundEffect("Spawn");
        Instantiate(Resources.Load("Prefabs/FX/CFX3_Hit_Light_C_Air"), transform.position, Quaternion.identity);
        //rotate the object so the the y-axis looks at the camera
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.position - transform.position, Vector3.up);

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        // if the hook hits the hookable object, it is becomes a child of the hookable object
        if (other.gameObject.tag == "Hook" && other.gameObject.transform.childCount < 3)
        {
            gameObject.transform.parent = other.gameObject.transform;
            transform.localPosition = Vector3.zero;
            other.gameObject.GetComponent<Hook>().isReturning = true;
            print("Hooked");
        }
    }
    private void OnDestroy()
    {

        HookablesManager.instance.hookables.Remove(gameObject);
        if (GameManager.instance.mode == GameManager.Mode.Forge)
        {
            SoundEffectManager.instance.PlaySoundEffect("MuchSuccess");
            Instantiate(Resources.Load("Prefabs/FX/CFXR3 Hit Ice B (Air)"), transform.position, Quaternion.identity);
        }
        else
        {
            if (this.GetComponent<ScoreObject>().isCorrect)
            {


                LevelManager.instance.CheckNextLevel();
                SoundEffectManager.instance.PlaySoundEffect("HappySuccess");
                Instantiate(Resources.Load("Prefabs/FX/CFXR4 Firework HDR Shoot Single (Random Color)"), transform.position, Quaternion.identity);
                Instantiate(Resources.Load("Prefabs/FX/CFXR4 Firework HDR Shoot Single (Random Color)"), transform.position, Quaternion.identity);
                Instantiate(Resources.Load("Prefabs/FX/CFXR4 Firework HDR Shoot Single (Random Color)"), transform.position, Quaternion.identity);
                Instantiate(Resources.Load("Prefabs/FX/CFX_Hit_C White"), transform.position, Quaternion.identity);
            }
            else
            {
                SoundEffectManager.instance.PlaySoundEffect("Wrong");

                Instantiate(Resources.Load("Prefabs/FX/CFXR2 WW Enemy Explosion"), transform.position, Quaternion.Euler(-90, 0, 0));
            }
        }


    }

}
