using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forge_Cauldron : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hook"))
        {
            LevelManager.instance.NextLevel();
            Instantiate(Resources.Load("Prefabs/FX/CauldronReset"), transform.position, Quaternion.identity);
            SoundEffectManager.instance.PlaySoundEffect("CauldronEffect");
            Destroy(other.gameObject);
        }
    }
}
