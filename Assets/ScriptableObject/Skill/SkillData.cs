using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum SkillEffectType { DamageBoost, FireRateBoost, ExplosiveBullet, ChainShot, Stun, Custom }

public interface ISkill
{
    string SkillName { get; }
    float CoolDown { get; }
    Sprite SkillIcon { get; }
    void Activate();
}

[CreateAssetMenu(fileName = "NewSkill", menuName = "Skill")]
public class SkillData : ScriptableObject
{
    public string skillName;
    public ISkill skill;
    public string description;
    public float cooldown;
    public Sprite skillIcon;
}