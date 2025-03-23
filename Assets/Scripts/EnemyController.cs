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
    

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();           // 네비게이션 에이전트 가져오기
        animator = GetComponent<Animator>();            // 애니메이터 가져오기
        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>(); // 캐릭터의 메쉬 렌더러 가져오기
        // NavMesh 위의 가장 가까운 지점으로 이동 (Terrain에서 오류 방지)
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 10f, NavMesh.AllAreas))
        {
            transform.position = (hit.position);
			defaultPos = hit.position;
        }
    }

    void Start()
    {
        defaultDetectDistance = detectDistance; // 기본 감지 거리 저장
        SetState(AIState.Idle); // 시작할 때 Idle상태로 전환
    }

    void Update()
    {
        // 플레이어와의 거리 계산
        playerDistance = Vector3.Distance(transform.position, CharacterManager.Instance.player.transform.position);


        // 현재 AI 상태에 따라 동작 분기
        switch (aiState)
        {
            case AIState.Idle:
                PassiveUpdate(); // 감지 및 리턴 로직 실행
                break;
            case AIState.Attacking:
                AttackingUpdate(); // 전투 로직 실행
                break;
        }
    }

    /// <summary>
    /// AI 상태 변경 함수
    /// </summary>
    /// <param name="state">변경할 AI 상태</param>
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

        // 애니메이션 속도를 걷기 속도 기준으로 조절
        // animator.speed = agent.speed / walkSpeed;
    }

    // 플레이어 감지 및 배회 관련 업데이트
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


    bool CanReachToPlayer()
    {
        agent.areaMask = NavMesh.GetAreaFromName("SettlementArea");

        agent.CalculatePath(CharacterManager.Instance.player.transform.position, path);
        return (path.status == NavMeshPathStatus.PathComplete);
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

    // 애니메이션 이벤트로 호출될 메서드
    public void DealDamage()
    {
            CharacterManager.Instance.player.GetComponent<IDamageable>().TakeDamage(attackDamage);
    }


    // 플레이어가 NPC의 시야 내에 있는지 확인
    // bool IsPlayerInFieldOfView()
    // {
    //     Vector3 directionToPlayer = CharacterManager.Instance.player.transform.position - transform.position;
    //     float angle = Vector3.Angle(transform.forward, directionToPlayer);
    //     return angle < fieldOfView * 0.5f;
    // }

    // 데미지를 받았을 때 처리
    public void TakeDamage(float damage)
    {
        stats.Health -= damage;
        if (stats.Health <= 0)
        {
            Die();
        }

        StartCoroutine(DamageFlash()); // 피격 효과
    }

    // 사망 처리
    void Die()
    {

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
