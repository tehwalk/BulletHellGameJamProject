using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }
    AudioSource audioSource;
    [SerializeField] AudioClip battleClip;
    [SerializeField] AudioClip lostClip;
    [SerializeField] float audioDelay;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null && instance != this) instance = null;
        instance = this;
    }
    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBattleTrack()
    {
       audioSource.clip = battleClip;
       audioSource.loop = true;
       audioSource.PlayDelayed(audioDelay);
    }

    public void PlayLostTrack()
    {
        audioSource.clip = lostClip;
        audioSource.loop = false;
        audioSource.PlayDelayed(audioDelay);
    }
}
