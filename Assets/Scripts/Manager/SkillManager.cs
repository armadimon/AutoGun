using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public Weapon equippedWeapon;
    public Image[] skillIcons;
    private float[] skillCooldowns = new float[3];

    private void Start()
    {
        if (InventoryManager.Instance.currentWeapon != null)
            UpdateSkillUI(InventoryManager.Instance.currentWeapon.data);
    }

    void Update()
    {
        for (int i = 0; i < skillCooldowns.Length; i++)
        {
            if (skillCooldowns[i] > 0)
                skillCooldowns[i] -= Time.deltaTime;
        }
    }

    public void UseSkill(int skillIndex)
    {
        equippedWeapon = CharacterManager.Instance.player.currentWeapon;
        if (equippedWeapon == null || equippedWeapon.data.allSkills[skillIndex] == null)
            return;

        ISkill skill = equippedWeapon.data.allSkills[skillIndex];

        if (skillCooldowns[skillIndex] > 0)
        {
            Debug.Log($"{skillIndex} 사용 불가! 남은 쿨타임: {skillCooldowns[skillIndex]:F1}초");
            return;
        }

        skill.Activate();
        skillCooldowns[skillIndex] = skill.CoolDown;
    }
    
    
    private void OnEnable()
    {
        InventoryManager.OnWeaponChanged += UpdateSkillUI;
    }

    private void OnDisable()
    {
        InventoryManager.OnWeaponChanged -= UpdateSkillUI;
    }

    private void UpdateSkillUI(EquipmentData weapondata)
    {
        Debug.Log(weapondata.skillData.Length);
        if (weapondata.equipmentType == EquipmentType.Weapon)
        {
            for (int i = 0; i < weapondata.skillData.Length; i++)
            {
                if (weapondata.skillData[i].skillIcon != null)
                {
                    skillIcons[i].sprite = weapondata.skillData[i].skillIcon;
                }
                else
                {
                    skillIcons[i].gameObject.SetActive(false);
                }
            }
        }
    }
    
}