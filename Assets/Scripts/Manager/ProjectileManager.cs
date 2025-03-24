using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public static ProjectileManager Instance;
    public ProjectilePool projectilePool;
    public ProjectilePool chainProjectilePool;
    public Player player;

    private void Awake()
    {
        Instance = this;
    }
    
    public void FireBullet(Transform firePoint, Vector3 bulletRotation)
    {
        
        GameObject projectile = projectilePool.GetProjectile(firePoint, bulletRotation);
    }
    
    public void FireChainBullet(Transform firePoint, Vector3 bulletRotation)
    {
        GameObject chainProjectile = chainProjectilePool.GetProjectile(firePoint, bulletRotation);
    }
}
