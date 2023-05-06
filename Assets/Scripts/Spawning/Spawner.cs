using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletFury;

public class Spawner : MonoBehaviour
{
    PlayerLevel playerLevel;
    [SerializeField] GameObject prefabToBeSpawned;
    [SerializeField] float spawnRate;
    [SerializeField] int burstsStart;
    int bursts = 0;
    [SerializeField] MobPool pool;
    private void Start()
    {
        playerLevel = PlayerLevel.Instance;
        bursts = burstsStart;
        InvokeRepeating("SpawnEnemy", 1f, spawnRate);
    }
    private void Update() {
        bursts = burstsStart * 2;
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
