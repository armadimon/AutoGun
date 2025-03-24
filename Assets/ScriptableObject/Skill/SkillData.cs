using System.Collections;
using UnityEngine;

public enum SkillEffectType { DamageBoost, FireRateBoost, ExplosiveBullet, ChainShot, Stun, Custom }

public interface ISkill
{
    string SkillName { get; }
    float CoolDown { get; }
    void Activate();
}

[CreateAssetMenu(fileName = "NewSkill", menuName = "Weapons/Skill")]
public class SkillData : ScriptableObject
{
    public string skillName;
    public ISkill skill;
    public string description;
    public float effectValue; // 효과 수치 (ex: 데미지 20% 증가)
    public float duration; // 지속 시간 (0이면 즉시 효과)
    public float cooldown; // 액티브 스킬일 경우 쿨타임
    public Sprite skillIcon; // UI 아이콘
}