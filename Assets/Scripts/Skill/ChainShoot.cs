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
    public int numberOfChainBullets = 5;
    public float attackDelay = 0.2f;
    private int currentBullet = 0;

    public void Start()
    {
        Debug.Log(SkillIcon);
    }

    public void Activate()
    {
        StartAttack();
    }

    private void StartAttack()
    {
        InvokeRepeating(nameof(FireChainBullet), 0f, attackDelay);
    }

    private void FireChainBullet()
    {
        if (currentBullet < numberOfChainBullets)
        {
            Debug.Log("Shot");
            ProjectileManager.Instance.chainProjectilePool.GetProjectile(CharacterManager.Instance.player.weaponPos,
                new Vector3(0, CharacterManager.Instance.player.transform.eulerAngles.y, 0));
            currentBullet++;
        }
        else
        {
            CancelInvoke(nameof(FireChainBullet));
            currentBullet = 0;
        }
    }
}

