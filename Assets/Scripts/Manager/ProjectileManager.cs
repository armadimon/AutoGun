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
        
        GameObject obj = projectilePool.GetProjectile(firePoint, bulletRotation);
        Projectile projectile = obj.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.SetDamage(CharacterManager.Instance.player.currentWeapon.data.GetPowerByLevel());
        }
    }
    
    public void FireChainBullet(Transform firePoint, Vector3 bulletRotation)
    {
        GameObject chainProjectile = chainProjectilePool.GetProjectile(firePoint, bulletRotation);
        GameObject obj = projectilePool.GetProjectile(firePoint, bulletRotation);
        ChainAttackProjectile projectile = obj.GetComponent<ChainAttackProjectile>();
        if (projectile != null)
        {
            projectile.SetDamage(CharacterManager.Instance.player.currentWeapon.data.GetPowerByLevel());
        }
    }
}
