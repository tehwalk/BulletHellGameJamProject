using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletFury;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject prefabToBeSpawned;
    [SerializeField] float spawnRate;
    [SerializeField] int bursts;
    [SerializeField] MobPool pool;
    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 1f, spawnRate);
    }
    void SpawnEnemy()
    {
        for (int i = 0; i < bursts; i++)
        {
            //Instantiate(prefabToBeSpawned, transform.position, Quaternion.identity);
            GameObject mob = pool.GetMob(prefabToBeSpawned);
            mob.transform.position = transform.position;
            mob.transform.rotation = Quaternion.identity;
            mob.SetActive(true);
        }
    }

}
