using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom Items/Enemy", fileName = "New Enemy")]
public class Enemy : ScriptableObject
{
    public string enemyName;
    public int lifePointsMax;
    public int damage;
    public float moveSpeed;
    public GameObject enemyGFX;
    public GameObject[] spawnables;
}
