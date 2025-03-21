using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public static ProjectileManager Instance;
    public Player player;

    private void Awake()
    {
        Instance = this;
    }
    
    public void FireBullet(Transform firePoint, Vector3 bulletRotation)
    {
        
        GameObject projectile = ProjectilePool.Instance.GetProjectile(firePoint, bulletRotation);
    }
}
