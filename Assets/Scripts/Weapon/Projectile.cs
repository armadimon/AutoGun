using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float bulletSpeed = 2f;
    public float damage = 10f;

    private void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        transform.position += transform.forward * (Time.deltaTime * bulletSpeed);
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<BaseController>()?.TakeDamage(damage);
        ProjectileManager.Instance.projectilePool.ReturnProjectile(this.gameObject);
    }
}
