using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public EquipmentData data;

    private void Awake()
    {
        data.allSkills = new ISkill[3];
    }
}
