using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    PlayerLevel playerLevel;
    GameManager gameManager;
    [SerializeField] Enemy[] prefabToBeSpawned;
    [SerializeField] GameObject rangedEnemyPrefab, meleeEnemyPrefab;
    [SerializeField] int levelOfChange;
    [SerializeField] float spawnRate;
    [SerializeField] int burstsStart;
    int bursts = 0;
    [SerializeField] MobPool pool;
    private void Start()
    {
        playerLevel = PlayerLevel.Instance;
        gameManager = GameManager.Instance;
        // prefabToBeSpawned = spawnables1;
        bursts = burstsStart;
        InvokeRepeating("SpawnEnemy", 1f, spawnRate);
    }
    private void Update()
    {
        if ((Time.timeSinceLevelLoad != gameManager.TimeRemaining / 3) || (Time.timeSinceLevelLoad != gameManager.TimeRemaining * 2 / 3)) return;
        bursts = bursts * 2;
    }
    void SpawnEnemy()
    {
        for (int i = 0; i < bursts; i++)
        {
            //Instantiate(prefabToBeSpawned, transform.position, Quaternion.identity);
            GameObject mob;
            // mob.SetActive(false);
            Enemy mobData = prefabToBeSpawned[Random.Range(0, prefabToBeSpawned.Length)];
            if (mobData.GetType() == typeof(RangedEnemy))
            {
                mob = Instantiate(rangedEnemyPrefab, transform.position, Quaternion.identity);

            }
            else
            {
                mob = Instantiate(meleeEnemyPrefab, transform.position, Quaternion.identity);
            }
            mob.GetComponent<EnemyBehaviour>().SetMyData(mobData);
            // mob.SetActive(true);
            //GameObject mob = pool.GetMob(prefabToBeSpawned[Random.Range(0, prefabToBeSpawned.Length)]);
            mob.transform.position = transform.position;
            mob.transform.rotation = Quaternion.identity;
            mob.SetActive(true);
        }
    }

}
