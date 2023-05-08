using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleShooter : MonoBehaviour
{
    [SerializeField] int numberOfColumns;
    [SerializeField] float speed;
    [SerializeField] Sprite bulletSprite;
    [SerializeField] Color bulletColor;
    [SerializeField] float lifeTime;
    [SerializeField] float size;
    [SerializeField] float fireRate;
    [SerializeField] float arc = 360;
    float angle;
    [SerializeField] Material bulletMaterial;
    // public GameObject objectToCollide;
    ParticleSystem system;
    [SerializeField] LayerMask targetMask;
    [SerializeField] ParticleSystem bloodEmitter;

    public Action<GameObject> desiredAction;
    private void Start()
    {
        //Init();
    }
    void Init()
    {
        ResetShooter();
        angle = arc / numberOfColumns;
        for (int i = 0; i < numberOfColumns; i++)
        {
            // A simple particle material with no texture.
            Material particleMaterial = bulletMaterial;
            // Create a green Particle System.
            var go = new GameObject("Particle System");
            go.transform.Rotate(angle * i, 90, 0); // Rotate so the system emits upwards.
            go.transform.parent = transform;
            go.transform.position = transform.position;
            var blood = Instantiate(bloodEmitter, go.transform.position, Quaternion.identity);
            blood.transform.SetParent(go.transform);
            go.AddComponent<ParticleCollision>();
            system = go.AddComponent<ParticleSystem>();
            go.GetComponent<ParticleSystemRenderer>().material = particleMaterial;
            var mainModule = system.main;
            /* mainModule.startColor = bulletColor;
             mainModule.startSize = size;*/
            mainModule.playOnAwake = false;
            //mainModule.loop = false;
            mainModule.startSpeed = speed;
            mainModule.simulationSpace = ParticleSystemSimulationSpace.World;
            // mainModule.duration = fireRate;
            mainModule.startColor = bulletColor;
            mainModule.startSize = size;
            mainModule.startLifetime = lifeTime;

            var emission = system.emission;
            //emission.enabled= false;
            emission.enabled = true;
            emission.rateOverTime = fireRate;

            var form = system.shape;
            form.enabled = true;
            form.shapeType = ParticleSystemShapeType.Sprite;
            form.sprite = null;

            var texture = system.textureSheetAnimation;
            texture.enabled = true;
            texture.mode = ParticleSystemAnimationMode.Sprites;
            texture.AddSprite(bulletSprite);

            var collision = system.collision;
            collision.enabled = true;
            collision.type = ParticleSystemCollisionType.World;
            collision.mode = ParticleSystemCollisionMode.Collision2D;
            //collision.

            collision.lifetimeLoss = 1;
            collision.bounce = 0;
            collision.sendCollisionMessages = true;
            collision.collidesWith = targetMask;

            var subEmitters = system.subEmitters;
            subEmitters.enabled = true;
            subEmitters.AddSubEmitter(blood, ParticleSystemSubEmitterType.Collision, ParticleSystemSubEmitterProperties.InheritNothing, 1);
        }
        // Every 2 secs we will emit.
        //InvokeRepeating("DoEmit", 0f, fireRate);
    }


    public void Shoot()
    {
        foreach (Transform t in transform)
        {
            var lowerSystem = t.GetComponent<ParticleSystem>();
            lowerSystem.Play();
        }
    }

    void ResetShooter()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
    }

    public void Stop()
    {
        foreach (Transform t in transform)
        {
            var lowerSystem = t.GetComponent<ParticleSystem>();
            lowerSystem.Stop();
        }
    }

    public void SetSettings(Weapon weapon)
    {
        numberOfColumns = weapon.numberOfColumns;
        speed = weapon.speed;
        bulletSprite = weapon.bulletSprite;
        bulletColor = weapon.bulletColor;
        bulletMaterial = weapon.bulletMaterial;
        arc = weapon.arc;
        lifeTime = weapon.lifeTime;
        size = weapon.size;
        fireRate = weapon.fireRate;
        Init();
    }

    public void OnDamageInflicted(GameObject gameObject)
    {
        desiredAction(gameObject);
    }



}
