using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletFury;
using BulletFury.Data;

public class RangedEnemyBehaviour : EnemyBehaviour
{
    [SerializeField] RangedEnemy enemyData;
    [SerializeField] ParticleShooter shooter;
    protected new RangedEnemy myEnemyData { get => (RangedEnemy)base.myEnemyData; set => base.myEnemyData = value; }
    //BulletManager bulletManager;
    Weapon enemyWeapon;
    enum State { Reaching, Shooting }
    State enemyState;
    float stopDist;
    GameManager manager;

    protected override void Start()
    {
        manager = GameManager.Instance;
        myEnemyData = enemyData;
        base.Start();
        enemyWeapon = myEnemyData.enemyWeapon;
        stopDist = myEnemyData.stopDist;
        /*bulletManager = manager.GlobalManager;
        bulletManager.SetBulletSettings(enemyWeapon.bulletSettings);
        bulletManager.SetSpawnSettings(enemyWeapon.spawnSettings);*/
        shooter.SetSettings(enemyWeapon);
        damage = enemyWeapon.bulletDamage;
        shooter.desiredAction = (GameObject gameObject)=>
        {
            if(gameObject.TryGetComponent<PlayerLevel>(out PlayerLevel level))
            {
               Debug.Log("hit player");
               //enemy.GetDamage(activeWeapon.bulletDamage);
               level.LoseXP(damage);
            }
        };
        enemyState = State.Reaching;
    }
    protected override void AttackBehaviour()
    {
        float playerDist = Vector2.Distance(transform.position, player.transform.position);
        switch (enemyState)
        {
            case State.Reaching:
                if (playerDist <= stopDist)
                {
                    shooter.Shoot();
                    enemyState = State.Shooting;
                }
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
                break;
            case State.Shooting:
                if (playerDist > stopDist)
                {
                    shooter.Stop();
                    enemyState = State.Reaching;
                }
               // bulletManager.Spawn(transform.position, transform.up);
                break;
        }
    }
}
