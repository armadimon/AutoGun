using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public Weapon equippedWeapon; // 현재 장착 중인 무기
    public Image[] skillIcons; // 3개의 스킬 UI 슬롯
    private float[] skillCooldowns = new float[3]; // 각 스킬의 남은 쿨타임

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
        if (equippedWeapon == null || equippedWeapon.data.allSkills[skillIndex] == null)
            return;

        ISkill skill = equippedWeapon.data.allSkills[skillIndex];

        if (skillCooldowns[skillIndex] > 0)
        {
            Debug.Log($"{skillIndex} 사용 불가! 남은 쿨타임: {skillCooldowns[skillIndex]:F1}초");
            return;
        }

        Debug.Log($"{skillIndex} 사용!");

        skill.Activate();
        skillCooldowns[skillIndex] = skill.CoolDown; // 쿨타임 적용
    }

    private void ApplySkillEffect(ISkill skill)
    {
    }

    private IEnumerator TemporaryBuff(float duration, System.Action applyEffect)
    {
        applyEffect();
        yield return new WaitForSeconds(duration);
        // 버프 제거 코드
    }
    
    private void OnEnable()
    {
        InventoryManager.OnWeaponChanged += UpdateSkillUI; // 이벤트 등록
    }

    private void OnDisable()
    {
        InventoryManager.OnWeaponChanged -= UpdateSkillUI; // 이벤트 해제
    }

    private void UpdateSkillUI(WeaponData weapon)
    {
        Debug.Log(skillIcons.Length);
        for (int i = 0; i < 1; i++)
        // for (int i = 0; i < skillIcons.Length; i++)
        {
            if (weapon.allSkills[i].SkillIcon != null)
            {
                skillIcons[i].sprite = weapon.allSkills[i].SkillIcon;
            }
            else
            {
                // skillIcons[i].gameObject.SetActive(false); // 스킬이 없으면 숨김
            }
        }
    }
    
}