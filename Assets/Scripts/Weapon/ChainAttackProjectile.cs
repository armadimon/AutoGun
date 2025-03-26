using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainAttackProjectile : MonoBehaviour
{
    public float bulletSpeed = 5f;
    public float damage = 10;
    public float chainRange = 10f;
    public int maxChains = 3; 
    public LayerMask layerMask;

    private int chainCount = 0;            // 진행된 체인 횟수
    private bool isChaining = false;

    private void Update()
    {
        transform.position += transform.forward * (Time.deltaTime * bulletSpeed);
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & layerMask.value) != 0)
        {

            isChaining = ChainAttack(other);
            ApplyDamage(other.gameObject);
            if (!isChaining)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                ProjectileManager.Instance.chainProjectilePool.ReturnProjectile(this.gameObject);
                chainCount = 0;
                isChaining = false;
                return;
            }
            chainCount++;
        }
    }

    private void ApplyDamage(GameObject target)
    {
        var enemy = target.GetComponent<BaseController>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }

    private bool ChainAttack(Collider other)
    {
            Vector3 currentPosition = other.gameObject.transform.position;
            Collider[] colliders = Physics.OverlapSphere(currentPosition, chainRange, layerMask);
            GameObject nextTarget = null;
            float closestDistance = Mathf.Infinity;
            
            foreach (Collider col in colliders)
            {
                if (col.gameObject == other.gameObject)
                    continue;

                float distance = Vector3.Distance(currentPosition, col.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    nextTarget = col.gameObject;
                }
            }

            Debug.Log(nextTarget);
            // 새로운 타겟이 없으면 체인 종료
            if (nextTarget == null || chainCount >= maxChains)
                return false;

            // 가장 가까운 타겟으로 방향 전환
            Vector3 targetPosition = new Vector3(nextTarget.transform.position.x, transform.position.y, nextTarget.transform.position.z);
            transform.LookAt(targetPosition);
            return true;
    }
}