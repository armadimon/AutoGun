using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChainShoot : MonoBehaviour, ISkill
{
    public String SkillName { get; set; }
    [SerializeField]
    private Sprite _skillIcon;
    public Sprite SkillIcon {get{return _skillIcon;} set{_skillIcon = value;}}
    public float CoolDown { get; set; }
    public int numberOfChainBullets = 5; // 체인 탄환 수
    public float attackDelay = 0.2f; // 각 공격 사이의 딜레이 시간
    private int currentBullet = 0;

    public void Start()
    {
        Debug.Log(SkillIcon);
    }

    public void Activate()
    {
        if (currentBullet == 0)
        {
            StartAttack(); // 공격 시작
        }
    }

    private void StartAttack()
    {
        // 체인샷을 반복적으로 발사하기 위해 Invoke 호출
        InvokeRepeating(nameof(FireChainBullet), 0f, attackDelay); // 최초 즉시 발사, 이후 attackDelay마다 발사
    }

    private void FireChainBullet()
    {
        // 탄환을 발사하고, 발사 횟수 증가
        if (currentBullet < numberOfChainBullets)
        {
            Debug.Log("Shot");
            ProjectileManager.Instance.chainProjectilePool.GetProjectile(CharacterManager.Instance.player.weaponPos,
                new Vector3(0, CharacterManager.Instance.player.transform.eulerAngles.y, 0));
            currentBullet++;
        }
        else
        {
            // 모든 탄환을 발사한 후 InvokeRepeating을 멈춤
            CancelInvoke(nameof(FireChainBullet));
            currentBullet = 0; // 공격이 끝난 후 초기화
        }
    }
}

