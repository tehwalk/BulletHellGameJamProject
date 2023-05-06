using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    ParticleShooter shooter;
    private void Start() {
        shooter = transform.parent.GetComponent<ParticleShooter>();
    }
    private void OnParticleCollision(GameObject other)
    {
       // Debug.Log("hit");
       shooter.OnDamageInflicted(other);
    }


}
