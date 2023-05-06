using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletFury;


public class EnemyBehaviour : MonoBehaviour
{
    protected virtual Enemy myEnemyData { get; set; }
    protected int lifePointsMax, lifePoints;
    protected int damage;
    public int Damage { get { return damage; } }
    protected float moveSpeed;
    protected GameObject player;
    //public GameObject xpPrefab;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        damage = myEnemyData.damage;
        lifePointsMax = myEnemyData.lifePointsMax;
        lifePoints = lifePointsMax;
        moveSpeed = myEnemyData.moveSpeed;
        GameObject gfx = Instantiate(myEnemyData.enemyGFX, transform.position, Quaternion.identity);
        gfx.transform.SetParent(transform);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        AttackBehaviour();
    }

    protected virtual void AttackBehaviour() { }

    /*public void HitByPlayer()
    {
        GetDamage((int)player.GetComponent<BulletManager>().GetBulletSettings().Damage);
    }*/
    public void GetDamage(int dmg)
    {
        lifePoints -= dmg;
        if (lifePoints <= 0) Die();
    }

    void Die()
    {
        for (int i = 0; i < myEnemyData.spawnables.Length; i++)
            Instantiate(myEnemyData.spawnables[Random.Range(0, myEnemyData.spawnables.Length)], transform.position, Quaternion.identity);
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }


}
