using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    [SerializeField] float timestamp;
    [SerializeField] AudioSource first;
    [SerializeField] AudioSource loop;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlaySequence());
    }

    IEnumerator PlaySequence()
    {
       first.Play();
       yield return new WaitForSecondsRealtime(timestamp);
       first.Stop();
       loop.Play();
    }

    
}
