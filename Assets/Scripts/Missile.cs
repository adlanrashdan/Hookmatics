using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Missile : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10f;
    public float lifetime = 2f;
    private bool isFrozen;
    void Start()
    {
        Destroy(gameObject, lifetime);
        SoundEffectManager.instance.PlaySoundEffect("MissileSpawn");
    }

    // Update is called once per frame
    void Update()
    {
        if (isFrozen)
        {
            return;
        }
        // the missile tries to fly towards the player but not perfectly

        Vector3 direction = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.05f);

        transform.Translate(Vector3.forward * Time.deltaTime * speed);

    }
    private void OnDestroy()
    {
        Instantiate(Resources.Load("Prefabs/FX/CFX_Explosion_B_Smoke+Text"), transform.position, Quaternion.identity);

        SoundEffectManager.instance.PlaySoundEffect("MissileDeath");
    }

    public void Freeze()
    {
        isFrozen = true;
        Invoke("UnFreeze", 4f);
    }
    public void UnFreeze()
    {
        isFrozen = false;
    }
}
