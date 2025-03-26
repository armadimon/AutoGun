using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseController : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    public StatHandler stats;

    [Header("AI")]
    protected NavMeshAgent agent;
    protected NavMeshPath path;
    public float detectDistance;
    protected AIState aiState;
    public LayerMask layerMask;
    protected Vector3 defaultPos;
    bool player = false;

    [Header("Combat")]
    public float attackDamage;
    public float attackRate;
    protected float lastAttackTime;
    public float attackDistance;
    protected float targetDistance;
    public ResourceController resourceController;
    protected Animator animator;
    protected SkinnedMeshRenderer[] meshRenderers;
    

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
        SetState(AIState.Idle);
    }

    void Update()
    {
        
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
                agent.isStopped = true;
                break;
            case AIState.Attacking:
                agent.speed = stats.Speed;
                agent.isStopped = true;
                break;
        }

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