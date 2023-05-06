using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BulletFury;

public class WeaponActivator : MonoBehaviour
{
    //BulletManager bulletManager;
    [SerializeField] ParticleShooter shooter;
    bool fire = false;
    [SerializeField] float lifeTimeMax;
    [SerializeField] Image img;
    float lifeTime = 0;
    Weapon activeWeapon;
    Vector3 imgOriginalScale;

    // Start is called before the first frame update
    void Start()
    {
        //bulletManager = GetComponent<BulletManager>();
       // shooter = GetComponent<ParticleShooter>();
        DisableWeapon();
        imgOriginalScale = img.rectTransform.localScale;
        shooter.desiredAction = (GameObject gameObject)=>
        {
            if(gameObject.TryGetComponent<EnemyBehaviour>(out EnemyBehaviour enemy))
            {
               enemy.GetDamage(activeWeapon.bulletDamage);
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (fire == true)
        {
           // bulletManager.Spawn(transform.position, transform.up);
            
            lifeTime -= Time.deltaTime;
            img.rectTransform.localScale = new Vector3(ExtentionMethods.MapValueToRange(lifeTime, lifeTimeMax, 0, imgOriginalScale.x, 0), imgOriginalScale.y, imgOriginalScale.z);
            if (lifeTime <= 0)
            {
                DisableWeapon();
            }
        }

    }

    public void ActivateWeapon()
    {
        fire = true;
        lifeTime = lifeTimeMax;
        img.enabled = true;
        shooter.Shoot();
    }

    public void DisableWeapon()
    {
        fire = false;
        img.enabled = false;
        shooter.Stop();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<WeaponHolder>(out WeaponHolder holder))
        {
            activeWeapon = holder.myWeapon;
            shooter.SetSettings(activeWeapon);
           // bulletManager.SetBulletSettings(activeWeapon.bulletSettings);
           // bulletManager.SetSpawnSettings(activeWeapon.spawnSettings);
            ActivateWeapon();
            Destroy(other.gameObject);
        }
    }
}
