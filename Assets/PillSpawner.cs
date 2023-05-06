using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillSpawner : MonoBehaviour
{
    [SerializeField] GameObject spawnPrefab;
    [SerializeField] float spawnRate;
    bool hasItem = false;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 0f, spawnRate);
    }

    void Spawn()
    {
        if (hasItem == true) return;
        Instantiate(spawnPrefab, transform.position, Quaternion.identity);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<WeaponHolder>(out WeaponHolder holder))
        {
            Debug.Log("i have item triggrer");
            hasItem = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<WeaponHolder>(out WeaponHolder holder))
        {
            Debug.Log("item taken trigger");
            hasItem = false;
            CancelInvoke();
            InvokeRepeating("Spawn", spawnRate, spawnRate);
        }
    }


}
