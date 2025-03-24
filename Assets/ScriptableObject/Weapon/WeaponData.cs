using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { SubmachineGun, Shotgun, Sniper }
public enum WeaponRarity { Common, Rare, Epic }

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/Weapon")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public WeaponType type;
    public WeaponRarity rarity;
    public int baseDamage;
    public float fireRate;
    public ISkill[] allSkills; // 가능한 모든 스킬 (최대 3개)

    public int GetDamageByLevel(int level)
    {
        float rarityMultiplier = 1 + ((int)rarity * 0.2f); // 등급에 따른 배율 증가
        return Mathf.RoundToInt(baseDamage * rarityMultiplier * (1 + level * 0.1f));
    }

    public ISkill GetUnlockedSkill(int level)
    {
        if (level >= 7) return allSkills[2];
        if (level >= 5) return allSkills[1];
        if (level >= 3) return allSkills[0];
        return null;
    }
}
