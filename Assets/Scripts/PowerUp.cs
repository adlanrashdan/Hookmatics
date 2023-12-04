using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    PowerUpManager.PowerUpType type;
    // Start is called before the first frame update
    void Start()
    {
        //pick a random powerup type
        int randomIndex = Random.Range(0, 2);
        type = (PowerUpManager.PowerUpType)randomIndex;

        // set the sprite based on the type
        gameObject.GetComponentInChildren<MeshRenderer>().material = Resources.Load<Material>("Materials/" + type.ToString());
        Destroy(gameObject, 15f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision other)
    {
        // if the player collides with the powerup
        if (other.gameObject.CompareTag("Player"))
        {
            // apply the powerup
            ApplyPowerUp();
            // destroy the powerup
            Destroy(gameObject);
        }
    }
    void ApplyPowerUp()
    {
        // apply the powerup
        switch (type)
        {
            case PowerUpManager.PowerUpType.Shield:
                // apply shield
                ApplyShield();
                break;
            case PowerUpManager.PowerUpType.Freeze:
                // apply freeze
                ApplyFreeze();
                break;
            case PowerUpManager.PowerUpType.TimeFreeze:
                // apply time freeze
                ApplyTimeFreeze();
                break;
            default:
                break;
        }
    }
    void ApplyShield()
    {
        // apply shield
        Player.instance.AddSheild();
        //Play sound
        SoundEffectManager.instance.PlaySoundEffect("ShieldPop");

    }
    void ApplyFreeze()
    {
        // apply freeze
        // find all missiles and freeze them
        GameObject[] missiles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject missile in missiles)
        {
            if (missile.TryGetComponent<Missile>(out Missile m))
                missile.GetComponent<Missile>().Freeze();
        }
        Instantiate(Resources.Load("Prefabs/FX/CFXR3 Hit Ice B (Air)"), Player.instance.transform.position, Quaternion.Euler(90, 0, 0));
        //Play sound
        SoundEffectManager.instance.PlaySoundEffect("Freeze");

    }
    void ApplyTimeFreeze()
    {
        TimeManager.instance.TimeFreezeEvent();
        // apply time freeze
        Instantiate(Resources.Load("Prefabs/FX/CFXR3 Hit Ice B (Air)"), Player.instance.transform.position, Quaternion.Euler(90, 0, 0));
        SoundEffectManager.instance.PlaySoundEffect("Freeze");
    }
}
