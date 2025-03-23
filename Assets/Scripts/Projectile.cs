using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float bulletSpeed = 0.2f;
    void Start()
    {
        bulletSpeed = 0.2f;
    }

    private void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        transform.position += (transform.forward * bulletSpeed);
    }

    private void OnCollisionEnter(Collision other)
    {
        other.gameObject.GetComponent<ResourceController>()?.ChangeHealth(-10);
        ProjectilePool.Instance.ReturnProjectile(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<ResourceController>()?.ChangeHealth(-10);
        ProjectilePool.Instance.ReturnProjectile(this.gameObject);
    }
}
