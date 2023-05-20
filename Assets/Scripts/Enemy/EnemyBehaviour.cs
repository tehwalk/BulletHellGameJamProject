using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using BulletFury;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBehaviour : MonoBehaviour
{
    protected virtual Enemy myEnemyData { get; set; }
    protected int lifePointsMax, lifePoints;
    protected int damage;
    public int Damage { get { return damage; } }
    protected float moveSpeed;
    protected GameObject player;
    protected NavMeshAgent agent;
    //public GameObject xpPrefab;
    // Start is called before the first frame update
    protected virtual void Init()
    {
        damage = myEnemyData.damage;
        lifePointsMax = myEnemyData.lifePointsMax;
        lifePoints = lifePointsMax;
        moveSpeed = myEnemyData.moveSpeed;
        GameObject gfx = Instantiate(myEnemyData.enemyGFX, transform.position, Quaternion.identity);
        gfx.transform.SetParent(transform);
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;
        agent.speed = moveSpeed;
    }

    public virtual void SetMyData(Enemy data)
    {
        myEnemyData = data;
        Init();
    }
    void Update()
    {
        if (myEnemyData == null) return;
        AttackBehaviour();
    }

    protected virtual void AttackBehaviour() { }


    public void GetDamage(int dmg)
    {
        StartCoroutine(Flash());
        lifePoints -= dmg;
        if (lifePoints <= 0) Die();
    }

    void Die()
    {
        // for (int i = 0; i < myEnemyData.spawnables.Length; i++)
        Instantiate(myEnemyData.spawnables[Random.Range(0, myEnemyData.spawnables.Length)], transform.position, Quaternion.identity);
        //Destroy(gameObject);
        SoundEffectManager.Instance.PlayDeathSound();
        gameObject.SetActive(false);
    }

    IEnumerator Flash()
    {
        GetComponentInChildren<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.2f);
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }


}
