using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainAttackProjectile : MonoBehaviour
{
    public float bulletSpeed = 5f;         // 탄환 이동 속도 (이동 애니메이션에 사용)
    public float damage = 10;                // 기본 데미지
    public float chainRange = 10f;         // 체인 어택 범위
    public int maxChains = 3;              // 최대 체인 횟수
    public LayerMask layerMask;            // 적 레이어 마스크

    private int chainCount = 0;            // 진행된 체인 횟수
    private bool isChaining = false;       // 체인 공격 진행 여부

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
        // 적 레이어인지 체크
        if (((1 << other.gameObject.layer) & layerMask.value) != 0)
        {

            isChaining = ChainAttack(other);
            ApplyDamage(other.gameObject);
            // 체인 공격이 이미 진행 중이면 무시 (한 번만 시작)
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

    // 지정된 타겟에게 데미지를 적용하는 함수
    private void ApplyDamage(GameObject target)
    {
        var enemy = target.GetComponent<BaseController>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }

    // 체인 어택을 진행하는 코루틴
    private bool ChainAttack(Collider other)
    {
            // 현재 위치를 기준으로 체인 범위 내의 적 검색
            Vector3 currentPosition = other.gameObject.transform.position;
            Collider[] colliders = Physics.OverlapSphere(currentPosition, chainRange, layerMask);
            GameObject nextTarget = null;
            float closestDistance = Mathf.Infinity;

            // 가장 가까운 타겟을 찾는다 (첫 번째 타겟만 제외)
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