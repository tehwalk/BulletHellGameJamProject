using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletFury;
using BulletFury.Data;

public class RangedEnemyBehaviour : EnemyBehaviour
{
    [SerializeField] RangedEnemy enemyData;
    [SerializeField] ParticleShooter shooter;
    [SerializeField] float rotSpeed;
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
        shooter.desiredAction = (GameObject gameObject) =>
        {
            if (gameObject.TryGetComponent<PlayerLevel>(out PlayerLevel level))
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
        // transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y));
        Vector2 vectorToTarget = player.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotSpeed);
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
                // transform.Rotate(Vector3.RotateTowards(transform.position, player.transform.position, rotSpeed*Time.deltaTime,180f));

                // bulletManager.Spawn(transform.position, transform.up);
                break;
        }
    }
}
