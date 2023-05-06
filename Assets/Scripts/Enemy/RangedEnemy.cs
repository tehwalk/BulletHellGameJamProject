using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletFury.Data;

[CreateAssetMenu(menuName = "Ranged Enemy", fileName = "New RangedEnemy")]
public class RangedEnemy : Enemy
{
    public float stopDist;
    public Weapon enemyWeapon;
}
