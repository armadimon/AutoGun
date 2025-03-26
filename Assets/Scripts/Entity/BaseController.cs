using System.Collections;
using UnityEngine;
using UnityEngine.AI;


// NPC 클래스: 네비게이션, 배회, 전투 기능을 수행
public abstract class BaseController : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    public StatHandler stats;

	//    public ItemData[] dropOnDeath;
	// 사망 시 드롭할 아이템 목록

    [Header("AI")]
    protected NavMeshAgent agent;     // 네비게이션 에이전트 (이동 제어)
    protected NavMeshPath path;       // 경로 계산을 위한 NavMeshPath
    public float detectDistance;    // 플레이어 감지 거리
    protected AIState aiState;        // 현재 AI 상태
    public LayerMask layerMask;   // 건물 레이어
    protected Vector3 defaultPos;
    bool player = false;

    [Header("Combat")]
    public float attackDamage;             // 공격력
    public float attackRate;         // 공격 속도 (공격 간격)
    protected float lastAttackTime;    // 마지막 공격 시간
    public float attackDistance;     // 공격 거리
    protected float targetDistance;    // 타겟과의 거리
    public ResourceController resourceController;
    protected Animator animator;       // 애니메이터
    protected SkinnedMeshRenderer[] meshRenderers; // 캐릭터의 스킨 메쉬 렌더러 (피격 효과용)
    

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
        SetState(AIState.Idle); // 시작할 때 Idle상태로 전환
    }

    void Update()
    {


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
    protected abstract void PassiveUpdate();
    
    protected abstract void AttackingUpdate();
    
    
    // 일정 시간 후 원래 위치로 이동
    void ReturnToDefaultLocation()
    {
        agent.SetDestination(defaultPos);
    }


    public abstract void TakeDamage(float damage);

    public void Death()
    {
        Destroy(gameObject);
    }
}