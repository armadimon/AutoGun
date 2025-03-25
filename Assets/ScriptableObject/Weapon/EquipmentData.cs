using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum WeaponType { SubmachineGun, Shotgun, Sniper }
public enum EquipmentType { Weapon, Armor }
public enum EquipmentRarity { Common, Rare, Epic }

[CreateAssetMenu(fileName = "NewEquipment", menuName = "Equipment/Equipment")]
public class EquipmentData : ItemData
{
    public EquipmentType equipmentType;
    public GameObject equipmentPrefab;
    public EquipmentRarity rarity;
    public int baseValue; // 무기: 공격력, 방어구: 방어력

    // 무기 전용 속성
    public WeaponType weaponType;
    public float fireRate;
    public ISkill[] allSkills; // 무기일 때만 사용
    public SkillData[] skillData;

    public int GetPowerByLevel(int level)
    {
        float rarityMultiplier = 1 + ((int)rarity * 0.2f);
        return Mathf.RoundToInt(baseValue * rarityMultiplier * (1 + level * 0.1f));
    }

    public ISkill GetUnlockedSkill(int level)
    {
        if (equipmentType == EquipmentType.Weapon && allSkills != null)
        {
            if (level >= 10) return allSkills.Length > 2 ? allSkills[2] : null;
            if (level >= 5) return allSkills.Length > 1 ? allSkills[1] : null;
            if (level >= 1) return allSkills.Length > 0 ? allSkills[0] : null;
        }
        return null;
    }
}