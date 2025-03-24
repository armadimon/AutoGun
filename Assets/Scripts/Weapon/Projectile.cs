using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float bulletSpeed = 2f;

    private void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        transform.position += transform.forward * (Time.deltaTime * bulletSpeed);
    }

    // private void OnCollisionEnter(Collision other)
    // {
    //     other.gameObject.GetComponent<ResourceController>()?.ChangeHealth(-10);
    //     ProjectileManager.Instance.projectilePool.ReturnProjectile(this.gameObject);
    // }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<ResourceController>()?.ChangeHealth(-10);
        ProjectileManager.Instance.projectilePool.ReturnProjectile(this.gameObject);
    }
}
