using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillButtonUI : MonoBehaviour
{
    public SkillManager skillManager;
    public int skillIndex;

    public void OnClick()
    {
        skillManager.UseSkill(skillIndex);
    }
}
