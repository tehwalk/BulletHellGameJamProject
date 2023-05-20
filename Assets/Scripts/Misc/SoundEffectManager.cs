using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    private static SoundEffectManager instance;
    public static SoundEffectManager Instance { get { return instance; } }
    public AudioSource deathSound, pickupSound, fireSound;

    private void Awake()
    {
        if (instance != null && instance != this) instance = null;
        instance = this;
    }

    public void PlayDeathSound()
    {
        deathSound.Play();
    }
    public void PlayPickupSound()
    {
        pickupSound.Play();
    }
    public void PlayFireSound()
    {
        fireSound.Play();
    }

}
