using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using BulletFury.Data;

[CreateAssetMenu(menuName = "Custom Items/Weapon", fileName = "New Weapon")]
public class Weapon : ScriptableObject
{
    public string weaponName;
    public int bulletDamage;
    //public BulletSettings bulletSettings;
    //public SpawnSettings spawnSettings;
    public int numberOfColumns;
    public float speed;
    public Sprite bulletSprite;
    public Color bulletColor;
    public float lifeTime;
    public float size;
    public float fireRate;
    [Range(0,360)]
    public float arc;
    public Material bulletMaterial;
}
