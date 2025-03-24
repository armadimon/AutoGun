using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : MonoBehaviour
{
    public float speed = 2f;
    private int damage;
    private float explosionRadius;

    public void SetDamage(int dmg, float radius)
    {
        damage = dmg;
        explosionRadius = radius;
    }

    void Update()
    {
        transform.position += transform.forward * (Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<ResourceController>()?.ChangeHealth(damage);
            }
        }
        Destroy(gameObject);
    }
}