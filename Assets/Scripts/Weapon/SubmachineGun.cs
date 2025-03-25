using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmachineGun : Weapon
{
    public GameObject chainSkill;

    private void Start()
    {
        
        if (chainSkill == null)
        {
            Debug.LogError("Chain Skill is not assigned!");
            return;
        }

        ISkill temp = chainSkill.GetComponent<ISkill>();
        if (temp == null)
        {
            Debug.LogError("ISkill component not found on chainSkill!");
        }
        else
        {
            Debug.Log("ISkill component found!");
            data.allSkills[0] = temp;  // 스킬 할당
            Debug.Log(data.allSkills[0].SkillIcon);
        }
    }
}