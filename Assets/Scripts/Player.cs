using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player : MonoBehaviour, IDamageable
{   
    public float Health { get; set; }
    public EnemyController target;
    public Rigidbody rb;
    public Animator animator;
    public Transform weaponPos;

    public float detectionRadius = 10;
    public LayerMask layerMask;
    public float fireRate = 0.1f;
    private float lastFireTime = 0f;

    void Update()
    {
        FindEnemy();
        Move();
        Look();
        AnimationUpdate();
        Fire();
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.W))
            rb.AddForce(Vector3.forward * 10f);
        else if (Input.GetKey(KeyCode.S))
            rb.AddForce(Vector3.back * 10f);
        else if (Input.GetKey(KeyCode.A))
            rb.AddForce(Vector3.left * 10f);
        else if (Input.GetKey(KeyCode.D))
            rb.AddForce(Vector3.right * 10f);
    }

    void Look()
    {
        if (target != null)
            transform.LookAt(target.transform);
    }

    void AnimationUpdate()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.magnitude));
    }

    void Fire()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time >= lastFireTime + fireRate)
        {
            float playerYRotation = transform.eulerAngles.y;
            ProjectileManager.Instance.FireBullet(weaponPos, new Vector3(0, playerYRotation, 0));

            lastFireTime = Time.time;
        }
    }

    public void FindEnemy()
    {
        Collider[] targetColiders = Physics.OverlapSphere(transform.position, detectionRadius, layerMask);
        Vector3 distance = Vector3.zero;
        if (targetColiders.Length > 0)
        {
            for (int i = 0; i < targetColiders.Length; i++)
            {
                if (distance.magnitude < (targetColiders[i].transform.position - transform.position).magnitude)
                {
                    EnemyController detectedTarget = targetColiders[i].gameObject.GetComponent<EnemyController>();
                    distance = targetColiders[i].transform.position - transform.position;
                    if (target != null)
                    {
                        target = detectedTarget;
                    }
                }
            }
        }
    }

    public void TakeDamage(float amount)
    {
        Debug.Log($"데미지를 입었습니다! : {amount}");
    }
}