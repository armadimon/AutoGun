using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

// AI의 상태를 나타내는 열거형
public enum AIState
{
    Idle,       // 대기 상태
    Move,
    Attacking,   // 공격 상태
}

// NPC 클래스: 네비게이션, 배회, 전투 기능을 수행
public class EnemyController : BaseController
{
    
    private float playerDistance;    // 플레이어와의 거리
    private float defaultDetectDistance;            // 기본 감지 거리
    public EnemyDropTable dropTable;
    public event Action OnDeath;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 10f, NavMesh.AllAreas))
        {
            transform.position = (hit.position);
			defaultPos = hit.position;
        }
    }

    void Start()
    {
        defaultDetectDistance = detectDistance;
        SetState(AIState.Idle);
    }

    void Update()
    {
        playerDistance = Vector3.Distance(transform.position, CharacterManager.Instance.player.transform.position);


        switch (aiState)
        {
            case AIState.Idle:
                PassiveUpdate();
                break;
            case AIState.Attacking:
                AttackingUpdate();
                break;
        }
    }
    public void SetState(AIState state)
    {
        aiState = state;

        switch (aiState)
        {
            case AIState.Idle:
                agent.speed = stats.Speed;
                agent.isStopped = true; // 이동 중지
                break;
            case AIState.Attacking:
                agent.speed = stats.Speed;
                agent.isStopped = true; // 공격 시 이동 정지
                break;
        }
        
    }

    protected override void PassiveUpdate()
    {
        // 현재 위치에서 경로를 미리 계산
        path = new NavMeshPath();
        agent.CalculatePath(CharacterManager.Instance.player.transform.position, path);

        if (playerDistance < detectDistance)
        {
            SetState(AIState.Attacking);
            return;
        }

        if (agent.remainingDistance < 0.3f)
        {
            SetState(AIState.Idle);
        }
    }

    // 일정 시간 후 원래 위치로 이동
    void ReturnToDefaultLocation()
    {
        agent.SetDestination(defaultPos);
    }
    
    protected override void AttackingUpdate()
    {

        if (playerDistance > detectDistance)
        {
            SetState(AIState.Idle);
            return;
        }

        if (playerDistance < attackDistance)
        {
            agent.isStopped = true;

            // 공격 가능 시간인지 체크
            if (Time.time - lastAttackTime > attackRate)
            {
                lastAttackTime = Time.time; // 마지막 공격 시간 갱신
                Collider[] hit = Physics.OverlapBox(transform.position, new Vector3(1f, 1f, 1f), Quaternion.identity, layerMask);
                if (hit.Length > 0)
                {
                    DealDamage();
                }
                animator.speed = 1f; // 애니메이션 속도 설정
                animator.SetTrigger("Attack"); // 공격 애니메이션 실행
            }
        }
        else
        {
            agent.isStopped = false;
            agent.speed = stats.Speed;
            animator.SetFloat("MoveSpeed", Mathf.Abs(agent.velocity.magnitude));
            agent.SetDestination(CharacterManager.Instance.player.transform.position);
        }
    }
    
    public void DealDamage()
    {
            CharacterManager.Instance.player.GetComponent<IDamageable>().TakeDamage(attackDamage);
    }
    

    // 데미지를 받았을 때 처리
    public override void TakeDamage(float damage)
    {
        resourceController.ChangeHealth(-damage);
        if (resourceController.CurrentHealth <= 0)
        {
            Die();
        }

        StartCoroutine(DamageFlash()); // 피격 효과
    }

    // 사망 처리
    void Die()
    {   

        if (dropTable != null)
        {
            // 아이템 드롭
            ItemData droppedItem = dropTable.GetDroppedItem();
            if (droppedItem != null)
            {
                InventoryManager.Instance.AddItem(droppedItem);
            }
            // 골드 추가
            InventoryManager.Instance.AddGold(dropTable.dropGoldAmount);
        }
        OnDeath?.Invoke();
        Destroy(gameObject);
    }

    // 피격 시 빨갛게 변했다가 다시 원래 색으로 복귀
    IEnumerator DamageFlash()
    {
        // 모든 스킨 메쉬 렌더러에 대해 색상 변경
        foreach (var renderer in meshRenderers)
        {
            renderer.material.color = new Color(1.0f, 0.6f, 0.6f);  // 빨갛게 변경
        }
        yield return new WaitForSeconds(0.1f);
        // 원래 색상으로 변경
        foreach (var renderer in meshRenderers)
        {
            renderer.material.color = Color.white;
        }
    }

}
