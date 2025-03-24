using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmachineGun : Weapon
{
    public GameObject chainSkill;  // 인스펙터에서 할당된 체인 스킬 프리팹

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
        }
    }
}