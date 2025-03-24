using System.Collections;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public Weapon equippedWeapon; // 현재 장착 중인 무기
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
        Debug.Log(skillIndex);
        Debug.Log(equippedWeapon.data.allSkills[skillIndex]);
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
}