using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyBehaviour : EnemyBehaviour
{
    [SerializeField] Enemy enemyData;
    protected override Enemy myEnemyData { get => base.myEnemyData; set => base.myEnemyData = value; }

    protected override void Init()
    {
       // myEnemyData = enemyData;
        base.Init();
    }
    protected override void AttackBehaviour()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }


    


}
