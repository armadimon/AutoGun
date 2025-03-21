using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player : MonoBehaviour
{
    public Enemy target;
    public Rigidbody rb;
    public Animator animator;
    public Transform weaponPos;
    
    public float fireRate = 0.1f;
    private float lastFireTime = 0f;

    void Update()
    {
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
}