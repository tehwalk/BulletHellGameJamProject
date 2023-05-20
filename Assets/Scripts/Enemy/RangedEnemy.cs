using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using BulletFury.Data;

[CreateAssetMenu(menuName = "Custom Items/Ranged Enemy", fileName = "New RangedEnemy")]
public class RangedEnemy : Enemy
{
    public float stopDist;
    public Weapon enemyWeapon;
}
