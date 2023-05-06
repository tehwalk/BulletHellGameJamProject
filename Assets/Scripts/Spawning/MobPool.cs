using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobPool : MonoBehaviour
{
    // [SerializeField] GameObject mobPrefab;
    bool notEnoughMobsInPool = true;
    List<GameObject> mobs;
    // Start is called before the first frame update
    void Start()
    {
        mobs = new List<GameObject>();
    }

    public GameObject GetMob(GameObject mobPrefab)
    {
        if (mobs.Count > 0)
        {
            foreach (var mob in mobs)
            {
                if (!mob.activeInHierarchy) return mob;
            }
        }

        if (notEnoughMobsInPool == true)
        {
            GameObject mob = Instantiate(mobPrefab);
            mob.SetActive(false);
            mobs.Add(mob);
            return mob;
        }

        return null;
    }
}
