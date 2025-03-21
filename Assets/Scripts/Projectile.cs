using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    void Start()
    {
        
    }

    private void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        transform.position += (transform.forward * 0.01f);
    }

    private void OnCollisionEnter(Collision other)
    {
        ProjectilePool.Instance.ReturnProjectile(this.gameObject);
    }
}
