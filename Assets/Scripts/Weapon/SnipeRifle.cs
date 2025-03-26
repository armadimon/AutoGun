using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnipeRifle : Weapon
{
    public GameObject SnipeShot;

    public SnipeShot snipeShotInstance;
    private void Start()
    {
        GameObject skillObject = Instantiate(SnipeShot);
        snipeShotInstance = skillObject.GetComponent<SnipeShot>();

        ISkill temp = snipeShotInstance.GetComponent<ISkill>();
        if (temp == null)
        {
            Debug.LogError("ISkill component not found!");
        }
        else
        {
            Debug.Log("ISkill component found!");
            data.allSkills[0] = temp;
            Debug.Log(data.allSkills[0].SkillIcon);
        }
    }
}