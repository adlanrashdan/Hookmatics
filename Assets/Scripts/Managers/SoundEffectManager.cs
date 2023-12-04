using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public AudioClip[] soundEffects;
    public static SoundEffectManager instance;
    public AudioSource audioSource;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else {
            print("There is already an instance of SoundEffectManager");
        }
    }
    void Start()
    {
        
    }

    public void PlaySoundEffect(string name)
    {
        for (int i = 0; i < soundEffects.Length; i++)
        {
            if (soundEffects[i].name == name)
            {
                audioSource.GetComponent<AudioSource>().PlayOneShot(soundEffects[i]);
                return;
            }
        }
        Debug.LogWarning("Sound effect not found: " + name);
    }
}